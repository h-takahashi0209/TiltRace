using System;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - ���s�������Ƃ̃C�x���g����
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRaceDistanceEventController : ExMonoBehaviour
    {
        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// ���݂̃��x��
        /// </summary>
        private int mLevel;

        /// <summary>
        /// ���C�t�񕜂�����
        /// </summary>
        private int mRecoveredLifeCount;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// ���x���A�b�v���N�G�X�g
        /// </summary>
        public Action<int> OnReqLevelUp { private get; set; }

        /// <summary>
        /// ���C�t�񕜃��N�G�X�g
        /// </summary>
        public Action<int> OnReqRecoveryLife { private get; set; }


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            OnReqLevelUp        = null;
            OnReqRecoveryLife   = null;
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        public void Setup()
        {
            mLevel              = 0;
            mRecoveredLifeCount = 0;
        }

        /// <summary>
        /// ���s�����X�V
        /// </summary>
        /// <param name="distance"> ���s���� </param>
        public void UpdateDistance(float distance)
        {
            int nextLevel = (int)(distance / TiltRaceSettings.DistanceEvent.LevelUpDistanceInterval);

            // ���Ԋu���s���邲�ƂɃ��x���A�b�v
            if (nextLevel > mLevel)
            {
                mLevel = nextLevel;

                OnReqLevelUp(mLevel);
            }

            int nextRecoveredLifeCount = (int)(distance / TiltRaceSettings.DistanceEvent.RecoveredLifeDistanceInterval);

            // ���Ԋu���s���邲�ƂɃ��C�t��
            if (nextRecoveredLifeCount > mRecoveredLifeCount)
            {
                mRecoveredLifeCount = nextRecoveredLifeCount;

                OnReqRecoveryLife(TiltRaceSettings.DistanceEvent.RecoveredLife);
            }
        }
    }
}
