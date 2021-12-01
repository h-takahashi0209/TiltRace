using System;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - プレゼンター
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRacePresenter : ExMonoBehaviour
    {
        //====================================
        //! 変数（SerializeField）
        //====================================
        [SerializeField] private UITiltRaceBg           UIBg;
        [SerializeField] private UITiltRaceCountDown    UICountDown;
        [SerializeField] private UITiltRaceDistance     UIDistance;
        [SerializeField] private UITiltRaceLevel        UILevel;
        [SerializeField] private UITiltRaceLife         UILife;
        [SerializeField] private UITiltRacePause        UIPause;
        [SerializeField] private UITiltRaceTelop        UITelop;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// カウントダウン完了時コールバック
        /// </summary>
        public Action OnCompleteCountDown { set { UICountDown.OnComplete = value; } }

        /// <summary>
        /// 一時停止時コールバック
        /// </summary>
        public Action OnPause { set { UIPause.OnPause = value; } }

        /// <summary>
        /// 再開時コールバック
        /// </summary>
        public Action OnResume { set { UIPause.OnResume = value; } }

        /// <summary>
        /// 再挑戦時コールバック
        /// </summary>
        public Action OnRetry { set { UIPause.OnRetry = value; } }

        /// <summary>
        /// 中断時コールバック
        /// </summary>
        public Action OnSuspend { set { UIPause.OnSuspend = value; } }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            UIPause.Initialize();
        }

        /// <summary>
        /// セットアップ
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
        /// カウントダウン開始
        /// </summary>
        public void BeginCountDown()
        {
            UICountDown.Begin();
        }

        /// <summary>
        /// 一時停止ボタン表示
        /// </summary>
        public void ShowPauseButton()
        {
            UIPause.ShowPauseButton();
        }

        /// <summary>
        /// ライフ設定
        /// </summary>
        /// <param name="life"> ライフ </param>
        public void SetLife(int life)
        {
            UILife.SetLife(life);
        }

        /// <summary>
        /// レベル設定
        /// </summary>
        /// <param name="level"> レベル </param>
        public void SetLevel(int level)
        {
            UILevel.SetLevel(level);
        }

        /// <summary>
        /// UI 表示更新
        /// </summary>
        /// <param name="distance"> 走行距離 </param>
        public void UpdateUI(float distance)
        {
            UIBg.Scroll();

            UIDistance.SetDistance(distance);
        }

        /// <summary>
        /// 一時停止 UI を開く
        /// </summary>
        public void OpenPauseUI()
        {
            UIPause.OpenPauseUI();
        }

        /// <summary>
        /// ゲームオーバー時一時停止 UI を開く
        /// </summary>
        public void OpenGameOverPauseUI()
        {
            UIPause.OpenGameOverPauseUI();
        }

        /// <summary>
        /// テロップ再生
        /// </summary>
        /// <param name="telopType"> テロップ種別 </param>
        public void PlayTelop(UITiltRaceTelop.TelopType telopType)
        {
            UITelop.Play(telopType);
        }
    }
}
