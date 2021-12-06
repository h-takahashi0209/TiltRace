using System;
using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �ꎞ��~ UI
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UITiltRacePause : ExMonoBehaviour
    {
        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �ꎞ��~�{�^��
        /// </summary>
        [SerializeField] private UIButton UIPauseButton;

        /// <summary>
        /// �ꎞ��~���
        /// </summary>
        [SerializeField] private GameObject UIPauseScreen;

        /// <summary>
        /// �ĊJ�{�^��
        /// </summary>
        [SerializeField] private UIButton UIResumeButton;

        /// <summary>
        /// �ĊJ�{�^���e�L�X�g
        /// </summary>
        [SerializeField] private Text UIResumeButtonText;

        /// <summary>
        /// ���f�{�^��
        /// </summary>
        [SerializeField] private UIButton UISuspendButton;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �Q�[���I�[�o�[��
        /// </summary>
        private bool mIsGameOver;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �ꎞ��~���R�[���o�b�N
        /// </summary>
        public Action OnPause { private get; set; }

        /// <summary>
        /// �ĊJ���R�[���o�b�N
        /// </summary>
        public Action OnResume { private get; set; }

        /// <summary>
        /// �Ē��펞�R�[���o�b�N
        /// </summary>
        public Action OnRetry { private get; set; }

        /// <summary>
        /// ���f���R�[���o�b�N
        /// </summary>
        public Action OnSuspend { private get; set; }


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

#if UNITY_EDITOR

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (UIPauseScreen.activeInHierarchy)
            {
                if (InputManager.IsKeyDown(KeyCode.P))
                {
                    _OnResume();
                }

                if (InputManager.IsKeyDown(KeyCode.S))
                {
                    _OnSuspend();
                }
            }
            else
            {
                if (InputManager.IsKeyDown(KeyCode.P))
                {
                    _OnPause();
                }
            }
        }

#endif

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            OnPause     = null;
            OnResume    = null;
            OnSuspend   = null;
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// ������
        /// </summary>
        public void Initialize()
        {
            UIPauseButton   .OnClick = () => _OnPause();
            UIResumeButton  .OnClick = () => _OnResume();
            UISuspendButton .OnClick = () => _OnSuspend();
        }

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        public void Setup()
        {
            UIPauseScreen   .SetActive(false);
            UIPauseButton   .SetActive(false);
        }

        /// <summary>
        /// �ꎞ��~�{�^���\��
        /// </summary>
        public void ShowPauseButton()
        {
            UIPauseButton.SetActive(true);
        }

        /// <summary>
        /// �ꎞ��~ UI ���J��
        /// </summary>
        public void OpenPauseUI()
        {
            UIResumeButtonText.text = "�ĊJ";

            UIPauseScreen   .SetActive(true);
            UIPauseButton   .SetActive(false);

            mIsGameOver = false;
        }

        /// <summary>
        /// �Q�[���I�[�o�[���ꎞ��~ UI ���J��
        /// </summary>
        public void OpenGameOverPauseUI()
        {
            UIResumeButtonText.text = "�Ē���";

            UIPauseScreen   .SetActive(true);
            UIPauseButton   .SetActive(false);

            mIsGameOver = true;
        }


        //====================================
        //! �֐��iprivate event�j
        //====================================

        /// <summary>
        /// �ꎞ��~��
        /// </summary>
        private void _OnPause()
        {
            SoundManager.PlaySe(SoundDef.ResidentScene.Se.ButtonDecide.ToString());

            OpenPauseUI();
            OnPause();
        }

        /// <summary>
        /// �ĊJ��
        /// </summary>
        private void _OnResume()
        {
            SoundManager.PlaySe(SoundDef.ResidentScene.Se.ButtonDecide.ToString());

            UIPauseScreen   .SetActive(false);
            UIPauseButton   .SetActive(true);

            if (mIsGameOver)
            {
                OnRetry();
            }
            else
            {
                OnResume();
            }
        }

        /// <summary>
        /// ���f��
        /// </summary>
        private void _OnSuspend()
        {
            SoundManager.PlaySe(SoundDef.ResidentScene.Se.ButtonCancel.ToString());

            UIPauseScreen.SetActive(false);

            OnSuspend();
        }
    }
}
