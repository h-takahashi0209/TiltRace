using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - テロップ UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public sealed class UITiltRaceTelop : ExMonoBehaviour
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// テロップ種別
        /// </summary>
        public enum TelopType
        {
            LevelUp
        }


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// アニメーター
        /// </summary>
        [SerializeField] private Animator Animator;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 再生
        /// </summary>
        /// <param name="telopType"> テロップ種別 </param>
        public void Play(TelopType telopType)
        {
            Animator.Play(telopType.ToString());
        }
    }
}
