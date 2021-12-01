using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace
    /// </summary>
    public sealed class TiltRaceScene : SceneBase
    {
        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �v���C���[����
        /// </summary>
        [SerializeField] private TiltRacePlayerCarController PlayerCarController;

        /// <summary>
        /// �G����
        /// </summary>
        [SerializeField] private TiltRaceEnemyCarController EnemyCarController;

        /// <summary>
        /// �A�C�e������
        /// </summary>
        [SerializeField] private TiltRaceItemController ItemController;

        /// <summary>
        /// ���s�������Ƃ̃C�x���g����
        /// </summary>
        [SerializeField] private TiltRaceDistanceEventController DistanceEventController;

        /// <summary>
        /// �v���[���^�[
        /// </summary>
        [SerializeField] private TiltRacePresenter Presenter;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �����蔻��Ǘ�
        /// </summary>
        private TiltRaceCollisionManager mCollisionManager = new TiltRaceCollisionManager();

        /// <summary>
        /// BGM �n���h��
        /// </summary>
        private ISoundHandle mBgmHandle;

        /// <summary>
        /// �ꎞ��~����
        /// </summary>
        private bool mIsPause;


        //====================================
        //! �֐��iSceneBase�j
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
        //! �֐��iprivate�j
        //====================================

        /// <summary>
        /// ������
        /// </summary>
        private void Initialize()
        {
            PlayerCarController .Initialize();
            EnemyCarController  .Initialize();
            ItemController      .Initialize();
            Presenter           .Initialize();
        }

        /// <summary>
        /// �C�x���g�o�^
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
        /// �Z�b�g�A�b�v
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
        /// �v���C���[�̉�
        /// </summary>
        /// <param name="recoveredLife"> �񕜗� </param>
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
        /// �v���C���[�ւ̃_���[�W
        /// </summary>
        /// <param name="damagedLife"> �_���[�W�� </param>
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
        /// �A�C�e���擾
        /// </summary>
        /// <param name="id">          ID              </param>
        /// <param name="itemType">    �A�C�e�����    </param>
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
        /// ���x���A�b�v
        /// </summary>
        /// <param name="level"> ���x�� </param>
        private void LevelUp(int level)
        {
            EnemyCarController.LevelUp();

            Presenter.PlayTelop(UITiltRaceTelop.TelopType.LevelUp);
            Presenter.SetLevel(level);

            PlaySe(SoundDef.TiltRaceScene.Se.Telop);
        }

        /// <summary>
        /// ���C�t��0�ɂȂ������̏���
        /// </summary>
        private void ProcessLifeZero()
        {
            Presenter.OpenGameOverPauseUI();

            Pause();

            SoundManager.Stop(mBgmHandle);

            PlaySe(SoundDef.TiltRaceScene.Se.GameOver);
        }

        /// <summary>
        /// �J�E���g�_�E������������
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
        /// �ꎞ��~
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
        /// �ĊJ
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
        /// �Ē���
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
        /// ���f
        /// </summary>
        private void Suspend()
        {
            UIFade.FadeOut(() =>
            {
                SceneManager.Load(SceneType.TitleScene);
            });
        }

        /// <summary>
        /// SE �Đ�
        /// </summary>
        /// <param name="se"> SE ��� </param>
        private void PlaySe(SoundDef.TiltRaceScene.Se se)
        {
            SoundManager.PlaySe(se.ToString());
        }
    }
}
