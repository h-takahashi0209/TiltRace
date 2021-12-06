using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 走行距離 UI
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UITiltRaceDistance : ExMonoBehaviour
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 走行距離テキスト
        /// </summary>
        [SerializeField] private Text UIDistanceText;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        public void Setup()
        {
            SetDistance(0);
        }

        /// <summary>
        /// 走行距離設定
        /// </summary>
        /// <param name="distance"> 走行距離 </param>
        public void SetDistance(float distance)
        {
            UIDistanceText.text = ((int)distance).ToString();
        }
    }
}
