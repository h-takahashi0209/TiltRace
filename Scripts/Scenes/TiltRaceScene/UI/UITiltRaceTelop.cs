using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �e���b�v UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public sealed class UITiltRaceTelop : ExMonoBehaviour
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// �e���b�v���
        /// </summary>
        public enum TelopType
        {
            LevelUp
        }


        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �A�j���[�^�[
        /// </summary>
        [SerializeField] private Animator Animator;


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �Đ�
        /// </summary>
        /// <param name="telopType"> �e���b�v��� </param>
        public void Play(TelopType telopType)
        {
            Animator.Play(telopType.ToString());
        }
    }
}
