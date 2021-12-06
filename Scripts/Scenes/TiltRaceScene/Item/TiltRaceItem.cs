using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �A�C�e��
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRaceItem : ExMonoBehaviour, ITiltRaceItemCollision
    {
        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �A�C�e���A�C�R��
        /// </summary>
        [SerializeField] private UITiltRaceItemIcon UIItemIcon;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// ��ړ��x�N�g��
        /// </summary>
        private Vector3 mDefMoveVec;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// �A�C�e�����
        /// </summary>
        public ItemType ItemType { get; private set; }

        /// <summary>
        /// ���W
        /// </summary>
        public Vector3 Position => transform.localPosition;

        /// <summary>
        /// ����
        /// </summary>
        public float Width => UIItemIcon.Width;

        /// <summary>
        /// �c��
        /// </summary>
        public float Height => UIItemIcon.Height;


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            UIItemIcon = GetComponentInChildren<UITiltRaceItemIcon>();
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        /// <param name="id">          ID              </param>
        /// <param name="itemType">    �A�C�e�����    </param>
        /// <param name="sprite">      �X�v���C�g      </param>
        /// <param name="position">    ���W            </param>
        public void Setup(int id, ItemType itemType, Sprite sprite, Vector3 position)
        {
            Id          = id;
            ItemType    = itemType;
            mDefMoveVec = Vector3.down;

            UIItemIcon.Setup(sprite);

            this.SetLocalPosition(position);
        }

        /// <summary>
        /// ���W�X�V
        /// </summary>
        public void UpdatePosition()
        {
            this.AddLocalPosition(mDefMoveVec * TiltRaceSettings.Item.Speed * TimeManager.DeltaTime);
        }
    }
}
