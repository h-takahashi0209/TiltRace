using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 敵の車移動パターン基底 - ジグザグ移動と停止を繰り返す
    /// </summary>
    public sealed class TiltRaceEnemyCarMovePatternZigzagAndStop : TiltRaceEnemyCarMovePatternBase
    {
        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 左向きか
        /// </summary>
        private bool mIsLeft;

        /// <summary>
        /// 停止中か
        /// </summary>
        private bool mIsStop;

        /// <summary>
        /// 移動角度
        /// </summary>
        private Vector3 mMoveAngle;


        //====================================
        //! 関数（MovePatternBase）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        protected override void DoSetup()
        {
            mIsLeft = Random.Range(0, 1) == 0 ? true : false;

            SwitchDirection();

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
                return;
            }

            MoveVec = mMoveAngle * mSpeed * TimeManager.DeltaTime;

            var nextFramePosition = mMyCarPosition + MoveVec;

            // 次のフレームでの座標が移動範囲を越えているか？
            bool isSwitchDirection =
            (
                nextFramePosition.x < -TiltRaceSettings.WidthLimit
            ||  nextFramePosition.x >  TiltRaceSettings.WidthLimit
            );

            // 移動範囲を越えたら向きを変える
            if(isSwitchDirection)
            {
                SwitchDirection();
            }
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 向き切り替え
        /// </summary>
        private void SwitchDirection()
        {
            mIsLeft = !mIsLeft;

            var direction   = mIsLeft ? -1f : 1f;
            var angle       = Random.Range(TiltRaceSettings.Enemy.ZigzagMoveAngleMin, TiltRaceSettings.Enemy.ZigzagMoveAngleMax) * direction;
            var moveVec     = Quaternion.Euler(180f, 0f, angle) * Vector3.up;

            mMoveAngle  = moveVec.normalized;
            MoveVec     = mMoveAngle * mSpeed * TimeManager.DeltaTime;
        }

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
