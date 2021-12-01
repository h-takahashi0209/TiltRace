using System;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �G�ݒ�
    /// </summary>
    [CreateAssetMenu(fileName = nameof(TiltRaceEnemySettings), menuName = "ScriptableObjects/Scenes/TiltRaceScene/" + nameof(TiltRaceEnemySettings))]
    public sealed class TiltRaceEnemySettings : ScriptableObject
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// �p�����[�^
        /// </summary>
        [Serializable]
        public class Param
        {
            /// <summary>
            /// �����l
            /// </summary>
            public float Def;

            /// <summary>
            /// �͈�
            /// </summary>
            public float Range;

            /// <summary>
            /// ���x���A�b�v���̉��Z�l
            /// </summary>
            public float LevelUp;

            /// <summary>
            /// ����
            /// </summary>
            public float Min;
        }

        /// <summary>
        /// �ړ��p�^�[�����Ƃ̐����m��
        /// </summary>
        [Serializable]
        public class GenerateProbability
        {
            /// <summary>
            /// �ړ��p�^�[�����
            /// </summary>
            public EnemyCarMovePatternType MovePatternType;

            /// <summary>
            /// �m��
            /// </summary>
            public int Probability;
        }


        //====================================
        //! �ϐ��ipublic�j
        //====================================

        /// <summary>
        /// ���x
        /// </summary>
        public Param Speed;

        /// <summary>
        /// �X�P�[��
        /// </summary>
        public Param Scale;

        /// <summary>
        /// �����Ԋu���ԁi�b�j
        /// </summary>
        public Param GenIntervalTimeSec;

        /// <summary>
        /// �ړ����ԁi�b�j
        /// </summary>
        public Param MoveTimeSec;

        /// <summary>
        /// ��~���ԁi�b�j
        /// </summary>
        public Param StopTimeSec;

        /// <summary>
        /// �z�[�~���O�̓��[�g
        /// </summary>
        public Param HormingPowerRate;

        /// <summary>
        /// �W�O�U�O�ړ��̊p�x����
        /// </summary>
        public float ZigzagMoveAngleMin;

        /// <summary>
        /// �W�O�U�O�ړ��̊p�x���
        /// </summary>
        public float ZigzagMoveAngleMax;

        /// <summary>
        /// �v���C���[�ɗ^����_���[�W��
        /// </summary>
        public int Damage;

        /// <summary>
        /// �ړ��p�^�[�����Ƃ̐����m�����X�g
        /// </summary>
        public GenerateProbability[] GenerateProbabilityList;
    }
}
