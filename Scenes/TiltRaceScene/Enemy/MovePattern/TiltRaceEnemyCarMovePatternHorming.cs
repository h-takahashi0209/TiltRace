using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 敵の車移動パターン基底 - 追従
    /// </summary>
    public sealed class TiltRaceEnemyCarMovePatternHorming : TiltRaceEnemyCarMovePatternBase
    {
        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 基準移動ベクトル
        /// </summary>
        private Vector3 mDefMoveVec;


        //====================================
        //! 関数（MovePatternBase）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        protected override void DoSetup()
        {
            mDefMoveVec = Vector3.down;
        }

        /// <summary>
        /// 移動ベクトル更新
        /// </summary>
        protected override void DoUpdateMoveVec()
        {
            // プレイヤーに向かいつつ下に流れるよう、下方向のベクトルを足し合わせる
            var targetVec = (mPlayerCarPosition - mMyCarPosition).normalized * mHormingPowerRate + mDefMoveVec;

            MoveVec = targetVec * mSpeed * TimeManager.DeltaTime;
        }
    }
}
