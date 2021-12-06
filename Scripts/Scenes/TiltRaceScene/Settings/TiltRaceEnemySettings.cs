using System;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 敵設定
    /// </summary>
    [CreateAssetMenu(fileName = nameof(TiltRaceEnemySettings), menuName = "ScriptableObjects/Scenes/TiltRaceScene/" + nameof(TiltRaceEnemySettings))]
    public sealed class TiltRaceEnemySettings : ScriptableObject
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// パラメータ
        /// </summary>
        [Serializable]
        public class Param
        {
            /// <summary>
            /// 初期値
            /// </summary>
            public float Def;

            /// <summary>
            /// 範囲
            /// </summary>
            public float Range;

            /// <summary>
            /// レベルアップ時の加算値
            /// </summary>
            public float LevelUp;

            /// <summary>
            /// 下限
            /// </summary>
            public float Min;
        }

        /// <summary>
        /// 移動パターンごとの生成確率
        /// </summary>
        [Serializable]
        public class GenerateProbability
        {
            /// <summary>
            /// 移動パターン種別
            /// </summary>
            public EnemyCarMovePatternType MovePatternType;

            /// <summary>
            /// 確率
            /// </summary>
            public int Probability;
        }


        //====================================
        //! 変数（public）
        //====================================

        /// <summary>
        /// 速度
        /// </summary>
        public Param Speed;

        /// <summary>
        /// スケール
        /// </summary>
        public Param Scale;

        /// <summary>
        /// 生成間隔時間（秒）
        /// </summary>
        public Param GenIntervalTimeSec;

        /// <summary>
        /// 移動時間（秒）
        /// </summary>
        public Param MoveTimeSec;

        /// <summary>
        /// 停止時間（秒）
        /// </summary>
        public Param StopTimeSec;

        /// <summary>
        /// ホーミング力レート
        /// </summary>
        public Param HormingPowerRate;

        /// <summary>
        /// ジグザグ移動の角度下限
        /// </summary>
        public float ZigzagMoveAngleMin;

        /// <summary>
        /// ジグザグ移動の角度上限
        /// </summary>
        public float ZigzagMoveAngleMax;

        /// <summary>
        /// プレイヤーに与えるダメージ量
        /// </summary>
        public int Damage;

        /// <summary>
        /// 移動パターンごとの生成確率リスト
        /// </summary>
        public GenerateProbability[] GenerateProbabilityList;
    }
}
