using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - プレイヤーの車
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UITiltRaceCar))]
    public sealed class TiltRacePlayerCar : ExMonoBehaviour, ITiltRacePlayerCarCollision
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 車 UI
        /// </summary>
        [SerializeField] private UITiltRaceCar UICar;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 走行距離
        /// </summary>
        public float Distance { get; set; }

        /// <summary>
        /// 無敵状態か
        /// </summary>
        public bool IsInvincible { get; set; }

        /// <summary>
        /// ライフ0か
        /// </summary>
        public bool IsLifeZero => Life <= 0;

        /// <summary>
        /// ライフ
        /// </summary>
        public int Life { get; set; }

        /// <summary>
        /// 座標
        /// </summary>
        public Vector3 Position => UICar.Position;

        /// <summary>
        /// 横幅
        /// </summary>
        public float Width => UICar.Width;

        /// <summary>
        /// 縦幅
        /// </summary>
        public float Height => UICar.Height;

        /// <summary>
        /// 移動速度
        /// </summary>
        public float Speed { get; set; }


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            UICar = GetComponent<UITiltRaceCar>();
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="sprite">      スプライト    </param>
        /// <param name="position">    座標          </param>
        /// <param name="scale">       スケール      </param>
        public void Setup(Sprite sprite, Vector3 position, float scale)
        {
            IsInvincible    = false;
            Life            = TiltRaceSettings.Player.DefLife;
            Distance        = 0;
            Speed           = TiltRaceSettings.Player.DefSpeed;

            UICar.Setup(sprite, position, scale);
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
        /// 走行距離更新
        /// </summary>
        public void UpdateDistance()
        {
            Distance += TimeManager.DeltaTime * TiltRaceSettings.Player.OneFrameDistance;
        }

        /// <summary>
        /// 速度加算
        /// </summary>
        /// <param name="addedSpeed"> 加算する速度 </param>
        public void AddSpeed(int addedSpeed)
        {
            Speed = Mathf.Max(Speed + addedSpeed, TiltRaceSettings.Player.SpeedMin);
        }

        /// <summary>
        /// アニメーション再生
        /// </summary>
        /// <param name="animType"> アニメーション種別 </param>
        public void PlayAnimation(UITiltRaceCar.AnimType animType)
        {
            UICar.PlayAnimation(animType);
        }

        /// <summary>
        /// エフェクト再生
        /// </summary>
        /// <param name="effectType"> エフェクト種別 </param>
        public void PlayEffect(UITiltRaceCar.EffectType effectType)
        {
            UICar.PlayEffect(effectType);
        }
    }
}
