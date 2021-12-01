using System;


namespace TakahashiH
{
    /// <summary>
    /// DateTime �g�����\�b�h��`�p
    /// </summary>
    public static class DateTimeExtensions
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// Unix �G�|�b�N
        /// </summary>
        private static DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0);


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// Unix ���Ԃɕϊ�
        /// </summary>
        public static long ToUnixTime(this DateTime self)
        {
            return (long)(self.ToUniversalTime() - UNIX_EPOCH).TotalSeconds;
        }
    }
}
