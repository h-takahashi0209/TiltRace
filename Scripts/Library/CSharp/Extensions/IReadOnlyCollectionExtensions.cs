using System;
using System.Collections.Generic;


namespace TakahashiH
{
    /// <summary>
    /// IReadOnlyCollection �g�����\�b�h��`�p
    /// </summary>
    public static class IReadOnlyCollectionExtensions
    {
        //====================================
        //! �֐��ipublic static�j
        //====================================

        /// <summary>
        /// ��
        /// </summary>
        public static bool IsEmpty<T>(this IReadOnlyCollection<T> self)
        {
            return self.Count == 0;
        }

        /// <summary>
        /// null �܂��͋�
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T> self)
        {
            return self == null || self.Count == 0;
        }

        /// <summary>
        /// �v�f��1�ł����邩
        /// </summary>
        public static bool IsNotNullOrEmpty<T>(this IReadOnlyCollection<T> self)
        {
            return !self.IsNullOrEmpty();
        }
    }
}
