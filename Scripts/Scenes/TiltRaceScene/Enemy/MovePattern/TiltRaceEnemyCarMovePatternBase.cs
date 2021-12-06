using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    public interface ITiltRaceEnemyCarMovePattern
    {
        Vector3 MoveVec { get; }

        void Setup(float speed, float moveTimeSec, float stopTimeSec, float hormingPowerRate);
        void UpdateMoveVec(Vector3 myCarPosition, Vector3 playerCarPosition);
        void Dispose();
        void Pause();
        void Resume();
    }

    /// <summary>
    /// TiltRace - �G�̎Ԉړ��p�^�[�����
    /// </summary>
    public abstract class TiltRaceEnemyCarMovePatternBase : ITiltRaceEnemyCarMovePattern
    {
        //====================================
        //! �ϐ��iprotected�j
        //====================================

        /// <summary>
        /// ���x
        /// </summary>
        protected float mSpeed;

        /// <summary>
        /// ���g�̎Ԃ̍��W
        /// </summary>
        protected Vector3 mMyCarPosition;

        /// <summary>
        /// �v���C���[�̎Ԃ̍��W
        /// </summary>
        protected Vector3 mPlayerCarPosition;

        /// <summary>
        /// �ړ����ԁi�b�j
        /// </summary>
        protected float mMoveTimeSec;

        /// <summary>
        /// ��~���ԁi�b�j
        /// </summary>
        protected float mStopTimeSec;

        /// <summary>
        /// �z�[�~���O�̓��[�g
        /// </summary>
        protected float mHormingPowerRate;

        /// <summary>
        /// �^�C�}�[
        /// </summary>
        protected Timer mTimer = new Timer();


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �ړ��x�N�g��
        /// </summary>
        public Vector3 MoveVec { get; protected set; }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        /// <param name="speed">               ���x                  </param>
        /// <param name="moveTimeSec">         �ړ����ԁi�b�j        </param>
        /// <param name="stopTimeSec">         ��~���ԁi�b�j        </param>
        /// <param name="hormingPowerRate">    �z�[�~���O�̓��[�g    </param>
        public void Setup(float speed, float moveTimeSec, float stopTimeSec, float hormingPowerRate)
        {
            mSpeed              = speed;
            mMoveTimeSec        = moveTimeSec;
            mStopTimeSec        = stopTimeSec;
            mHormingPowerRate   = hormingPowerRate;

            DoSetup();
        }

        /// <summary>
        /// �j��
        /// </summary>
        public void Dispose()
        {
            mTimer.Dispose();
        }

        /// <summary>
        /// �ꎞ��~
        /// </summary>
        public void Pause()
        {
            mTimer.Pause();
        }

        /// <summary>
        /// �ĊJ
        /// </summary>
        public void Resume()
        {
            mTimer.Resume();
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �ړ��x�N�g���X�V
        /// </summary>
        /// <param name="myCarPosition">        ���g�̎Ԃ̍��W          </param>
        /// <param name="playerCarPosition">    �v���C���[�̎Ԃ̍��W    </param>
        public void UpdateMoveVec(Vector3 myCarPosition, Vector3 playerCarPosition)
        {
            mMyCarPosition      = myCarPosition;
            mPlayerCarPosition  = playerCarPosition;

            mTimer.UpdateTimer(TimeManager.DeltaTime);

            DoUpdateMoveVec();
        }


        //====================================
        //! �֐��iprotected virtual�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        protected virtual void DoSetup() {}

        /// <summary>
        /// �ړ��x�N�g���X�V
        /// </summary>
        protected virtual void DoUpdateMoveVec() {}
    }
}
