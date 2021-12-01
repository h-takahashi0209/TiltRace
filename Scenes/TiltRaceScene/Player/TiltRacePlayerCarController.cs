using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 自身の車制御
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRacePlayerCarController : ExMonoBehaviour
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// プレイヤーの車
        /// </summary>
        [SerializeField] private TiltRacePlayerCar Car;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 初期座標
        /// </summary>
        private Vector3 mDefPosition;

        /// <summary>
        /// タイマー
        /// </summary>
        private Timer mTimer = new Timer();


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 走行距離
        /// </summary>
        public float Distance => Car.Distance;

        /// <summary>
        /// 座標
        /// </summary>
        public Vector3 Position => Car.Position;

        /// <summary>
        /// ライフ
        /// </summary>
        public int Life => Car.Life;

        /// <summary>
        /// ライフ0か
        /// </summary>
        public bool IsLifeZero => Car.IsLifeZero;

        /// <summary>
        /// 当たり判定
        /// </summary>
        public ITiltRacePlayerCarCollision Collision => Car;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            mTimer.Dispose();
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            mDefPosition = Car.Position;
        }

        /// <summary>
        /// セットアップ
        /// </summary>
        public void Setup()
        {
            var sprite = Resources.Load<Sprite>(Path.Scenes.TiltRaceScene.CarImage.Format(CarColor.Green));

            Car.Setup(sprite, mDefPosition, 1f);
            Car.PlayAnimation(UITiltRaceCar.AnimType.Wait);
        }

        /// <summary>
        /// 車の状態更新
        /// </summary>
        /// <param name="enemyActiveCarCollisionList"> 敵のアクティブな車の当たり判定データリスト </param>
        public void UpdateCar(IReadOnlyList<ITiltRaceCollision> enemyActiveCarCollisionList)
        {
            Car.UpdateDistance();

            mTimer.UpdateTimer(TimeManager.DeltaTime);

            Move();
        }

        /// <summary>
        /// 回復
        /// </summary>
        /// <param name="life"> 回復後のライフ </param>
        public void Recovery(int life)
        {
            if (life == Car.Life) {
                return;
            }

            Car.Life = life;

            Car.PlayEffect(UITiltRaceCar.EffectType.RecoveryLife);
        }

        /// <summary>
        /// 被ダメージ
        /// </summary>
        /// <param name="life"> 被ダメージ後のライフ </param>
        public void Damage(int life)
        {
            if (life == Car.Life) {
                return;
            }

            Car.Life = life;

            Car.PlayEffect(UITiltRaceCar.EffectType.Hit);

            if (Car.IsLifeZero)
            {
                Car.PlayAnimation(UITiltRaceCar.AnimType.Bomb);
                return;
            }

            BeginInvincible();
        }

        /// <summary>
        /// アイテム取得エフェクト再生
        /// </summary>
        /// <param name="itemType"> アイテム種別 </param>
        public void PlayGetItemEffect(ItemType itemType)
        {
            var effectType = itemType switch
            {
                ItemType.RecoveryLife  => UITiltRaceCar.EffectType.RecoveryLife ,
                ItemType.SpeedUp       => UITiltRaceCar.EffectType.SpeedUp      ,
                ItemType.SpeedDown     => UITiltRaceCar.EffectType.SpeedDown    ,
                _                      => UITiltRaceCar.EffectType.None         ,
            };

            Car.PlayEffect(effectType);
        }

        /// <summary>
        /// 移動速度加算
        /// </summary>
        /// <param name="addedSpeed"> 加算する速度 </param>
        public void AddSpeed(int addedSpeed)
        {
            Car.AddSpeed(addedSpeed);
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public void Pause()
        {
            mTimer.Pause();
        }

        /// <summary>
        /// 再開
        /// </summary>
        public void Resume()
        {
            mTimer.Resume();
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 移動制御
        /// </summary>
        private void Move()
        {
            if (Car.IsLifeZero) {
                return;
            }

            var position = Car.Position + TiltRaceInputManager.GetInputVec(Car.Speed);

            // 上
            if (position.y > TiltRaceSettings.HeightLimit)
            {
                position.y = TiltRaceSettings.HeightLimit;
            }

            // 下
            if (position.y < -TiltRaceSettings.HeightLimit)
            {
                position.y = -TiltRaceSettings.HeightLimit;
            }

            // 左
            if (position.x < -TiltRaceSettings.WidthLimit)
            {
                position.x = -TiltRaceSettings.WidthLimit;
            }

            // 右
            if (position.x > TiltRaceSettings.WidthLimit)
            {
                position.x = TiltRaceSettings.WidthLimit;
            }

            Car.SetPosition(position);
        }

        /// <summary>
        /// 無敵状態開始
        /// </summary>
        private void BeginInvincible()
        {
            Car.IsInvincible = true;

            Car.PlayAnimation(UITiltRaceCar.AnimType.Invincible);

            mTimer.Begin(TiltRaceSettings.Player.DamageInvincibleTimeSec, () =>
            {
                Car.IsInvincible = false;

                Car.PlayAnimation(UITiltRaceCar.AnimType.Wait);
            });
        }
    }
}
