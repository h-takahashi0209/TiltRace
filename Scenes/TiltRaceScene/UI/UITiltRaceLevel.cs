using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - ���x�� UI
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UITiltRaceLevel : ExMonoBehaviour
    {
        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// ���x���e�L�X�g
        /// </summary>
        [SerializeField] private Text UILevelText;


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        public void Setup()
        {
            SetLevel(0);
        }

        /// <summary>
        /// ���x���ݒ�
        /// </summary>
        /// <param name="level"> ���x�� </param>
        public void SetLevel(float level)
        {
            UILevelText.text = ((int)level).ToString();
        }
    }
}
