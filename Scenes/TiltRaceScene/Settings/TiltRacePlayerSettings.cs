using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �v���C���[�ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = nameof(TiltRacePlayerSettings), menuName = "ScriptableObjects/Scenes/TiltRaceScene/" + nameof(TiltRacePlayerSettings))]
    public sealed class TiltRacePlayerSettings : ScriptableObject
    {
        //====================================
        //! �ϐ��ipublic�j
        //====================================

        /// <summary>
        /// �������C�t
        /// </summary>
        public int DefLife;

        /// <summary>
        /// �ő僉�C�t
        /// </summary>
        public int MaxLife;

        /// <summary>
        /// ��_���[�W���̖��G���ԁi�b�j
        /// </summary>
        public float DamageInvincibleTimeSec;

        /// <summary>
        /// 1�t���[�����Ƃ̑��s����
        /// </summary>
        public float OneFrameDistance;

        /// <summary>
        /// �Ԃ̐F
        /// </summary>
        public CarColor CarColor;

        /// <summary>
        /// ��ړ����x
        /// </summary>
        public float DefSpeed;

        /// <summary>
        /// �ړ����x����
        /// </summary>
        public float SpeedMin;
    }
}
