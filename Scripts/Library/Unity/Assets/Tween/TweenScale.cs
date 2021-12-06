using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// �X�P�[���g�k Tween
    /// </summary>
    public sealed class TweenScale : TweenBase<Vector3>
    {
        //====================================
        //! �֐��iTweenBase�j
        //====================================

        /// <summary>
        /// �J�n������
        /// </summary>
        protected override void DoBegin()
        {
            this.SetLocalScale(FromAppliedReverse);
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        protected override void DoUpdate()
        {
            this.SetLocalScale(Vector3.Lerp(FromAppliedReverse, ToAppliedReverse, Progress));
        }

        /// <summary>
        /// ����������
        /// </summary>
        protected override void DoComplete()
        {
            this.SetLocalScale(ToAppliedReverse);
        }
    }
}
