using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    public interface ITiltRaceEnemyCarMovePattern
    {
        Vector3 MoveVec { get; }

        void Setup(float speed, float moveTimeSec, float stopTimeSec, float hormingPowerRate);
        void UpdateMoveVec(Vector3 myCarPosition, Vector3 playerCarPosition);
        void Dispose();
        void Pause();
        void Resume();
    }

    /// <summary>
    /// TiltRace - 敵の車移動パターン基底
    /// </summary>
    public abstract class TiltRaceEnemyCarMovePatternBase : ITiltRaceEnemyCarMovePattern
    {
        //====================================
        //! 変数（protected）
        //====================================

        /// <summary>
        /// 速度
        /// </summary>
        protected float mSpeed;

        /// <summary>
        /// 自身の車の座標
        /// </summary>
        protected Vector3 mMyCarPosition;

        /// <summary>
        /// プレイヤーの車の座標
        /// </summary>
        protected Vector3 mPlayerCarPosition;

        /// <summary>
        /// 移動時間（秒）
        /// </summary>
        protected float mMoveTimeSec;

        /// <summary>
        /// 停止時間（秒）
        /// </summary>
        protected float mStopTimeSec;

        /// <summary>
        /// ホーミング力レート
        /// </summary>
        protected float mHormingPowerRate;

        /// <summary>
        /// タイマー
        /// </summary>
        protected Timer mTimer = new Timer();


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 移動ベクトル
        /// </summary>
        public Vector3 MoveVec { get; protected set; }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="speed">               速度                  </param>
        /// <param name="moveTimeSec">         移動時間（秒）        </param>
        /// <param name="stopTimeSec">         停止時間（秒）        </param>
        /// <param name="hormingPowerRate">    ホーミング力レート    </param>
        public void Setup(float speed, float moveTimeSec, float stopTimeSec, float hormingPowerRate)
        {
            mSpeed              = speed;
            mMoveTimeSec        = moveTimeSec;
            mStopTimeSec        = stopTimeSec;
            mHormingPowerRate   = hormingPowerRate;

            DoSetup();
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            mTimer.Dispose();
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public void Pause()
        {
            mTimer.Pause();
        }

        /// <summary>
        /// 再開
        /// </summary>
        public void Resume()
        {
            mTimer.Resume();
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 移動ベクトル更新
        /// </summary>
        /// <param name="myCarPosition">        自身の車の座標          </param>
        /// <param name="playerCarPosition">    プレイヤーの車の座標    </param>
        public void UpdateMoveVec(Vector3 myCarPosition, Vector3 playerCarPosition)
        {
            mMyCarPosition      = myCarPosition;
            mPlayerCarPosition  = playerCarPosition;

            mTimer.UpdateTimer(TimeManager.DeltaTime);

            DoUpdateMoveVec();
        }


        //====================================
        //! 関数（protected virtual）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        protected virtual void DoSetup() {}

        /// <summary>
        /// 移動ベクトル更新
        /// </summary>
        protected virtual void DoUpdateMoveVec() {}
    }
}
