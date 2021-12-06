using System;
using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 一時停止 UI
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UITiltRacePause : ExMonoBehaviour
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 一時停止ボタン
        /// </summary>
        [SerializeField] private UIButton UIPauseButton;

        /// <summary>
        /// 一時停止画面
        /// </summary>
        [SerializeField] private GameObject UIPauseScreen;

        /// <summary>
        /// 再開ボタン
        /// </summary>
        [SerializeField] private UIButton UIResumeButton;

        /// <summary>
        /// 再開ボタンテキスト
        /// </summary>
        [SerializeField] private Text UIResumeButtonText;

        /// <summary>
        /// 中断ボタン
        /// </summary>
        [SerializeField] private UIButton UISuspendButton;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// ゲームオーバーか
        /// </summary>
        private bool mIsGameOver;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 一時停止時コールバック
        /// </summary>
        public Action OnPause { private get; set; }

        /// <summary>
        /// 再開時コールバック
        /// </summary>
        public Action OnResume { private get; set; }

        /// <summary>
        /// 再挑戦時コールバック
        /// </summary>
        public Action OnRetry { private get; set; }

        /// <summary>
        /// 中断時コールバック
        /// </summary>
        public Action OnSuspend { private get; set; }


        //====================================
        //! 関数（MonoBehaviour）
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
        //! 関数（public）
        //====================================

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            UIPauseButton   .OnClick = () => _OnPause();
            UIResumeButton  .OnClick = () => _OnResume();
            UISuspendButton .OnClick = () => _OnSuspend();
        }

        /// <summary>
        /// セットアップ
        /// </summary>
        public void Setup()
        {
            UIPauseScreen   .SetActive(false);
            UIPauseButton   .SetActive(false);
        }

        /// <summary>
        /// 一時停止ボタン表示
        /// </summary>
        public void ShowPauseButton()
        {
            UIPauseButton.SetActive(true);
        }

        /// <summary>
        /// 一時停止 UI を開く
        /// </summary>
        public void OpenPauseUI()
        {
            UIResumeButtonText.text = "再開";

            UIPauseScreen   .SetActive(true);
            UIPauseButton   .SetActive(false);

            mIsGameOver = false;
        }

        /// <summary>
        /// ゲームオーバー時一時停止 UI を開く
        /// </summary>
        public void OpenGameOverPauseUI()
        {
            UIResumeButtonText.text = "再挑戦";

            UIPauseScreen   .SetActive(true);
            UIPauseButton   .SetActive(false);

            mIsGameOver = true;
        }


        //====================================
        //! 関数（private event）
        //====================================

        /// <summary>
        /// 一時停止時
        /// </summary>
        private void _OnPause()
        {
            SoundManager.PlaySe(SoundDef.ResidentScene.Se.ButtonDecide.ToString());

            OpenPauseUI();
            OnPause();
        }

        /// <summary>
        /// 再開時
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
        /// 中断時
        /// </summary>
        private void _OnSuspend()
        {
            SoundManager.PlaySe(SoundDef.ResidentScene.Se.ButtonCancel.ToString());

            UIPauseScreen.SetActive(false);

            OnSuspend();
        }
    }
}
