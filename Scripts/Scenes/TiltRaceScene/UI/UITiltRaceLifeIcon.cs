using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 残ライフアイコン
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Animator))]
    public sealed class UITiltRaceLifeIcon : ExMonoBehaviour
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// アニメーション種別
        /// </summary>
        public enum AnimType
        {
            Enable  ,
            Disable ,
            Show    ,
            Hide    ,
        }


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// アニメーター
        /// </summary>
        [SerializeField] private Animator Animator;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            Animator = GetComponent<Animator>();
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="isShow"> 表示するか </param>
        public void Setup(bool isShow)
        {
            var animType = isShow ? AnimType.Enable : AnimType.Disable;

            PlayAnimation(animType);
        }

        /// <summary>
        /// アニメーション再生
        /// </summary>
        /// <param name="animType"> アニメーション種別 </param>
        public void PlayAnimation(AnimType animType)
        {
            Animator.Play(animType.ToString());
        }
    }
}
