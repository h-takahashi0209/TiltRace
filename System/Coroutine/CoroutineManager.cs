using System;
using System.Collections;
using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// �R���[�`���Ǘ�
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class CoroutineManager : ExMonoBehaviour
    {
        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �C���X�^���X
        /// </summary>
        public static CoroutineManager Instance { get; private set; }


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(this);
            }

            Instance = this;
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �w�莞�ԑҋ@
        /// </summary>
        /// <param name="timeSec">       �ҋ@���ԁi�b�j        </param>
        /// <param name="onComplete">    �������R�[���o�b�N    </param>
        public IEnumerator CallWaitForSeconds(float timeSec, Action onComplete)
        {
            var enumerator = _CallWaitForSeconds(timeSec, onComplete);

            StartCoroutine(enumerator);

            return enumerator;
        }

        /// <summary>
        /// �ꎞ��~
        /// </summary>
        /// <param name="coroutine"> �R���[�`�� </param>
        public void PauseCoroutie(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
        }

        /// <summary>
        /// �ĊJ
        /// </summary>
        /// <param name="coroutine"> �R���[�`�� </param>
        public void ResumeCoroutie(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }


        //====================================
        //! �֐��iprivate�j
        //====================================

        /// <summary>
        /// �w�莞�ԑҋ@
        /// </summary>
        /// <param name="timeSec">       �ҋ@���ԁi�b�j        </param>
        /// <param name="onComplete">    �������R�[���o�b�N    </param>
        public IEnumerator _CallWaitForSeconds(float timeSec, Action onComplete)
        {
            yield return new WaitForSeconds(timeSec);

            onComplete();
        }
    }
}
