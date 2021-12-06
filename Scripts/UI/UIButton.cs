using System;
using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH
{
    /// <summary>
    /// �{�^�� UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public sealed class UIButton : ExMonoBehaviour
    {
        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// uGUI �{�^��
        /// </summary>
        [SerializeField] private Button Button;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �������R�[���o�b�N
        /// </summary>
        public Action OnClick { private get; set; }


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            Button = GetComponent<Button>();
        }

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            Button.onClick.AddListener(() => OnClick?.Invoke());
        }

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            OnClick = null;
        }
    }
}
