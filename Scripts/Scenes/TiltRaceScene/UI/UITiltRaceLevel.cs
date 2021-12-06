using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - レベル UI
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UITiltRaceLevel : ExMonoBehaviour
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// レベルテキスト
        /// </summary>
        [SerializeField] private Text UILevelText;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        public void Setup()
        {
            SetLevel(0);
        }

        /// <summary>
        /// レベル設定
        /// </summary>
        /// <param name="level"> レベル </param>
        public void SetLevel(float level)
        {
            UILevelText.text = ((int)level).ToString();
        }
    }
}
