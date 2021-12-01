using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �� UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public sealed class UITiltRaceCar : ExMonoBehaviour
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// �A�j���[�V�������
        /// </summary>
        public enum AnimType
        {
            Wait        ,
            Invincible  ,
            Bomb        ,
        }

        /// <summary>
        /// �G�t�F�N�g���
        /// </summary>
        public enum EffectType
        {
            None            ,
            Hit             ,
            RecoveryLife    ,
            SpeedUp         ,
            SpeedDown       ,
        }


        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �ԉ摜
        /// </summary>
        [SerializeField] private Image UICarImage;

        /// <summary>
        /// �e�摜
        /// </summary>
        [SerializeField] private Image UIShadowImage;

        /// <summary>
        /// �Ԃ̃A�j���[�^�[
        /// </summary>
        [SerializeField] private Animator CarAnimator;

        /// <summary>
        /// �G�t�F�N�g�̃A�j���[�^�[
        /// </summary>
        [SerializeField] private Animator EffectAnimator;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �����T�C�Y
        /// </summary>
        private Vector2 mDefaultSizeDelta;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// ���W
        /// </summary>
        public Vector3 Position => this.GetLocalPosition();

        /// <summary>
        /// ����
        /// </summary>
        public float Width => UICarImage.rectTransform.sizeDelta.x;

        /// <summary>
        /// �c��
        /// </summary>
        public float Height => UICarImage.rectTransform.sizeDelta.y;


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            mDefaultSizeDelta = UICarImage.rectTransform.sizeDelta;
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        /// <param name="sprite">       �X�v���C�g   </param>
        /// <param name="position">     ���W         </param>
        /// <param name="scale">        �X�P�[��     </param>
        public void Setup(Sprite sprite, Vector3 position, float scale)
        {
            UICarImage      .sprite = sprite;
            UIShadowImage   .sprite = sprite;

            SetPosition(position);

            UICarImage      .rectTransform.sizeDelta = mDefaultSizeDelta * scale;
            UIShadowImage   .rectTransform.sizeDelta = mDefaultSizeDelta * scale;
        }

        /// <summary>
        /// ���W�ݒ�
        /// </summary>
        /// <param name="position"> ���W </param>
        public void SetPosition(Vector3 position)
        {
            this.SetLocalPosition(position);
        }

        /// <summary>
        /// �A�j���[�V�����Đ�
        /// </summary>
        /// <param name="animType"> �A�j���[�V������� </param>
        public void PlayAnimation(AnimType animType)
        {
            CarAnimator.Play(animType.ToString());
        }

        /// <summary>
        /// �G�t�F�N�g�Đ�
        /// </summary>
        /// <param name="effectType"> �G�t�F�N�g��� </param>
        public void PlayEffect(EffectType effectType)
        {
            EffectAnimator.Play(effectType.ToString());
        }
    }
}
