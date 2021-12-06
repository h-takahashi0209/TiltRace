using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// Stack �g�����\�b�h��`�p
    /// </summary>
    public static class StackExtensions
    {
        //====================================
        //! �֐��ipublic static�j
        //====================================

        /// <summary>
        /// �擪�̗v�f���폜���ĕԂ�
        /// �v�f�������ꍇ�̓f�t�H���g�̒l��Ԃ�
        /// </summary>
        public static T PopOrDefault<T>(this Stack<T> self)
        {
            if (self.Count == 0)
            {
                return default(T);
            }

            return self.Pop();
        }

        /// <summary>
        /// �擪�̗v�f���폜�����Ԃ�
        /// �v�f�������ꍇ�̓f�t�H���g�̒l��Ԃ�
        /// </summary>
        public static T PeekOrDefault<T>(this Stack<T> self)
        {
            if (self.Count == 0)
            {
                return default(T);
            }

            return self.Peek();
        }
    }
}
