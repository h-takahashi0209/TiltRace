
namespace TakahashiH
{
    /// <summary>
    /// string �g�����\�b�h��`�p
    /// </summary>
    public static class StringExtensions
    {
        //====================================
        //! �֐��ipublic static�j
        //====================================

        /// <summary>
        /// �t�H�[�}�b�g�`���ŕԂ�
        /// </summary>
        /// <param name="args"> ���� </param>
        public static string Format(this string self, object args)
        {
            return string.Format(self, args);
        }

        /// <summary>
        /// �t�H�[�}�b�g�`���ŕԂ�
        /// </summary>
        /// <param name="args1">    ����1    </param>
        /// <param name="args2">    ����2    </param>
        public static string Format(this string self, object args1, object args2)
        {
            return string.Format(self, args1, args2);
        }

        /// <summary>
        /// �t�H�[�}�b�g�`���ŕԂ�
        /// </summary>
        /// <param name="args1">    ����1    </param>
        /// <param name="args2">    ����2    </param>
        /// <param name="args3">    ����3    </param>
        public static string Format(this string self, object args1, object args2, object args3)
        {
            return string.Format(self, args1, args2, args3);
        }
    }
}
