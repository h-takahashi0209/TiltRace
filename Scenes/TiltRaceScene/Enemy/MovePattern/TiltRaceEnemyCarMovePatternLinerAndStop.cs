using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �G�̎Ԉړ��p�^�[����� - ���ނ��~���J��Ԃ�
    /// </summary>
    public sealed class TiltRaceEnemyCarMovePatternLinerAndStop : TiltRaceEnemyCarMovePatternBase
    {
        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// ��ړ��x�N�g��
        /// </summary>
        private Vector3 mDefMoveVec;

        /// <summary>
        /// ��~����
        /// </summary>
        private bool mIsStop;


        //====================================
        //! �֐��iMovePatternBase�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        protected override void DoSetup()
        {
            mIsStop     = false;
            mDefMoveVec = Vector3.down;

            mTimer.Begin(mMoveTimeSec, () => SwitchState());
        }

        /// <summary>
        /// �ړ��x�N�g���X�V
        /// </summary>
        protected override void DoUpdateMoveVec()
        {
            if (mIsStop)
            {
                MoveVec = Vector3.zero;
            }
            else
            {
                MoveVec = mDefMoveVec * mSpeed * TimeManager.DeltaTime;
            }
        }


        //====================================
        //! �֐��iprivate�j
        //====================================

        /// <summary>
        /// �ړ� / ��~�̏�Ԑ؂�ւ�
        /// </summary>
        private void SwitchState()
        {
            mIsStop = !mIsStop;

            float waitTimeSec = mIsStop ? mStopTimeSec : mMoveTimeSec;

            mTimer.Begin(waitTimeSec, () => SwitchState());
        }
    }
}
