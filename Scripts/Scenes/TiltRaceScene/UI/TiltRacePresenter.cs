using System;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �v���[���^�[
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRacePresenter : ExMonoBehaviour
    {
        //====================================
        //! �ϐ��iSerializeField�j
        //====================================
        [SerializeField] private UITiltRaceBg           UIBg;
        [SerializeField] private UITiltRaceCountDown    UICountDown;
        [SerializeField] private UITiltRaceDistance     UIDistance;
        [SerializeField] private UITiltRaceLevel        UILevel;
        [SerializeField] private UITiltRaceLife         UILife;
        [SerializeField] private UITiltRacePause        UIPause;
        [SerializeField] private UITiltRaceTelop        UITelop;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �J�E���g�_�E���������R�[���o�b�N
        /// </summary>
        public Action OnCompleteCountDown { set { UICountDown.OnComplete = value; } }

        /// <summary>
        /// �ꎞ��~���R�[���o�b�N
        /// </summary>
        public Action OnPause { set { UIPause.OnPause = value; } }

        /// <summary>
        /// �ĊJ���R�[���o�b�N
        /// </summary>
        public Action OnResume { set { UIPause.OnResume = value; } }

        /// <summary>
        /// �Ē��펞�R�[���o�b�N
        /// </summary>
        public Action OnRetry { set { UIPause.OnRetry = value; } }

        /// <summary>
        /// ���f���R�[���o�b�N
        /// </summary>
        public Action OnSuspend { set { UIPause.OnSuspend = value; } }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// ������
        /// </summary>
        public void Initialize()
        {
            UIPause.Initialize();
        }

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        public void Setup()
        {
            UIBg        .Setup();
            UIDistance  .Setup();
            UILevel     .Setup();
            UILife      .Setup();
            UIPause     .Setup();
        }

        /// <summary>
        /// �J�E���g�_�E���J�n
        /// </summary>
        public void BeginCountDown()
        {
            UICountDown.Begin();
        }

        /// <summary>
        /// �ꎞ��~�{�^���\��
        /// </summary>
        public void ShowPauseButton()
        {
            UIPause.ShowPauseButton();
        }

        /// <summary>
        /// ���C�t�ݒ�
        /// </summary>
        /// <param name="life"> ���C�t </param>
        public void SetLife(int life)
        {
            UILife.SetLife(life);
        }

        /// <summary>
        /// ���x���ݒ�
        /// </summary>
        /// <param name="level"> ���x�� </param>
        public void SetLevel(int level)
        {
            UILevel.SetLevel(level);
        }

        /// <summary>
        /// UI �\���X�V
        /// </summary>
        /// <param name="distance"> ���s���� </param>
        public void UpdateUI(float distance)
        {
            UIBg.Scroll();

            UIDistance.SetDistance(distance);
        }

        /// <summary>
        /// �ꎞ��~ UI ���J��
        /// </summary>
        public void OpenPauseUI()
        {
            UIPause.OpenPauseUI();
        }

        /// <summary>
        /// �Q�[���I�[�o�[���ꎞ��~ UI ���J��
        /// </summary>
        public void OpenGameOverPauseUI()
        {
            UIPause.OpenGameOverPauseUI();
        }

        /// <summary>
        /// �e���b�v�Đ�
        /// </summary>
        /// <param name="telopType"> �e���b�v��� </param>
        public void PlayTelop(UITiltRaceTelop.TelopType telopType)
        {
            UITelop.Play(telopType);
        }
    }
}
