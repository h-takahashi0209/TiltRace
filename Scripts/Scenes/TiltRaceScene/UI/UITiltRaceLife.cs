using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 残ライフ UI
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class UITiltRaceLife : ExMonoBehaviour
    {
        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// ライフ
        /// </summary>
        private int mLife;


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// ライフアイコンリスト
        /// </summary>
        [SerializeField] private UITiltRaceLifeIcon[] UILifeIconList;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            UILifeIconList = transform.root.GetComponentsInChildren<UITiltRaceLifeIcon>(true);
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
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
        /// ライフ設定
        /// </summary>
        /// <param name="life"> ライフ </param>
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
        //! 関数（private）
        //====================================

        /// <summary>
        /// ライフ増加
        /// </summary>
        /// <param name="afterLife"> 増加後のライフライフ </param>
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
        /// ライフ減少
        /// </summary>
        /// <param name="afterLife"> 減少後のライフライフ </param>
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
