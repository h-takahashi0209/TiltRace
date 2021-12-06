using System;


namespace TakahashiH
{
    /// <summary>
    /// �^�C�}�[
    /// </summary>
    public sealed class Timer : IDisposable
    {
        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �c�莞�ԁi�b�j
        /// </summary>
        private float mRemainingTimeSec;

        /// <summary>
        /// �v������
        /// </summary>
        private bool mIsActive;

        /// <summary>
        /// �������R�[���o�b�N
        /// </summary>
        private Action mOnComplete;


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �j��
        /// </summary>
        public void Dispose()
        {
            mOnComplete = null;
        }

        /// <summary>
        /// �J�n
        /// </summary>
        /// <param name="timeSec">       �ҋ@����              </param>
        /// <param name="onComplete">    �������R�[���o�b�N    </param>
        public void Begin(float timeSec, Action onComplete)
        {
            mRemainingTimeSec   = timeSec;
            mIsActive           = true;
            mOnComplete         = onComplete;
        }

        /// <summary>
        /// �ꎞ��~
        /// </summary>
        public void Pause()
        {
            mIsActive = true;
        }

        /// <summary>
        /// �ĊJ
        /// </summary>
        public void Resume()
        {
            mIsActive = false;
        }

        /// <summary>
        /// �X�V
        /// </summary>
        /// <param name="deltaTimeSec"> �f���^���ԁi�b�j </param>
        public void UpdateTimer(float deltaTimeSec)
        {
            if (!mIsActive) {
                return;
            }

            mRemainingTimeSec -= deltaTimeSec;

            if (mRemainingTimeSec <= 0f)
            {
                mIsActive = false;
                mOnComplete?.Invoke();
            }
        }
    }
}
