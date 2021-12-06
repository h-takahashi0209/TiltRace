using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 敵の車移動パターン基底 - 直退を停止を繰り返す
    /// </summary>
    public sealed class TiltRaceEnemyCarMovePatternLinerAndStop : TiltRaceEnemyCarMovePatternBase
    {
        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 基準移動ベクトル
        /// </summary>
        private Vector3 mDefMoveVec;

        /// <summary>
        /// 停止中か
        /// </summary>
        private bool mIsStop;


        //====================================
        //! 関数（MovePatternBase）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        protected override void DoSetup()
        {
            mIsStop     = false;
            mDefMoveVec = Vector3.down;

            mTimer.Begin(mMoveTimeSec, () => SwitchState());
        }

        /// <summary>
        /// 移動ベクトル更新
        /// </summary>
        protected override void DoUpdateMoveVec()
        {
            if (mIsStop)
            {
                MoveVec = Vector3.zero;
            }
            else
            {
                MoveVec = mDefMoveVec * mSpeed * TimeManager.DeltaTime;
            }
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 移動 / 停止の状態切り替え
        /// </summary>
        private void SwitchState()
        {
            mIsStop = !mIsStop;

            float waitTimeSec = mIsStop ? mStopTimeSec : mMoveTimeSec;

            mTimer.Begin(waitTimeSec, () => SwitchState());
        }
    }
}
