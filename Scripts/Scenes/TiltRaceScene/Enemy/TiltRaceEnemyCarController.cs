using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �G�̎Ԑ���
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRaceEnemyCarController : ExMonoBehaviour
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// �Ԃ��A�N�e�B�u�ɂ���Y���W
        /// </summary>
        private const float DeactivePosY = -2200f;


        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �Ԑ����@
        /// </summary>
        [SerializeField] private TiltRaceEnemyCarGenerator CarGenerator;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// ��A�N�e�B�u�ɂ���Ԃ� ID ���X�g
        /// </summary>
        private List<int> mDeactiveCarIdList = new List<int>();


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �A�N�e�B�u�ȎԂ̓����蔻��f�[�^���X�g
        /// </summary>
        public IReadOnlyList<ITiltRaceEnemyCarCollision> ActiveCarCollisionList => CarGenerator.ActiveCarList;


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// ������
        /// </summary>
        public void Initialize()
        {
            CarGenerator.Initialize();
        }

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        public void Setup()
        {
            CarGenerator.Setup();
        }

        /// <summary>
        /// �J�n
        /// </summary>
        public void Begin()
        {
            CarGenerator.Begin();
        }

        /// <summary>
        /// �ꎞ��~
        /// </summary>
        public void Pause()
        {
            CarGenerator.Pause();
        }

        /// <summary>
        /// �ĊJ
        /// </summary>
        public void Resume()
        {
            CarGenerator.Resume();
        }

        /// <summary>
        /// ���W�X�V
        /// </summary>
        /// <param name="playerCarPosition"> �v���C���[�̎Ԃ̍��W </param>
        public void UpdatePosition(Vector3 playerCarPosition)
        {
            mDeactiveCarIdList.Clear();

            bool existsDeactiveCar = false;

            // �A�N�e�B�u�ȎԂ̍��W�X�V
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

            // �w����W���z�����Ԃ��A�N�e�B�u��
            if (existsDeactiveCar)
            {
                CarGenerator.SetDeactiveCar(mDeactiveCarIdList);
            }
        }

        /// <summary>
        /// ���x���A�b�v
        /// </summary>
        public void LevelUp()
        {
            CarGenerator.LevelUp();
        }
    }
}
