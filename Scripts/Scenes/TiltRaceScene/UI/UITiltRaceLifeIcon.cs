using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �c���C�t�A�C�R��
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Animator))]
    public sealed class UITiltRaceLifeIcon : ExMonoBehaviour
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// �A�j���[�V�������
        /// </summary>
        public enum AnimType
        {
            Enable  ,
            Disable ,
            Show    ,
            Hide    ,
        }


        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �A�j���[�^�[
        /// </summary>
        [SerializeField] private Animator Animator;


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            Animator = GetComponent<Animator>();
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        /// <param name="isShow"> �\�����邩 </param>
        public void Setup(bool isShow)
        {
            var animType = isShow ? AnimType.Enable : AnimType.Disable;

            PlayAnimation(animType);
        }

        /// <summary>
        /// �A�j���[�V�����Đ�
        /// </summary>
        /// <param name="animType"> �A�j���[�V������� </param>
        public void PlayAnimation(AnimType animType)
        {
            Animator.Play(animType.ToString());
        }
    }
}
