using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace
    /// </summary>
    public sealed class TiltRaceScene : SceneBase
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// プレイヤー制御
        /// </summary>
        [SerializeField] private TiltRacePlayerCarController PlayerCarController;

        /// <summary>
        /// 敵制御
        /// </summary>
        [SerializeField] private TiltRaceEnemyCarController EnemyCarController;

        /// <summary>
        /// アイテム制御
        /// </summary>
        [SerializeField] private TiltRaceItemController ItemController;

        /// <summary>
        /// 走行距離ごとのイベント制御
        /// </summary>
        [SerializeField] private TiltRaceDistanceEventController DistanceEventController;

        /// <summary>
        /// プレゼンター
        /// </summary>
        [SerializeField] private TiltRacePresenter Presenter;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 当たり判定管理
        /// </summary>
        private TiltRaceCollisionManager mCollisionManager = new TiltRaceCollisionManager();

        /// <summary>
        /// BGM ハンドル
        /// </summary>
        private ISoundHandle mBgmHandle;

        /// <summary>
        /// 一時停止中か
        /// </summary>
        private bool mIsPause;


        //====================================
        //! 関数（SceneBase）
        //====================================

        /// <summary>
        /// DoStart
        /// </summary>
        protected override void DoStart()
        {
            mIsPause = true;

            TiltRaceSettings.Load();
            SoundManager.LoadSceneSoundData(SceneType.TiltRaceScene);

            Initialize();
            RegisterEvent();
            Setup();

            UIFade.FadeIn(() =>
            {
                Presenter.BeginCountDown();
            });
        }

        /// <summary>
        /// DoUpdate
        /// </summary>
        protected override void DoUpdate()
        {
            if (mIsPause) {
                return;
            }

            PlayerCarController     .UpdateCar      (EnemyCarController.ActiveCarCollisionList);
            EnemyCarController      .UpdatePosition (PlayerCarController.Position);
            ItemController          .UpdatePosition ();
            mCollisionManager       .CheckHit       (PlayerCarController.Collision, EnemyCarController.ActiveCarCollisionList, ItemController.ActiveItemCollisionList);
            DistanceEventController .UpdateDistance (PlayerCarController.Distance);
            Presenter               .UpdateUI       (PlayerCarController.Distance);
        }

        /// <summary>
        /// DoOnDestroy
        /// </summary>
        protected override void DoOnDestroy()
        {
            TiltRaceSettings.Dispose();

            SoundManager.DisposeSceneSoundData();

            mCollisionManager.Dispose();
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 初期化
        /// </summary>
        private void Initialize()
        {
            PlayerCarController .Initialize();
            EnemyCarController  .Initialize();
            ItemController      .Initialize();
            Presenter           .Initialize();
        }

        /// <summary>
        /// イベント登録
        /// </summary>
        private void RegisterEvent()
        {
            DistanceEventController .OnReqLevelUp           = level             => LevelUp(level);
            DistanceEventController .OnReqRecoveryLife      = recLife           => RecoveryPlayer(recLife);
            mCollisionManager       .OnHitEnemyCar          = damage            => DamagePlayer(damage);
            mCollisionManager       .OnHitItem              = (id, itemType)    => GetItem(id, itemType);
            Presenter               .OnCompleteCountDown    = ()                => CompleteCountDown();
            Presenter               .OnPause                = ()                => Pause();
            Presenter               .OnResume               = ()                => Resume();
            Presenter               .OnRetry                = ()                => Retry();
            Presenter               .OnSuspend              = ()                => Suspend();
        }

        /// <summary>
        /// セットアップ
        /// </summary>
        private void Setup()
        {
            PlayerCarController     .Setup();
            EnemyCarController      .Setup();
            ItemController          .Setup();
            DistanceEventController .Setup();
            Presenter               .Setup();
        }

        /// <summary>
        /// プレイヤーの回復
        /// </summary>
        /// <param name="recoveredLife"> 回復量 </param>
        private void RecoveryPlayer(int recoveredLife)
        {
            if (recoveredLife <= 0) {
                return;
            }

            int afterLife = Mathf.Min(PlayerCarController.Life + recoveredLife, TiltRaceSettings.Player.MaxLife);

            PlayerCarController.Recovery(afterLife);

            Presenter.SetLife(afterLife);
        }

        /// <summary>
        /// プレイヤーへのダメージ
        /// </summary>
        /// <param name="damagedLife"> ダメージ量 </param>
        private void DamagePlayer(int damagedLife)
        {
            if (damagedLife <= 0) {
                return;
            }

            int afterLife = Mathf.Max(PlayerCarController.Life - damagedLife, 0);

            PlayerCarController.Damage(afterLife);

            Presenter.SetLife(afterLife);

            if (PlayerCarController.IsLifeZero)
            {
                ProcessLifeZero();
                return;
            }

            PlaySe(SoundDef.TiltRaceScene.Se.Hit);
        }

        /// <summary>
        /// アイテム取得
        /// </summary>
        /// <param name="id">          ID              </param>
        /// <param name="itemType">    アイテム種別    </param>
        private void GetItem(int id, ItemType itemType)
        {
            ItemController.SetDeactiveItem(id);

            PlayerCarController.PlayGetItemEffect(itemType);

            switch(itemType)
            {
                case ItemType.RecoveryLife:
                    {
                        RecoveryPlayer(TiltRaceSettings.Item.RecoveredPlayerLife);

                        PlaySe(SoundDef.TiltRaceScene.Se.RecoveryLife);
                    }
                    break;

                case ItemType.SpeedUp:
                    {
                        PlayerCarController.AddSpeed(TiltRaceSettings.Item.AddedPlayerSpeed);

                        PlaySe(SoundDef.TiltRaceScene.Se.SpeedUp);
                    }
                    break;

                case ItemType.SpeedDown:
                    {
                        PlayerCarController.AddSpeed(-TiltRaceSettings.Item.AddedPlayerSpeed);

                        PlaySe(SoundDef.TiltRaceScene.Se.SpeedDown);
                    }
                    break;
            }
        }

        /// <summary>
        /// レベルアップ
        /// </summary>
        /// <param name="level"> レベル </param>
        private void LevelUp(int level)
        {
            EnemyCarController.LevelUp();

            Presenter.PlayTelop(UITiltRaceTelop.TelopType.LevelUp);
            Presenter.SetLevel(level);

            PlaySe(SoundDef.TiltRaceScene.Se.Telop);
        }

        /// <summary>
        /// ライフが0になった時の処理
        /// </summary>
        private void ProcessLifeZero()
        {
            Presenter.OpenGameOverPauseUI();

            Pause();

            SoundManager.Stop(mBgmHandle);

            PlaySe(SoundDef.TiltRaceScene.Se.GameOver);
        }

        /// <summary>
        /// カウントダウン完了時処理
        /// </summary>
        private void CompleteCountDown()
        {
            mIsPause = false;

            EnemyCarController  .Begin();
            ItemController      .Begin();

            Presenter.ShowPauseButton();

            mBgmHandle = SoundManager.PlayBgm(SoundDef.TiltRaceScene.Bgm.Race.ToString());
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        private void Pause()
        {
            mIsPause = true;

            PlayerCarController .Pause();
            EnemyCarController  .Pause();
            ItemController      .Pause();

            SoundManager.Pause(mBgmHandle);
        }

        /// <summary>
        /// 再開
        /// </summary>
        private void Resume()
        {
            mIsPause = false;

            PlayerCarController .Resume();
            EnemyCarController  .Resume();
            ItemController      .Resume();

            SoundManager.Resume(mBgmHandle);
        }

        /// <summary>
        /// 再挑戦
        /// </summary>
        private void Retry()
        {
            mIsPause = true;

            var task = new StepTask();

            task.Push(onNext => UIFade.FadeOut(0.2f, onNext));

            task.Push(() => Setup());

            task.Push(onNext => UIFade.FadeIn(0.2f, onNext));

            task.Process(() => Presenter.BeginCountDown());
        }

        /// <summary>
        /// 中断
        /// </summary>
        private void Suspend()
        {
            UIFade.FadeOut(() =>
            {
                SceneManager.Load(SceneType.TitleScene);
            });
        }

        /// <summary>
        /// SE 再生
        /// </summary>
        /// <param name="se"> SE 種別 </param>
        private void PlaySe(SoundDef.TiltRaceScene.Se se)
        {
            SoundManager.PlaySe(se.ToString());
        }
    }
}
