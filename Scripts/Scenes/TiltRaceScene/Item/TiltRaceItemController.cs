using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - アイテム制御
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRaceItemController : ExMonoBehaviour
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// アイテムを非アクティブにするY座標
        /// </summary>
        private const float DeactivePosY = -2200f;


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// アイテム生成機
        /// </summary>
        [SerializeField] private TiltRaceItemGenerator ItemGenerator;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 非アクティブにするアイテムの ID リスト
        /// </summary>
        private List<int> mDeactiveItemIdList = new List<int>();


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// アクティブなアイテムの当たり判定データリスト
        /// </summary>
        public IReadOnlyList<ITiltRaceItemCollision> ActiveItemCollisionList => ItemGenerator.ActiveItemList;


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            ItemGenerator.Initialize();
        }

        /// <summary>
        /// セットアップ
        /// </summary>
        public void Setup()
        {
            ItemGenerator.Setup();
        }

        /// <summary>
        /// 開始
        /// </summary>
        public void Begin()
        {
            ItemGenerator.Begin();
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public void Pause()
        {
            ItemGenerator.Pause();
        }

        /// <summary>
        /// 再開
        /// </summary>
        public void Resume()
        {
            ItemGenerator.Resume();
        }

        /// <summary>
        /// アイテムの座標更新
        /// </summary>
        public void UpdatePosition()
        {
            mDeactiveItemIdList.Clear();

            bool existsDeactiveItem = false;

            // アクティブなアイテムの座標更新
            for (int i = 0; i < ItemGenerator.ActiveItemList.Count; i++)
            {
                var activeItem = ItemGenerator.ActiveItemList[i];

                activeItem.UpdatePosition();

                if (activeItem.Position.y < DeactivePosY)
                {
                    mDeactiveItemIdList.Add(activeItem.Id);

                    existsDeactiveItem = true;
                }
            }

            // 指定座標を越えたアイテムを非アクティブに
            if (existsDeactiveItem)
            {
                ItemGenerator.SetDeactiveItem(mDeactiveItemIdList);
            }
        }

        /// <summary>
        /// 指定されたアイテムを非アクティブにする
        /// </summary>
        /// <param name="itemIdList"> 対象のアイテムの ID リスト </param>
        public void SetDeactiveItem(int id)
        {
            ItemGenerator.SetDeactiveItem(id);
        }
    }
}
