using System;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 走行距離ごとのイベント制御
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRaceDistanceEventController : ExMonoBehaviour
    {
        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// 現在のレベル
        /// </summary>
        private int mLevel;

        /// <summary>
        /// ライフ回復した回数
        /// </summary>
        private int mRecoveredLifeCount;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// レベルアップリクエスト
        /// </summary>
        public Action<int> OnReqLevelUp { private get; set; }

        /// <summary>
        /// ライフ回復リクエスト
        /// </summary>
        public Action<int> OnReqRecoveryLife { private get; set; }


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            OnReqLevelUp        = null;
            OnReqRecoveryLife   = null;
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// セットアップ
        /// </summary>
        public void Setup()
        {
            mLevel              = 0;
            mRecoveredLifeCount = 0;
        }

        /// <summary>
        /// 走行距離更新
        /// </summary>
        /// <param name="distance"> 走行距離 </param>
        public void UpdateDistance(float distance)
        {
            int nextLevel = (int)(distance / TiltRaceSettings.DistanceEvent.LevelUpDistanceInterval);

            // 一定間隔走行するごとにレベルアップ
            if (nextLevel > mLevel)
            {
                mLevel = nextLevel;

                OnReqLevelUp(mLevel);
            }

            int nextRecoveredLifeCount = (int)(distance / TiltRaceSettings.DistanceEvent.RecoveredLifeDistanceInterval);

            // 一定間隔走行するごとにライフ回復
            if (nextRecoveredLifeCount > mRecoveredLifeCount)
            {
                mRecoveredLifeCount = nextRecoveredLifeCount;

                OnReqRecoveryLife(TiltRaceSettings.DistanceEvent.RecoveredLife);
            }
        }
    }
}
