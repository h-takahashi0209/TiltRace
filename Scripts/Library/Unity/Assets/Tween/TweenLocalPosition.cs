using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// ���[�J�����W�ړ� Tween
    /// </summary>
    public sealed class TweenLocalPosition : TweenBase<Vector3>
    {
        //====================================
        //! �֐��iTweenBase�j
        //====================================

        /// <summary>
        /// �J�n������
        /// </summary>
        protected override void DoBegin()
        {
            this.SetLocalPosition(FromAppliedReverse);
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        protected override void DoUpdate()
        {
            this.SetLocalPosition(Vector3.Lerp(FromAppliedReverse, ToAppliedReverse, Progress));
        }

        /// <summary>
        /// ����������
        /// </summary>
        protected override void DoComplete()
        {
            this.SetLocalPosition(ToAppliedReverse);
        }
    }
}
