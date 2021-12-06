using System;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �J�E���g�_�E�� UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public sealed class UITiltRaceCountDown : ExMonoBehaviour
    {
        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �A�j���[�^�[
        /// </summary>
        [SerializeField] private Animator Animator;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �������R�[���o�b�N
        /// </summary>
        public Action OnComplete { private get; set; }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �J�n
        /// </summary>
        public void Begin()
        {
            this.SetActive(true);

            Animator.SetTrigger("Once");
        }

        /// <summary>
        /// �A�j���[�V�����C�x���g - ����J�E���g�_�E��
        /// </summary>
        public void AnimationEvent_FirstCountDown()
        {
            SoundManager.PlaySe(SoundDef.TiltRaceScene.Se.Engine .ToString());
            SoundManager.PlaySe(SoundDef.TiltRaceScene.Se.Count  .ToString());
        }

        /// <summary>
        /// �A�j���[�V�����C�x���g - �J�E���g�_�E��
        /// </summary>
        public void AnimationEvent_CountDown()
        {
            SoundManager.PlaySe(SoundDef.TiltRaceScene.Se.Count.ToString());
        }

        /// <summary>
        /// �A�j���[�V�����C�x���g - �������R�[���o�b�N�Ăяo��
        /// </summary>
        public void AnimationEvent_CallOnComplete()
        {
            SoundManager.PlaySe(SoundDef.TiltRaceScene.Se.Start.ToString());

            OnComplete?.Invoke();
        }
    }
}
