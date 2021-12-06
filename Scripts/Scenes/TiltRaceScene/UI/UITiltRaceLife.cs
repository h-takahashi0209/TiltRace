using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �c���C�t UI
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UITiltRaceLife : ExMonoBehaviour
    {
        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// ���C�t
        /// </summary>
        private int mLife;


        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// ���C�t�A�C�R�����X�g
        /// </summary>
        [SerializeField] private UITiltRaceLifeIcon[] UILifeIconList;


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            UILifeIconList = transform.root.GetComponentsInChildren<UITiltRaceLifeIcon>(true);
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        public void Setup()
        {
            mLife = TiltRaceSettings.Player.DefLife;

            for (int i = 0; i < UILifeIconList.Length; i++)
            {
                var lifeObj = UILifeIconList[i];

                lifeObj.Setup(i < mLife);
            }
        }

        /// <summary>
        /// ���C�t�ݒ�
        /// </summary>
        /// <param name="life"> ���C�t </param>
        public void SetLife(int life)
        {
            if (life > mLife && life <= UILifeIconList.Length)
            {
                IncLife(life);
            }
            else if (life < mLife && life >= 0)
            {
                DecLife(life);
            }
        }


        //====================================
        //! �֐��iprivate�j
        //====================================

        /// <summary>
        /// ���C�t����
        /// </summary>
        /// <param name="afterLife"> ������̃��C�t���C�t </param>
        private void IncLife(int afterLife)
        {
            int recoveredLife = afterLife - mLife;

            mLife = afterLife;

            for (int i = 0; i < recoveredLife; i++)
            {
                int iconIdx = mLife - 1 + i;

                UILifeIconList[iconIdx].PlayAnimation(UITiltRaceLifeIcon.AnimType.Show);
            }
        }

        /// <summary>
        /// ���C�t����
        /// </summary>
        /// <param name="afterLife"> ������̃��C�t���C�t </param>
        private void DecLife(int afterLife)
        {
            int damagedLife = mLife - afterLife;

            mLife = afterLife;

            for (int i = 0; i < damagedLife; i++)
            {
                int iconIdx = mLife - i;

                UILifeIconList[iconIdx].PlayAnimation(UITiltRaceLifeIcon.AnimType.Hide);
            }
        }
    }
}
