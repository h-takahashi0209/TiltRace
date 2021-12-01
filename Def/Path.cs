
namespace TakahashiH
{
    /// <summary>
    /// パス定義用クラス
    /// </summary>
    public static class Path
    {
        /// <summary>
        /// 共通
        /// </summary>
        public static class Common
        {
            /// <summary>
            /// サウンドデータ
            /// </summary>
            public static string SoundData = "ScriptableObjects/System/SoundData/{0}";
        }

        /// <summary>
        /// シーン別
        /// </summary>
        public static class Scenes
        {
            #region TiltRaceScene

            /// <summary>
            /// TiltRaceScene
            /// </summary>
            public static class TiltRaceScene
            {
                /// <summary>
                /// 車画像
                /// </summary>
                public static string CarImage = "Textures/Scenes/TiltRaceScene/Car/{0}";

                /// <summary>
                /// アイテム画像
                /// </summary>
                public static string ItemImage = "Textures/Scenes/TiltRaceScene/Item/{0}";

                /// <summary>
                /// 設定ファイル
                /// </summary>
                public static string Settings = "ScriptableObjects/Scenes/TiltRaceScene/TiltRaceSettings";
            }

            #endregion
        }
    }
}
