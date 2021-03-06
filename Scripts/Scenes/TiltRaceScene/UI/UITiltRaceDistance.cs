using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - s£ UI
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UITiltRaceDistance : ExMonoBehaviour
    {
        //====================================
        //! ΟiSerializeFieldj
        //====================================

        /// <summary>
        /// s£eLXg
        /// </summary>
        [SerializeField] private Text UIDistanceText;


        //====================================
        //! Φipublicj
        //====================================

        /// <summary>
        /// ZbgAbv
        /// </summary>
        public void Setup()
        {
            SetDistance(0);
        }

        /// <summary>
        /// s£έθ
        /// </summary>
        /// <param name="distance"> s£ </param>
        public void SetDistance(float distance)
        {
            UIDistanceText.text = ((int)distance).ToString();
        }
    }
}
