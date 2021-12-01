using System;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - アイテム設定
    /// </summary>
    [CreateAssetMenu(fileName = nameof(TiltRaceItemSettings), menuName = "ScriptableObjects/Scenes/TiltRaceScene/" + nameof(TiltRaceItemSettings))]
    public sealed class TiltRaceItemSettings : ScriptableObject
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// アイテム種別ごとの生成確率
        /// </summary>
        [Serializable]
        public class GenerateProbability
        {
            /// <summary>
            /// アイテム種別
            /// </summary>
            public ItemType ItemType;

            /// <summary>
            /// 確率
            /// </summary>
            public int Probability;
        }


        //====================================
        //! 変数（public）
        //====================================

        /// <summary>
        /// 基準生成時間（秒）
        /// </summary>
        public float BaseGenerateTimeSec;

        /// <summary>
        /// 生成時間範囲（秒）
        /// </summary>
        public float GenerateTimeRangeSec;

        /// <summary>
        /// 移動速度
        /// </summary>
        public float Speed;

        /// <summary>
        /// プレイヤーのライフ回復量
        /// </summary>
        public int RecoveredPlayerLife;

        /// <summary>
        /// プレイヤーの速度加算量
        /// </summary>
        public int AddedPlayerSpeed;

        /// <summary>
        /// アイテム種別ごとの生成確率リスト
        /// </summary>
        public GenerateProbability[] GenerateProbabilityList;
    }
}
