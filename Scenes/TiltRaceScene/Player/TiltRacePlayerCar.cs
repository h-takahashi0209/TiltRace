using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �v���C���[�̎�
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UITiltRaceCar))]
    public sealed class TiltRacePlayerCar : ExMonoBehaviour, ITiltRacePlayerCarCollision
    {
        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �� UI
        /// </summary>
        [SerializeField] private UITiltRaceCar UICar;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// ���s����
        /// </summary>
        public float Distance { get; set; }

        /// <summary>
        /// ���G��Ԃ�
        /// </summary>
        public bool IsInvincible { get; set; }

        /// <summary>
        /// ���C�t0��
        /// </summary>
        public bool IsLifeZero => Life <= 0;

        /// <summary>
        /// ���C�t
        /// </summary>
        public int Life { get; set; }

        /// <summary>
        /// ���W
        /// </summary>
        public Vector3 Position => UICar.Position;

        /// <summary>
        /// ����
        /// </summary>
        public float Width => UICar.Width;

        /// <summary>
        /// �c��
        /// </summary>
        public float Height => UICar.Height;

        /// <summary>
        /// �ړ����x
        /// </summary>
        public float Speed { get; set; }


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            UICar = GetComponent<UITiltRaceCar>();
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        /// <param name="sprite">      �X�v���C�g    </param>
        /// <param name="position">    ���W          </param>
        /// <param name="scale">       �X�P�[��      </param>
        public void Setup(Sprite sprite, Vector3 position, float scale)
        {
            IsInvincible    = false;
            Life            = TiltRaceSettings.Player.DefLife;
            Distance        = 0;
            Speed           = TiltRaceSettings.Player.DefSpeed;

            UICar.Setup(sprite, position, scale);
        }

        /// <summary>
        /// ���W�ݒ�
        /// </summary>
        /// <param name="position"> ���W </param>
        public void SetPosition(Vector3 position)
        {
            this.SetLocalPosition(position);
        }

        /// <summary>
        /// ���s�����X�V
        /// </summary>
        public void UpdateDistance()
        {
            Distance += TimeManager.DeltaTime * TiltRaceSettings.Player.OneFrameDistance;
        }

        /// <summary>
        /// ���x���Z
        /// </summary>
        /// <param name="addedSpeed"> ���Z���鑬�x </param>
        public void AddSpeed(int addedSpeed)
        {
            Speed = Mathf.Max(Speed + addedSpeed, TiltRaceSettings.Player.SpeedMin);
        }

        /// <summary>
        /// �A�j���[�V�����Đ�
        /// </summary>
        /// <param name="animType"> �A�j���[�V������� </param>
        public void PlayAnimation(UITiltRaceCar.AnimType animType)
        {
            UICar.PlayAnimation(animType);
        }

        /// <summary>
        /// �G�t�F�N�g�Đ�
        /// </summary>
        /// <param name="effectType"> �G�t�F�N�g��� </param>
        public void PlayEffect(UITiltRaceCar.EffectType effectType)
        {
            UICar.PlayEffect(effectType);
        }
    }
}
