using System;
using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// �t�F�[�h UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TweenImageColor))]
    public sealed class UIFade : ExMonoBehaviour
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// �f�t�H���g�̃t�F�[�h����
        /// </summary>
        private readonly float DefDurationTimeSec = 0.5f;

        /// <summary>
        /// �f�t�H���g�̃t�F�[�h�F
        /// </summary>
        private readonly Color DefColor = Color.black;


        //====================================
        //! �ϐ��iprivate static�j
        //====================================

        /// <summary>
        /// �C���X�^���X
        /// </summary>
        private static UIFade msInstance;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �J�n�F
        /// </summary>
        private Color mFromColor;

        /// <summary>
        /// �ڕW�F
        /// </summary>
        private Color mToColor;


        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �F�ύX Tween
        /// </summary>
        [SerializeField] private TweenImageColor TweenImageColor;


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            TweenImageColor = GetComponent<TweenImageColor>();
        }

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
        }


        //====================================
        //! �֐��ipublic static�j
        //====================================

        /// <summary>
        /// �t�F�[�h�C��
        /// </summary>
        /// <param name="color">            �t�F�[�h�F                   </param>
        /// <param name="durationTimeSec">  �t�F�[�h�ɂ����鎞�ԁi�b�j   </param>
        /// <param name="onComplete">       �������R�[���o�b�N           </param>
        public static void FadeIn(Color color, float durationTimeSec, Action onComplete = null)
        {
            Fade(true, color, durationTimeSec, onComplete);
        }

        /// <summary>
        /// �t�F�[�h�C��
        /// </summary>
        /// <param name="color">        �t�F�[�h�F           </param>
        /// <param name="onComplete">   �������R�[���o�b�N   </param>
        public static void FadeIn(Color color, Action onComplete = null)
        {
            if (!msInstance)
            {
                onComplete?.Invoke();
                return;
            }

            FadeIn(color, msInstance.DefDurationTimeSec, onComplete);
        }

        /// <summary>
        /// �t�F�[�h�C��
        /// </summary>
        /// <param name="durationTimeSec">  �t�F�[�h�ɂ����鎞�ԁi�b�j   </param>
        /// <param name="onComplete">       �������R�[���o�b�N           </param>
        public static void FadeIn(float durationTimeSec, Action onComplete = null)
        {
            if (!msInstance)
            {
                onComplete?.Invoke();
                return;
            }

            FadeIn(msInstance.DefColor, durationTimeSec, onComplete);
        }

        /// <summary>
        /// �t�F�[�h�C��
        /// </summary>
        /// <param name="onComplete"> �������R�[���o�b�N </param>
        public static void FadeIn(Action onComplete = null)
        {
            if (!msInstance)
            {
                onComplete?.Invoke();
                return;
            }

            FadeIn(msInstance.DefColor, msInstance.DefDurationTimeSec, onComplete);
        }

        /// <summary>
        /// �t�F�[�h�A�E�g
        /// </summary>
        /// <param name="color">            �t�F�[�h�F                   </param>
        /// <param name="durationTimeSec">  �t�F�[�h�ɂ����鎞�ԁi�b�j   </param>
        /// <param name="onComplete">       �������R�[���o�b�N           </param>
        public static void FadeOut(Color color, float durationTimeSec, Action onComplete = null)
        {
            Fade(false, color, durationTimeSec, onComplete);
        }

        /// <summary>
        /// �t�F�[�h�A�E�g
        /// </summary>
        /// <param name="color">        �t�F�[�h�F           </param>
        /// <param name="onComplete">   �������R�[���o�b�N   </param>
        public static void FadeOut(Color color, Action onComplete = null)
        {
            FadeOut(color, msInstance.DefDurationTimeSec, onComplete);
        }

        /// <summary>
        /// �t�F�[�h�A�E�g
        /// </summary>
        /// <param name="durationTimeSec">  �t�F�[�h�ɂ����鎞�ԁi�b�j   </param>
        /// <param name="onComplete">       �������R�[���o�b�N           </param>
        public static void FadeOut(float durationTimeSec, Action onComplete = null)
        {
            if (!msInstance)
            {
                onComplete?.Invoke();
                return;
            }

            FadeOut(msInstance.DefColor, durationTimeSec, onComplete);
        }

        /// <summary>
        /// �t�F�[�h�A�E�g
        /// </summary>
        /// <param name="onComplete"> �������R�[���o�b�N </param>
        public static void FadeOut(Action onComplete = null)
        {
            if (!msInstance)
            {
                onComplete?.Invoke();
                return;
            }

            FadeOut(msInstance.DefColor, msInstance.DefDurationTimeSec, onComplete);
        }


        //====================================
        //! �֐��iprivate�j
        //====================================

        /// <summary>
        /// �t�F�[�h
        /// </summary>
        /// <param name="isFadeIn">           �t�F�[�h�C�������邩          </param>
        /// <param name="color">              �t�F�[�h�F                    </param>
        /// <param name="durationTimeSec">    �t�F�[�h�ɂ����鎞�ԁi�b�j    </param>
        /// <param name="onComplete">         �������R�[���o�b�N            </param>
        private static void Fade(bool isFadeIn, Color color, float durationTimeSec, Action onComplete)
        {
            if (!msInstance)
            {
                onComplete?.Invoke();
                return;
            }

            var inputUnlocker = InputManager.Lock();

            msInstance.mFromColor = color;
            msInstance.mFromColor.a = 1f;

            msInstance.mToColor = color;
            msInstance.mToColor.a = 0f;

            msInstance.TweenImageColor.From             = msInstance.mFromColor;
            msInstance.TweenImageColor.To               = msInstance.mToColor;
            msInstance.TweenImageColor.DurationTimeSec  = durationTimeSec;

            msInstance.TweenImageColor.OnComplete = () =>
            {
                inputUnlocker.Unlock();
                onComplete?.Invoke();
            };

            if (isFadeIn)
            {
                msInstance.TweenImageColor.Begin();
            }
            else
            {
                msInstance.TweenImageColor.BeginReverse();
            }
        }
    }
}
