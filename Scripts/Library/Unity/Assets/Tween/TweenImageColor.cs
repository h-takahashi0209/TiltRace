using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH
{
    /// <summary>
    /// �摜�̐F�ύX Tween
    /// </summary>
    [RequireComponent(typeof(Image))]
    public sealed class TweenImageColor : TweenBase<Color>
    {
        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �摜
        /// </summary>
        [SerializeField] private Image Image;


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            Image = GetComponent<Image>();
        }


        //====================================
        //! �֐��iTweenBase�j
        //====================================

        /// <summary>
        /// �J�n������
        /// </summary>
        protected override void DoBegin()
        {
            Image.color = FromAppliedReverse;
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        protected override void DoUpdate()
        {
            Image.color = Color.Lerp(FromAppliedReverse, ToAppliedReverse, Progress);
        }

        /// <summary>
        /// ����������
        /// </summary>
        protected override void DoComplete()
        {
            Image.color = ToAppliedReverse;
        }
    }
}
