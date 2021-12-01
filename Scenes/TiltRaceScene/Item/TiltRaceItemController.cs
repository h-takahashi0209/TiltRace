using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �A�C�e������
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRaceItemController : ExMonoBehaviour
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// �A�C�e�����A�N�e�B�u�ɂ���Y���W
        /// </summary>
        private const float DeactivePosY = -2200f;


        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �A�C�e�������@
        /// </summary>
        [SerializeField] private TiltRaceItemGenerator ItemGenerator;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// ��A�N�e�B�u�ɂ���A�C�e���� ID ���X�g
        /// </summary>
        private List<int> mDeactiveItemIdList = new List<int>();


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �A�N�e�B�u�ȃA�C�e���̓����蔻��f�[�^���X�g
        /// </summary>
        public IReadOnlyList<ITiltRaceItemCollision> ActiveItemCollisionList => ItemGenerator.ActiveItemList;


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// ������
        /// </summary>
        public void Initialize()
        {
            ItemGenerator.Initialize();
        }

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        public void Setup()
        {
            ItemGenerator.Setup();
        }

        /// <summary>
        /// �J�n
        /// </summary>
        public void Begin()
        {
            ItemGenerator.Begin();
        }

        /// <summary>
        /// �ꎞ��~
        /// </summary>
        public void Pause()
        {
            ItemGenerator.Pause();
        }

        /// <summary>
        /// �ĊJ
        /// </summary>
        public void Resume()
        {
            ItemGenerator.Resume();
        }

        /// <summary>
        /// �A�C�e���̍��W�X�V
        /// </summary>
        public void UpdatePosition()
        {
            mDeactiveItemIdList.Clear();

            bool existsDeactiveItem = false;

            // �A�N�e�B�u�ȃA�C�e���̍��W�X�V
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

            // �w����W���z�����A�C�e�����A�N�e�B�u��
            if (existsDeactiveItem)
            {
                ItemGenerator.SetDeactiveItem(mDeactiveItemIdList);
            }
        }

        /// <summary>
        /// �w�肳�ꂽ�A�C�e�����A�N�e�B�u�ɂ���
        /// </summary>
        /// <param name="itemIdList"> �Ώۂ̃A�C�e���� ID ���X�g </param>
        public void SetDeactiveItem(int id)
        {
            ItemGenerator.SetDeactiveItem(id);
        }
    }
}
