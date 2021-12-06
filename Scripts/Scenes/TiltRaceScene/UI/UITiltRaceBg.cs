using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 背景 UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    public sealed class UITiltRaceBg : ExMonoBehaviour
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// スクロール速度
        /// </summary>
        private const float ScrollSpeed = 1f;


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 背景イメージ
        /// </summary>
        [SerializeField] private Image UIBgImage;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// スクロール進捗
        /// </summary>
        private float mScrollProgress;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        public void Setup()
        {
            mScrollProgress = 0;
        }

        /// <summary>
        /// スクロール
        /// </summary>
        public void Scroll()
        {
            mScrollProgress += Mathf.Clamp01(TimeManager.DeltaTime * ScrollSpeed);

            UIBgImage.material.SetFloat("_ScrollProgress", mScrollProgress);
        }
    }
}
