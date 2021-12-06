using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// ���[���h���W�ړ� Tween
    /// </summary>
    public sealed class TweenWorldPosition : TweenBase<Vector3>
    {
        //====================================
        //! �֐��iTweenBase�j
        //====================================

        /// <summary>
        /// �J�n������
        /// </summary>
        protected override void DoBegin()
        {
            this.SetWorldPosition(FromAppliedReverse);
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        protected override void DoUpdate()
        {
            this.SetWorldPosition(Vector3.Lerp(FromAppliedReverse, ToAppliedReverse, Progress));
        }

        /// <summary>
        /// ����������
        /// </summary>
        protected override void DoComplete()
        {
            this.SetWorldPosition(ToAppliedReverse);
        }
    }
}
