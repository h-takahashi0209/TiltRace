using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace TakahashiH
{
    /// <summary>
    /// �󂯎�����^�X�N�𓯊���������
    /// </summary>
    public sealed class StepTask
    {
        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �^�X�N�L���[
        /// </summary>
        private Queue<Action<Action>> mTaskQueue = new Queue<Action<Action>>();


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �^�X�N�ǉ��i�������R�[���o�b�N�����j
        /// </summary>
        /// <param name="action"> �^�X�N </param>
        public void Push(Action action)
        {
            mTaskQueue.Enqueue(onNext =>
            {
                action();
                onNext();
            });
        }

        /// <summary>
        /// �^�X�N�ǉ��i�������R�[���o�b�N�L��j
        /// </summary>
        /// <param name="action"> �^�X�N </param>
        public void Push(Action<Action> action)
        {
            mTaskQueue.Enqueue(action);
        }

        /// <summary>
        /// �N���A
        /// </summary>
        public void Clear()
        {
            mTaskQueue.Clear();
        }

        /// <summary>
        /// ���s
        /// </summary>
        /// <param name="onComplete"> �������R�[���o�b�N </param>
        public void Process(Action onComplete = null)
        {
            _Process(onComplete);
        }


        //====================================
        //! �֐��iprivate�j
        //====================================

        /// <summary>
        /// ���s
        /// </summary>
        /// <param name="onComplete"> �������R�[���o�b�N </param>
        [DebuggerHidden]
        [DebuggerStepThrough]
        private void _Process(Action onComplete)
        {
            var action = mTaskQueue.Dequeue();

            action(() =>
            {
                if(mTaskQueue.Count > 0)
                {
                    _Process(onComplete);
                }
                else
                {
                    onComplete?.Invoke();
                }
            });
        }
    }
}
