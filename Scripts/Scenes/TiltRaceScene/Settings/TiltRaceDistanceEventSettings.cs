using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - ���s�������Ƃ̃C�x���g�ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = nameof(TiltRaceDistanceEventSettings), menuName = "ScriptableObjects/Scenes/TiltRaceScene/" + nameof(TiltRaceDistanceEventSettings))]
    public sealed class TiltRaceDistanceEventSettings : ScriptableObject
    {
        //====================================
        //! �ϐ��ipublic�j
        //====================================

        /// <summary>
        /// ���x���A�b�v���鑖�s�����Ԋu
        /// </summary>
        public float LevelUpDistanceInterval;

        /// <summary>
        /// ���C�t���񕜂����鑖�s�����Ԋu
        /// </summary>
        public float RecoveredLifeDistanceInterval;

        /// <summary>
        /// ���C�t�񕜗�
        /// </summary>
        public int RecoveredLife;
    }
}
