using UnityEngine;

using MovePatternType = TakahashiH.Scenes.TiltRace.EnemyCarMovePatternType;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 敵の車
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UITiltRaceCar))]
    public sealed class TiltRaceEnemyCar : ExMonoBehaviour, ITiltRaceEnemyCarCollision
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 車 UI
        /// </summary>
        [SerializeField] private UITiltRaceCar UICar;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 移動パターンリスト
        /// </summary>
        private ITiltRaceEnemyCarMovePattern[] mMovePatternList = new ITiltRaceEnemyCarMovePattern[(int)MovePatternType.Sizeof];

        /// <summary>
        /// 移動パターン
        /// </summary>
        private ITiltRaceEnemyCarMovePattern mMovePattern;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 座標
        /// </summary>
        public Vector3 Position => UICar.Position;

        /// <summary>
        /// 横幅
        /// </summary>
        public float Width => UICar.Width;

        /// <summary>
        /// 縦幅
        /// </summary>
        public float Height => UICar.Height;

        /// <summary>
        /// プレイヤーに与えるダメージ量
        /// </summary>
        public int Damage { get; private set; }


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            UICar.GetComponent<UITiltRaceCar>();
        }

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            mMovePattern.Dispose();
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            for (int i = 0; i < (int)MovePatternType.Sizeof; i++)
            {
                mMovePatternList[i] = (MovePatternType)i switch
                {
                    MovePatternType.Liner           => mMovePatternList[i] = new TiltRaceEnemyCarMovePatternLiner           (),
                    MovePatternType.LinerAndStop    => mMovePatternList[i] = new TiltRaceEnemyCarMovePatternLinerAndStop    (),
                    MovePatternType.Horming         => mMovePatternList[i] = new TiltRaceEnemyCarMovePatternHorming         (),
                    MovePatternType.Zigzag          => mMovePatternList[i] = new TiltRaceEnemyCarMovePatternZigzag          (),
                    MovePatternType.ZigzagAndStop   => mMovePatternList[i] = new TiltRaceEnemyCarMovePatternZigzagAndStop   (),
                    _                               => mMovePatternList[i] = new TiltRaceEnemyCarMovePatternLiner           (),
                };
            }
        }

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="id">                  ID                              </param>
        /// <param name="sprite">              スプライト                      </param>
        /// <param name="position">            座標                            </param>
        /// <param name="movePatternType">     移動パターン種別                </param>
        /// <param name="damage">              プレイヤーに与えるダメージ量    </param>
        /// <param name="speed">               速度                            </param>
        /// <param name="scale">               スケール                        </param>
        /// <param name="moveTimeSec">         移動時間（秒）                  </param>
        /// <param name="stopTimeSec">         停止時間（秒）                  </param>
        /// <param name="hormingPowerRate">    ホーミング力レート              </param>
        public void Setup
        (
            int                 id                  ,
            Sprite              sprite              ,
            Vector3             position            ,
            MovePatternType     movePatternType     ,
            int                 damage              ,
            float               speed               ,
            float               scale               ,
            float               moveTimeSec         ,
            float               stopTimeSec         ,
            float               hormingPowerRate
        )
        {
            Id     = id;
            Damage = damage;

            mMovePattern = mMovePatternList[(int)movePatternType];
            mMovePattern.Setup(speed, moveTimeSec, stopTimeSec, hormingPowerRate);

            UICar.Setup(sprite, position, scale);
        }

        /// <summary>
        /// 座標更新
        /// </summary>
        /// <param name="playerCarPosition"> プレイヤーの車の座標 </param>
        public void UpdatePosition(Vector3 playerCarPosition)
        {
            mMovePattern.UpdateMoveVec(UICar.Position, playerCarPosition);

            var pos = UICar.Position + mMovePattern.MoveVec;

            UICar.SetPosition(pos);
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public void Pause()
        {
            mMovePattern.Pause();
        }

        /// <summary>
        /// 再開
        /// </summary>
        public void Resume()
        {
            mMovePattern.Resume();
        }
    }
}
