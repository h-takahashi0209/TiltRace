using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// Queue �g�����\�b�h��`�p
    /// </summary>
    public static class QueueExtensions
    {
        //====================================
        //! �֐��ipublic static�j
        //====================================

        /// <summary>
        /// �擪�̗v�f���폜���ĕԂ�
        /// �v�f�������ꍇ�̓f�t�H���g�̒l��Ԃ�
        /// </summary>
        public static T DequeueOrDefault<T>(this Queue<T> self)
        {
            if (self.Count == 0)
            {
                return default(T);
            }

            return self.Dequeue();
        }

        /// <summary>
        /// �擪�̗v�f���폜�����Ԃ�
        /// �v�f�������ꍇ�̓f�t�H���g�̒l��Ԃ�
        /// </summary>
        public static T PeekOrDefault<T>(this Queue<T> self)
        {
            if (self.Count == 0)
            {
                return default(T);
            }

            return self.Peek();
        }
    }
}
