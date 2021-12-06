using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - ���g�̎Ԑ���
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRacePlayerCarController : ExMonoBehaviour
    {
        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �v���C���[�̎�
        /// </summary>
        [SerializeField] private TiltRacePlayerCar Car;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �������W
        /// </summary>
        private Vector3 mDefPosition;

        /// <summary>
        /// �^�C�}�[
        /// </summary>
        private Timer mTimer = new Timer();


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// ���s����
        /// </summary>
        public float Distance => Car.Distance;

        /// <summary>
        /// ���W
        /// </summary>
        public Vector3 Position => Car.Position;

        /// <summary>
        /// ���C�t
        /// </summary>
        public int Life => Car.Life;

        /// <summary>
        /// ���C�t0��
        /// </summary>
        public bool IsLifeZero => Car.IsLifeZero;

        /// <summary>
        /// �����蔻��
        /// </summary>
        public ITiltRacePlayerCarCollision Collision => Car;


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            mTimer.Dispose();
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// ������
        /// </summary>
        public void Initialize()
        {
            mDefPosition = Car.Position;
        }

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        public void Setup()
        {
            var sprite = Resources.Load<Sprite>(Path.Scenes.TiltRaceScene.CarImage.Format(CarColor.Green));

            Car.Setup(sprite, mDefPosition, 1f);
            Car.PlayAnimation(UITiltRaceCar.AnimType.Wait);
        }

        /// <summary>
        /// �Ԃ̏�ԍX�V
        /// </summary>
        /// <param name="enemyActiveCarCollisionList"> �G�̃A�N�e�B�u�ȎԂ̓����蔻��f�[�^���X�g </param>
        public void UpdateCar(IReadOnlyList<ITiltRaceCollision> enemyActiveCarCollisionList)
        {
            Car.UpdateDistance();

            mTimer.UpdateTimer(TimeManager.DeltaTime);

            Move();
        }

        /// <summary>
        /// ��
        /// </summary>
        /// <param name="life"> �񕜌�̃��C�t </param>
        public void Recovery(int life)
        {
            if (life == Car.Life) {
                return;
            }

            Car.Life = life;

            Car.PlayEffect(UITiltRaceCar.EffectType.RecoveryLife);
        }

        /// <summary>
        /// ��_���[�W
        /// </summary>
        /// <param name="life"> ��_���[�W��̃��C�t </param>
        public void Damage(int life)
        {
            if (life == Car.Life) {
                return;
            }

            Car.Life = life;

            Car.PlayEffect(UITiltRaceCar.EffectType.Hit);

            if (Car.IsLifeZero)
            {
                Car.PlayAnimation(UITiltRaceCar.AnimType.Bomb);
                return;
            }

            BeginInvincible();
        }

        /// <summary>
        /// �A�C�e���擾�G�t�F�N�g�Đ�
        /// </summary>
        /// <param name="itemType"> �A�C�e����� </param>
        public void PlayGetItemEffect(ItemType itemType)
        {
            var effectType = itemType switch
            {
                ItemType.RecoveryLife  => UITiltRaceCar.EffectType.RecoveryLife ,
                ItemType.SpeedUp       => UITiltRaceCar.EffectType.SpeedUp      ,
                ItemType.SpeedDown     => UITiltRaceCar.EffectType.SpeedDown    ,
                _                      => UITiltRaceCar.EffectType.None         ,
            };

            Car.PlayEffect(effectType);
        }

        /// <summary>
        /// �ړ����x���Z
        /// </summary>
        /// <param name="addedSpeed"> ���Z���鑬�x </param>
        public void AddSpeed(int addedSpeed)
        {
            Car.AddSpeed(addedSpeed);
        }

        /// <summary>
        /// �ꎞ��~
        /// </summary>
        public void Pause()
        {
            mTimer.Pause();
        }

        /// <summary>
        /// �ĊJ
        /// </summary>
        public void Resume()
        {
            mTimer.Resume();
        }


        //====================================
        //! �֐��iprivate�j
        //====================================

        /// <summary>
        /// �ړ�����
        /// </summary>
        private void Move()
        {
            if (Car.IsLifeZero) {
                return;
            }

            var position = Car.Position + TiltRaceInputManager.GetInputVec(Car.Speed);

            // ��
            if (position.y > TiltRaceSettings.HeightLimit)
            {
                position.y = TiltRaceSettings.HeightLimit;
            }

            // ��
            if (position.y < -TiltRaceSettings.HeightLimit)
            {
                position.y = -TiltRaceSettings.HeightLimit;
            }

            // ��
            if (position.x < -TiltRaceSettings.WidthLimit)
            {
                position.x = -TiltRaceSettings.WidthLimit;
            }

            // �E
            if (position.x > TiltRaceSettings.WidthLimit)
            {
                position.x = TiltRaceSettings.WidthLimit;
            }

            Car.SetPosition(position);
        }

        /// <summary>
        /// ���G��ԊJ�n
        /// </summary>
        private void BeginInvincible()
        {
            Car.IsInvincible = true;

            Car.PlayAnimation(UITiltRaceCar.AnimType.Invincible);

            mTimer.Begin(TiltRaceSettings.Player.DamageInvincibleTimeSec, () =>
            {
                Car.IsInvincible = false;

                Car.PlayAnimation(UITiltRaceCar.AnimType.Wait);
            });
        }
    }
}
