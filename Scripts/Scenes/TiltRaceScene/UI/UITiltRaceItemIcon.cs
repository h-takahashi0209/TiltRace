using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - アイテムアイコン
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UITiltRaceItemIcon : ExMonoBehaviour
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// アイテム画像
        /// </summary>
        [SerializeField] private Image ItemImage;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 横幅
        /// </summary>
        public float Width => rectTransform.sizeDelta.x;

        /// <summary>
        /// 縦幅
        /// </summary>
        public float Height => rectTransform.sizeDelta.y;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            ItemImage = GetComponentInChildren<Image>();
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="sprite"> スプライト </param>
        public void Setup(Sprite sprite)
        {
            ItemImage.sprite = sprite;
        }
    }
}
