using System.Diagnostics;
using UnityEngine;


namespace TakahashiH
{
    public interface IInputUnLocker
    {
        void Unlock();
    }

    /// <summary>
    /// ���͐���R���|�[�l���g
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class InputManager : ExMonoBehaviour, IInputUnLocker
    {
        //====================================
        //! �ϐ��iprivate static�j
        //====================================

        /// <summary>
        /// �C���X�^���X
        /// </summary>
        private static InputManager msInstance;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// ���b�N��
        /// </summary>
        private int mLockCount;


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            if (msInstance && msInstance != this)
            {
                Destroy(this);
            }

            msInstance = this;

            this.SetActive(false);
        }


        //====================================
        //! �֐��ipublic static�j
        //====================================

        /// <summary>
        /// ���͐���
        /// </summary>
        public static IInputUnLocker Lock()
        {
            if (!msInstance) {
                return null;
            }

            msInstance.SetActive(true);

            msInstance.mLockCount++;

            msInstance.UpdateLockCount();

            return msInstance;
        }

        /// <summary>
        /// �L�[���������ꂽ��
        /// </summary>
        /// <param name="keyCode"> �L�[�R�[�h </param>
        public static bool IsKeyDown(KeyCode keyCode)
        {
            if (!msInstance || msInstance.mLockCount > 0)
            {
                return false;
            }

            return Input.GetKeyDown(keyCode);
        }

        /// <summary>
        /// �L�[�������ꂽ��
        /// </summary>
        /// <param name="keyCode"> �L�[�R�[�h </param>
        public static bool IsKeyUp(KeyCode keyCode)
        {
            if (!msInstance || msInstance.mLockCount > 0)
            {
                return false;
            }

            return Input.GetKeyUp(keyCode);
        }

        /// <summary>
        /// �L�[��������
        /// </summary>
        /// <param name="keyCode"> �L�[�R�[�h </param>
        public static bool IsKeyPress(KeyCode keyCode)
        {
            if (!msInstance || msInstance.mLockCount > 0)
            {
                return false;
            }

            return Input.GetKey(keyCode);
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// ���͐�������
        /// </summary>
        public void Unlock()
        {
            mLockCount--;

            UpdateLockCount();

            if (mLockCount > 0) {
                return;
            }

            this.SetActive(false);
        }


        //====================================
        //! �֐��iprivate�j
        //====================================

        /// <summary>
        /// ���b�N���\���X�V
        /// </summary>
        [Conditional("UNITY_EDITOR")]
        private void UpdateLockCount()
        {
            if (mLockCount <= 0)
            {
                msInstance.name = "InputManager(UnLock)";
            }
            else
            {
                msInstance.name = $"InputManager(Lock({msInstance.mLockCount}))";
            }
        }
    }
}
