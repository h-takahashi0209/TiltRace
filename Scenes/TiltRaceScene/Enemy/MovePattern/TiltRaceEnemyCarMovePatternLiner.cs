using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �G�̎Ԉړ��p�^�[����� - ����
    /// </summary>
    public sealed class TiltRaceEnemyCarMovePatternLiner : TiltRaceEnemyCarMovePatternBase
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
            MoveVec = mDefMoveVec * mSpeed * TimeManager.DeltaTime;
        }
    }
}
