using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �A�C�e���A�C�R��
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UITiltRaceItemIcon : ExMonoBehaviour
    {
        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �A�C�e���摜
        /// </summary>
        [SerializeField] private Image ItemImage;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// ����
        /// </summary>
        public float Width => rectTransform.sizeDelta.x;

        /// <summary>
        /// �c��
        /// </summary>
        public float Height => rectTransform.sizeDelta.y;


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            ItemImage = GetComponentInChildren<Image>();
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        /// <param name="sprite"> �X�v���C�g </param>
        public void Setup(Sprite sprite)
        {
            ItemImage.sprite = sprite;
        }
    }
}
