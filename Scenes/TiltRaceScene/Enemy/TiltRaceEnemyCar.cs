using UnityEngine;

using MovePatternType = TakahashiH.Scenes.TiltRace.EnemyCarMovePatternType;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �G�̎�
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UITiltRaceCar))]
    public sealed class TiltRaceEnemyCar : ExMonoBehaviour, ITiltRaceEnemyCarCollision
    {
        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �� UI
        /// </summary>
        [SerializeField] private UITiltRaceCar UICar;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �ړ��p�^�[�����X�g
        /// </summary>
        private ITiltRaceEnemyCarMovePattern[] mMovePatternList = new ITiltRaceEnemyCarMovePattern[(int)MovePatternType.Sizeof];

        /// <summary>
        /// �ړ��p�^�[��
        /// </summary>
        private ITiltRaceEnemyCarMovePattern mMovePattern;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// ���W
        /// </summary>
        public Vector3 Position => UICar.Position;

        /// <summary>
        /// ����
        /// </summary>
        public float Width => UICar.Width;

        /// <summary>
        /// �c��
        /// </summary>
        public float Height => UICar.Height;

        /// <summary>
        /// �v���C���[�ɗ^����_���[�W��
        /// </summary>
        public int Damage { get; private set; }


        //====================================
        //! �֐��iMonoBehaviour�j
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
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// ������
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
        /// �Z�b�g�A�b�v
        /// </summary>
        /// <param name="id">                  ID                              </param>
        /// <param name="sprite">              �X�v���C�g                      </param>
        /// <param name="position">            ���W                            </param>
        /// <param name="movePatternType">     �ړ��p�^�[�����                </param>
        /// <param name="damage">              �v���C���[�ɗ^����_���[�W��    </param>
        /// <param name="speed">               ���x                            </param>
        /// <param name="scale">               �X�P�[��                        </param>
        /// <param name="moveTimeSec">         �ړ����ԁi�b�j                  </param>
        /// <param name="stopTimeSec">         ��~���ԁi�b�j                  </param>
        /// <param name="hormingPowerRate">    �z�[�~���O�̓��[�g              </param>
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
        /// ���W�X�V
        /// </summary>
        /// <param name="playerCarPosition"> �v���C���[�̎Ԃ̍��W </param>
        public void UpdatePosition(Vector3 playerCarPosition)
        {
            mMovePattern.UpdateMoveVec(UICar.Position, playerCarPosition);

            var pos = UICar.Position + mMovePattern.MoveVec;

            UICar.SetPosition(pos);
        }

        /// <summary>
        /// �ꎞ��~
        /// </summary>
        public void Pause()
        {
            mMovePattern.Pause();
        }

        /// <summary>
        /// �ĊJ
        /// </summary>
        public void Resume()
        {
            mMovePattern.Resume();
        }
    }
}
