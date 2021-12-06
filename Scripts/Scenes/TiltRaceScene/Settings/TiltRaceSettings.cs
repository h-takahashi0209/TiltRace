using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 設定
    /// </summary>
    [CreateAssetMenu(fileName = nameof(TiltRaceSettings), menuName = "ScriptableObjects/Scenes/TiltRaceScene/" + nameof(TiltRaceSettings))]
    public sealed class TiltRaceSettings : ScriptableObject
    {
        //====================================
        //! 変数（private static）
        //====================================

        /// <summary>
        /// インスタンス
        /// </summary>
        private static TiltRaceSettings msInstance;


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// プレイヤー関連
        /// </summary>
        [SerializeField] private TiltRacePlayerSettings mPlayer;

        /// <summary>
        /// 敵関連
        /// </summary>
        [SerializeField] private TiltRaceEnemySettings mEnemy;

        /// <summary>
        /// アイテム関連
        /// </summary>
        [SerializeField] private TiltRaceItemSettings mItem;

        /// <summary>
        /// 走行距離ごとのイベント関連
        /// </summary>
        [SerializeField] private TiltRaceDistanceEventSettings mDistanceEvent;

        /// <summary>
        /// 縦の移動領域上限
        /// </summary>
        [SerializeField] private float mHeightLimit;

        /// <summary>
        /// 横の移動領域上限
        /// </summary>
        [SerializeField] private float mWidthLimit;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// プレイヤー関連
        /// </summary>
        public static TiltRacePlayerSettings Player => msInstance.mPlayer;

        /// <summary>
        /// 敵関連
        /// </summary>
        public static TiltRaceEnemySettings Enemy => msInstance.mEnemy;

        /// <summary>
        /// アイテム関連
        /// </summary>
        public static TiltRaceItemSettings Item => msInstance.mItem;

        /// <summary>
        /// 走行距離ごとのイベント関連
        /// </summary>
        public static TiltRaceDistanceEventSettings DistanceEvent => msInstance.mDistanceEvent;

        /// <summary>
        /// 縦の移動領域上限
        /// </summary>
        public static float HeightLimit => msInstance.mHeightLimit;

        /// <summary>
        /// 横の移動領域上限
        /// </summary>
        public static float WidthLimit => msInstance.mWidthLimit;


        //====================================
        //! 関数（public static）
        //====================================

        /// <summary>
        /// 読み込み
        /// </summary>
        public static void Load()
        {
            msInstance = Resources.Load<TiltRaceSettings>(Path.Scenes.TiltRaceScene.Settings);
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public static void Dispose()
        {
            msInstance = null;
        }
    }
}
