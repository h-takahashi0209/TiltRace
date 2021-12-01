using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace TakahashiH
{
    /// <summary>
    /// �󂯎�����^�X�N��񓯊����s����
    /// </summary>
    public sealed class ParallelTask
    {
        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �^�X�N�L���[
        /// </summary>
        private List<Action<Action>> mTaskQueue = new List<Action<Action>>();


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �^�X�N�ǉ��i�������R�[���o�b�N�����j
        /// </summary>
        /// <param name="task"> �^�X�N </param>
        public void Push(Action task)
        {
            mTaskQueue.Add(onNext =>
            {
                task();
                onNext();
            });
        }

        /// <summary>
        /// �^�X�N�ǉ��i�������R�[���o�b�N�L��j
        /// </summary>
        /// <param name="task"> �^�X�N </param>
        public void Push(Action<Action> task)
        {
            mTaskQueue.Add(task);
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
        [DebuggerHidden]
        [DebuggerStepThrough]
        public void Process(Action onComplete = null)
        {
            int compTaskNum = 0;

            for (int i = 0; i < mTaskQueue.Count; i++)
            {
                mTaskQueue[i](() =>
                {
                    compTaskNum++;

                    if(compTaskNum >= mTaskQueue.Count)
                    {
                        onComplete?.Invoke();
                    }
                });
            }
        }
    }
}
