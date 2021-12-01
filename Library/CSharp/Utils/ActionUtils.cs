using System;


namespace TakahashiH
{
    /// <summary>
    /// Action �֘A Utils
    /// </summary>
    public static class ActionUtils
    {
        //====================================
        //! �֐��ipublic static�j
        //====================================

        /// <summary>
        /// ��x�����Ăяo��
        /// </summary>
        /// <param name="rAction"> �Ăяo�� Action </param>
        public static void CallOnce(ref Action rAction)
        {
            if (rAction == null) {
                return;
            }

            var tempAction = rAction;
            rAction = null;
            tempAction();
        }

        /// <summary>
        /// ��x�����Ăяo��
        /// </summary>
        /// <param name="rAction">    �Ăяo�� Action    </param>
        /// <param name="arg">        ����               </param>
        public static void CallOnce<T>(ref Action<T> rAction, T arg)
        {
            if (rAction == null) {
                return;
            }

            var tempAction = rAction;
            rAction = null;
            tempAction(arg);
        }

        /// <summary>
        /// ��x�����Ăяo��
        /// </summary>
        /// <param name="rAction">    �Ăяo�� Action    </param>
        /// <param name="arg1">       ����1              </param>
        /// <param name="arg2">       ����2              </param>
        public static void CallOnce<T1, T2>(ref Action<T1, T2> rAction, T1 arg1, T2 arg2)
        {
            if (rAction == null) {
                return;
            }

            var tempAction = rAction;
            rAction = null;
            tempAction(arg1, arg2);
        }

        /// <summary>
        /// ��x�����Ăяo��
        /// </summary>
        /// <param name="rAction">    �Ăяo�� Action    </param>
        /// <param name="arg1">       ����1              </param>
        /// <param name="arg2">       ����2              </param>
        /// <param name="arg3">       ����3              </param>
        public static void CallOnce<T1, T2, T3>(ref Action<T1, T2, T3> rAction, T1 arg1, T2 arg2, T3 arg3)
        {
            if (rAction == null) {
                return;
            }

            var tempAction = rAction;
            rAction = null;
            tempAction(arg1, arg2, arg3);
        }
    }
}
