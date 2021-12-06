using System;
using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH
{
    /// <summary>
    /// ボタン UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public sealed class UIButton : ExMonoBehaviour
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// uGUI ボタン
        /// </summary>
        [SerializeField] private Button Button;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 押下時コールバック
        /// </summary>
        public Action OnClick { private get; set; }


        //====================================
        //! 関数（MonoBehaviour）
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
