using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 走行距離ごとのイベント設定
    /// </summary>
    [CreateAssetMenu(fileName = nameof(TiltRaceDistanceEventSettings), menuName = "ScriptableObjects/Scenes/TiltRaceScene/" + nameof(TiltRaceDistanceEventSettings))]
    public sealed class TiltRaceDistanceEventSettings : ScriptableObject
    {
        //====================================
        //! 変数（public）
        //====================================

        /// <summary>
        /// レベルアップする走行距離間隔
        /// </summary>
        public float LevelUpDistanceInterval;

        /// <summary>
        /// ライフを回復させる走行距離間隔
        /// </summary>
        public float RecoveredLifeDistanceInterval;

        /// <summary>
        /// ライフ回復量
        /// </summary>
        public int RecoveredLife;
    }
}
