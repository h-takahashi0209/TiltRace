using System;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �A�C�e���ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = nameof(TiltRaceItemSettings), menuName = "ScriptableObjects/Scenes/TiltRaceScene/" + nameof(TiltRaceItemSettings))]
    public sealed class TiltRaceItemSettings : ScriptableObject
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// �A�C�e����ʂ��Ƃ̐����m��
        /// </summary>
        [Serializable]
        public class GenerateProbability
        {
            /// <summary>
            /// �A�C�e�����
            /// </summary>
            public ItemType ItemType;

            /// <summary>
            /// �m��
            /// </summary>
            public int Probability;
        }


        //====================================
        //! �ϐ��ipublic�j
        //====================================

        /// <summary>
        /// ��������ԁi�b�j
        /// </summary>
        public float BaseGenerateTimeSec;

        /// <summary>
        /// �������Ԕ͈́i�b�j
        /// </summary>
        public float GenerateTimeRangeSec;

        /// <summary>
        /// �ړ����x
        /// </summary>
        public float Speed;

        /// <summary>
        /// �v���C���[�̃��C�t�񕜗�
        /// </summary>
        public int RecoveredPlayerLife;

        /// <summary>
        /// �v���C���[�̑��x���Z��
        /// </summary>
        public int AddedPlayerSpeed;

        /// <summary>
        /// �A�C�e����ʂ��Ƃ̐����m�����X�g
        /// </summary>
        public GenerateProbability[] GenerateProbabilityList;
    }
}
