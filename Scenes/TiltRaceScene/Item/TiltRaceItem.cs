using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - アイテム
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRaceItem : ExMonoBehaviour, ITiltRaceItemCollision
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// アイテムアイコン
        /// </summary>
        [SerializeField] private UITiltRaceItemIcon UIItemIcon;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 基準移動ベクトル
        /// </summary>
        private Vector3 mDefMoveVec;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// アイテム種別
        /// </summary>
        public ItemType ItemType { get; private set; }

        /// <summary>
        /// 座標
        /// </summary>
        public Vector3 Position => transform.localPosition;

        /// <summary>
        /// 横幅
        /// </summary>
        public float Width => UIItemIcon.Width;

        /// <summary>
        /// 縦幅
        /// </summary>
        public float Height => UIItemIcon.Height;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            UIItemIcon = GetComponentInChildren<UITiltRaceItemIcon>();
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="id">          ID              </param>
        /// <param name="itemType">    アイテム種別    </param>
        /// <param name="sprite">      スプライト      </param>
        /// <param name="position">    座標            </param>
        public void Setup(int id, ItemType itemType, Sprite sprite, Vector3 position)
        {
            Id          = id;
            ItemType    = itemType;
            mDefMoveVec = Vector3.down;

            UIItemIcon.Setup(sprite);

            this.SetLocalPosition(position);
        }

        /// <summary>
        /// 座標更新
        /// </summary>
        public void UpdatePosition()
        {
            this.AddLocalPosition(mDefMoveVec * TiltRaceSettings.Item.Speed * TimeManager.DeltaTime);
        }
    }
}
