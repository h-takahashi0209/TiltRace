using System;


namespace TakahashiH
{
    /// <summary>
    /// タイマー
    /// </summary>
    public sealed class Timer : IDisposable
    {
        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 残り時間（秒）
        /// </summary>
        private float mRemainingTimeSec;

        /// <summary>
        /// 計測中か
        /// </summary>
        private bool mIsActive;

        /// <summary>
        /// 完了時コールバック
        /// </summary>
        private Action mOnComplete;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            mOnComplete = null;
        }

        /// <summary>
        /// 開始
        /// </summary>
        /// <param name="timeSec">       待機時間              </param>
        /// <param name="onComplete">    完了時コールバック    </param>
        public void Begin(float timeSec, Action onComplete)
        {
            mRemainingTimeSec   = timeSec;
            mIsActive           = true;
            mOnComplete         = onComplete;
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public void Pause()
        {
            mIsActive = true;
        }

        /// <summary>
        /// 再開
        /// </summary>
        public void Resume()
        {
            mIsActive = false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="deltaTimeSec"> デルタ時間（秒） </param>
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
