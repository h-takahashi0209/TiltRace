using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - プレイヤー設定
    /// </summary>
    [CreateAssetMenu(fileName = nameof(TiltRacePlayerSettings), menuName = "ScriptableObjects/Scenes/TiltRaceScene/" + nameof(TiltRacePlayerSettings))]
    public sealed class TiltRacePlayerSettings : ScriptableObject
    {
        //====================================
        //! 変数（public）
        //====================================

        /// <summary>
        /// 初期ライフ
        /// </summary>
        public int DefLife;

        /// <summary>
        /// 最大ライフ
        /// </summary>
        public int MaxLife;

        /// <summary>
        /// 被ダメージ時の無敵時間（秒）
        /// </summary>
        public float DamageInvincibleTimeSec;

        /// <summary>
        /// 1フレームごとの走行距離
        /// </summary>
        public float OneFrameDistance;

        /// <summary>
        /// 車の色
        /// </summary>
        public CarColor CarColor;

        /// <summary>
        /// 基準移動速度
        /// </summary>
        public float DefSpeed;

        /// <summary>
        /// 移動速度下限
        /// </summary>
        public float SpeedMin;
    }
}
