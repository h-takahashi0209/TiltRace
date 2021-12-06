using UnityEngine;
using UnityEngine.UI;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 車 UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public sealed class UITiltRaceCar : ExMonoBehaviour
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// アニメーション種別
        /// </summary>
        public enum AnimType
        {
            Wait        ,
            Invincible  ,
            Bomb        ,
        }

        /// <summary>
        /// エフェクト種別
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
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 車画像
        /// </summary>
        [SerializeField] private Image UICarImage;

        /// <summary>
        /// 影画像
        /// </summary>
        [SerializeField] private Image UIShadowImage;

        /// <summary>
        /// 車のアニメーター
        /// </summary>
        [SerializeField] private Animator CarAnimator;

        /// <summary>
        /// エフェクトのアニメーター
        /// </summary>
        [SerializeField] private Animator EffectAnimator;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 初期サイズ
        /// </summary>
        private Vector2 mDefaultSizeDelta;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 座標
        /// </summary>
        public Vector3 Position => this.GetLocalPosition();

        /// <summary>
        /// 横幅
        /// </summary>
        public float Width => UICarImage.rectTransform.sizeDelta.x;

        /// <summary>
        /// 縦幅
        /// </summary>
        public float Height => UICarImage.rectTransform.sizeDelta.y;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            mDefaultSizeDelta = UICarImage.rectTransform.sizeDelta;
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="sprite">       スプライト   </param>
        /// <param name="position">     座標         </param>
        /// <param name="scale">        スケール     </param>
        public void Setup(Sprite sprite, Vector3 position, float scale)
        {
            UICarImage      .sprite = sprite;
            UIShadowImage   .sprite = sprite;

            SetPosition(position);

            UICarImage      .rectTransform.sizeDelta = mDefaultSizeDelta * scale;
            UIShadowImage   .rectTransform.sizeDelta = mDefaultSizeDelta * scale;
        }

        /// <summary>
        /// 座標設定
        /// </summary>
        /// <param name="position"> 座標 </param>
        public void SetPosition(Vector3 position)
        {
            this.SetLocalPosition(position);
        }

        /// <summary>
        /// アニメーション再生
        /// </summary>
        /// <param name="animType"> アニメーション種別 </param>
        public void PlayAnimation(AnimType animType)
        {
            CarAnimator.Play(animType.ToString());
        }

        /// <summary>
        /// エフェクト再生
        /// </summary>
        /// <param name="effectType"> エフェクト種別 </param>
        public void PlayEffect(EffectType effectType)
        {
            EffectAnimator.Play(effectType.ToString());
        }
    }
}
