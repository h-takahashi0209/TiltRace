using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �w�i UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    public sealed class UITiltRaceBg : ExMonoBehaviour
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// �X�N���[�����x
        /// </summary>
        private const float ScrollSpeed = 1f;


        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �w�i�C���[�W
        /// </summary>
        [SerializeField] private Image UIBgImage;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �X�N���[���i��
        /// </summary>
        private float mScrollProgress;


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        public void Setup()
        {
            mScrollProgress = 0;
        }

        /// <summary>
        /// �X�N���[��
        /// </summary>
        public void Scroll()
        {
            mScrollProgress += Mathf.Clamp01(TimeManager.DeltaTime * ScrollSpeed);

            UIBgImage.material.SetFloat("_ScrollProgress", mScrollProgress);
        }
    }
}
