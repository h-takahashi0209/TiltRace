using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �G�̎Ԉړ��p�^�[����� - �Ǐ]
    /// </summary>
    public sealed class TiltRaceEnemyCarMovePatternHorming : TiltRaceEnemyCarMovePatternBase
    {
        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// ��ړ��x�N�g��
        /// </summary>
        private Vector3 mDefMoveVec;


        //====================================
        //! �֐��iMovePatternBase�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        protected override void DoSetup()
        {
            mDefMoveVec = Vector3.down;
        }

        /// <summary>
        /// �ړ��x�N�g���X�V
        /// </summary>
        protected override void DoUpdateMoveVec()
        {
            // �v���C���[�Ɍ��������ɗ����悤�A�������̃x�N�g���𑫂����킹��
            var targetVec = (mPlayerCarPosition - mMyCarPosition).normalized * mHormingPowerRate + mDefMoveVec;

            MoveVec = targetVec * mSpeed * TimeManager.DeltaTime;
        }
    }
}
