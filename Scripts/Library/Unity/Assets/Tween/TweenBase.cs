using System;
using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// Tween ���
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class TweenBase<T> : ExMonoBehaviour
    {
        //====================================
        //! �ϐ��ipublic�j
        //====================================

        /// <summary>
        /// ���v���ԁi�b�j
        /// </summary>
        public float DurationTimeSec;

        /// <summary>
        /// �x�����ԁi�b�j
        /// </summary>
        public float DelayTimeSec;

        /// <summary>
        /// �J�n�l
        /// </summary>
        public T From;

        /// <summary>
        /// �ڕW�l
        /// </summary>
        public T To;


        //====================================
        //! �ϐ��iprotected�j
        //====================================

        /// <summary>
        /// To -> From ��
        /// </summary>
        protected bool mIsReverse;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �o�ߎ��ԁi�b�j
        /// </summary>
        private float mElapsedTimeSec;


        //====================================
        //! �v���p�e�B�ipublic�j
        //====================================

        /// <summary>
        /// �Đ�����
        /// </summary>
        public bool IsPlaying { private get; set; }

        /// <summary>
        /// �������R�[���o�b�N
        /// </summary>
        public Action OnComplete { private get; set; }


        //====================================
        //! �v���p�e�B�iprotected�j
        //====================================

        /// <summary>
        /// �J�n�l�iIsReverse �K�p�j
        /// </summary>
        protected T FromAppliedReverse => mIsReverse ? To : From;

        /// <summary>
        /// �ڕW�l�iIsReverse �K�p�j
        /// </summary>
        protected T ToAppliedReverse => mIsReverse ? From : To;

        /// <summary>
        /// �i��
        /// TODO : ���`��� �݂̂łȂ��AAnimationCurve ���ɂ���Ԃɂ��Ή�������
        /// </summary>
        protected float Progress => Mathf.Clamp01(mElapsedTimeSec / DurationTimeSec);


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (!IsPlaying) {
                return;
            }

            mElapsedTimeSec += TimeManager.DeltaTime;

            DoUpdate();

            if (mElapsedTimeSec >= DurationTimeSec)
            {
                DoComplete();

                IsPlaying = false;

                OnComplete?.Invoke();
            }
        }

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            DoOnDestroy();

            OnComplete = null;
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �J�n�iFrom -> To�j
        /// </summary>
        public void Begin()
        {
            DoBegin();

            IsPlaying       = true;
            mElapsedTimeSec = -DelayTimeSec;
            mIsReverse      = false;
        }

        /// <summary>
        /// �J�n�iTo -> From�j
        /// </summary>
        public void BeginReverse()
        {
            DoBegin();

            IsPlaying       = true;
            mElapsedTimeSec = -DelayTimeSec;
            mIsReverse      = true;
        }


        //====================================
        //! �֐��iprotected virtual�j
        //====================================

        /// <summary>
        /// �J�n������
        /// </summary>
        protected virtual void DoBegin() {}

        /// <summary>
        /// �X�V����
        /// </summary>
        protected virtual void DoUpdate() {}

        /// <summary>
        /// ����������
        /// </summary>
        protected virtual void DoComplete() {}

        /// <summary>
        /// �j������
        /// </summary>
        protected virtual void DoOnDestroy() {}
    }
}
