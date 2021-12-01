using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �G�̎Ԉړ��p�^�[����� - �W�O�U�O�ړ�
    /// </summary>
    public sealed class TiltRaceEnemyCarMovePatternZigzag : TiltRaceEnemyCarMovePatternBase
    {
        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// ��������
        /// </summary>
        private bool mIsLeft;

        /// <summary>
        /// �ړ��p�x
        /// </summary>
        private Vector3 mMoveAngle;


        //====================================
        //! �֐��iMovePatternBase�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        protected override void DoSetup()
        {
            mIsLeft = Random.Range(0, 1) == 0 ? true : false;

            SwitchDirection();
        }

        /// <summary>
        /// �ړ��x�N�g���X�V
        /// </summary>
        protected override void DoUpdateMoveVec()
        {
            MoveVec = mMoveAngle * mSpeed * TimeManager.DeltaTime;

            var nextFramePosition = mMyCarPosition + MoveVec;

            // ���̃t���[���ł̍��W���ړ��͈͂��z���Ă��邩�H
            bool isSwitchDirection =
            (
                nextFramePosition.x < -TiltRaceSettings.WidthLimit
            ||  nextFramePosition.x >  TiltRaceSettings.WidthLimit
            );

            // �ړ��͈͂��z�����������ς���
            if(isSwitchDirection)
            {
                SwitchDirection();
            }
        }


        //====================================
        //! �֐��iprivate�j
        //====================================

        /// <summary>
        /// �����؂�ւ�
        /// </summary>
        private void SwitchDirection()
        {
            mIsLeft = !mIsLeft;

            var direction   = mIsLeft ? -1f : 1f;
            var angle       = Random.Range(TiltRaceSettings.Enemy.ZigzagMoveAngleMin, TiltRaceSettings.Enemy.ZigzagMoveAngleMax) * direction;
            var moveVec     = Quaternion.Euler(180f, 0f, angle) * Vector3.up;

            mMoveAngle  = moveVec.normalized;
            MoveVec     = mMoveAngle * mSpeed * TimeManager.DeltaTime;
        }
    }
}
