using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 敵の車制御
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRaceEnemyCarController : ExMonoBehaviour
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// 車を非アクティブにするY座標
        /// </summary>
        private const float DeactivePosY = -2200f;


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 車生成機
        /// </summary>
        [SerializeField] private TiltRaceEnemyCarGenerator CarGenerator;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 非アクティブにする車の ID リスト
        /// </summary>
        private List<int> mDeactiveCarIdList = new List<int>();


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// アクティブな車の当たり判定データリスト
        /// </summary>
        public IReadOnlyList<ITiltRaceEnemyCarCollision> ActiveCarCollisionList => CarGenerator.ActiveCarList;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            CarGenerator.Initialize();
        }

        /// <summary>
        /// セットアップ
        /// </summary>
        public void Setup()
        {
            CarGenerator.Setup();
        }

        /// <summary>
        /// 開始
        /// </summary>
        public void Begin()
        {
            CarGenerator.Begin();
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public void Pause()
        {
            CarGenerator.Pause();
        }

        /// <summary>
        /// 再開
        /// </summary>
        public void Resume()
        {
            CarGenerator.Resume();
        }

        /// <summary>
        /// 座標更新
        /// </summary>
        /// <param name="playerCarPosition"> プレイヤーの車の座標 </param>
        public void UpdatePosition(Vector3 playerCarPosition)
        {
            mDeactiveCarIdList.Clear();

            bool existsDeactiveCar = false;

            // アクティブな車の座標更新
            for (int i = 0; i < CarGenerator.ActiveCarList.Count; i++)
            {
                var activeCar = CarGenerator.ActiveCarList[i];

                activeCar.UpdatePosition(playerCarPosition);

                if (activeCar.Position.y < DeactivePosY)
                {
                    mDeactiveCarIdList.Add(activeCar.Id);

                    existsDeactiveCar = true;
                }
            }

            // 指定座標を越えた車を非アクティブに
            if (existsDeactiveCar)
            {
                CarGenerator.SetDeactiveCar(mDeactiveCarIdList);
            }
        }

        /// <summary>
        /// レベルアップ
        /// </summary>
        public void LevelUp()
        {
            CarGenerator.LevelUp();
        }
    }
}
