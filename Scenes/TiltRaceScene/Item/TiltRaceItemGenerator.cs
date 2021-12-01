using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - アイテム生成機
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRaceItemGenerator : ExMonoBehaviour
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// アイテム種別ごとの生成確率データ
        /// </summary>
        private struct GenProbabilityData
        {
            /// <summary>
            /// アイテム種別
            /// </summary>
            public ItemType ItemType;

            /// <summary>
            /// 生成確率下限
            /// </summary>
            public int ProbabilityMin;

            /// <summary>
            /// 生成確率上限
            /// </summary>
            public int ProbabilityMax;
        }


        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// アイテムの生成座標 Transform
        /// </summary>
        [SerializeField] private Transform GenerateItemTransform;

        /// <summary>
        /// アイテムリスト
        /// </summary>
        [SerializeField] private TiltRaceItem[] ItemList;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// アクティブなアイテムリスト
        /// </summary>
        private List<TiltRaceItem> mActiveItemList = new List<TiltRaceItem>();

        /// <summary>
        /// 待機中のアイテムリスト
        /// </summary>
        private List<TiltRaceItem> mWaitItemList = new List<TiltRaceItem>();

        /// <summary>
        /// アイテムのスプライトリスト
        /// </summary>
        private Sprite[] mItemSpriteList = new Sprite[(int)ItemType.Sizeof];

        /// <summary>
        /// アイテム種別ごとの生成確率リスト
        /// </summary>
        private GenProbabilityData[] mGenProbabilityList = new GenProbabilityData[(int)EnemyCarMovePatternType.Sizeof];

        /// <summary>
        /// 待機コルーチン
        /// </summary>
        private IEnumerator mWaitEnumerator;

        /// <summary>
        /// 生成確率合計
        /// </summary>
        private int mTotalGenProbability;

        /// <summary>
        /// 生成数
        /// </summary>
        private int mGeneratedCount;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// アクティブなアイテムリスト
        /// </summary>
        public IReadOnlyList<TiltRaceItem> ActiveItemList => mActiveItemList;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            ItemList = this.GetRoot().GetComponentsInChildren<TiltRaceItem>(true);
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            for (int i = 0; i < (int)ItemType.Sizeof; i++)
            {
                var itemType = (ItemType)i;
                var sprite   = Resources.Load<Sprite>(Path.Scenes.TiltRaceScene.ItemImage.Format(itemType));
                var probData = TiltRaceSettings.Item.GenerateProbabilityList.FirstOrDefault(g => g.ItemType == itemType);

                mItemSpriteList[i] = sprite;

                mGenProbabilityList[i].ItemType       = itemType;
                mGenProbabilityList[i].ProbabilityMin = mTotalGenProbability + 1;
                mGenProbabilityList[i].ProbabilityMax = mTotalGenProbability + probData.Probability;

                mTotalGenProbability += probData.Probability;
            }
        }

        /// <summary>
        /// セットアップ
        /// </summary>
        public void Setup()
        {
            mActiveItemList .Clear();
            mWaitItemList   .Clear();

            for (int i = 0; i < ItemList.Length; i++)
            {
                var car = ItemList[i];

                mWaitItemList.Add(car);

                car.SetActive(false);
            }
        }

        /// <summary>
        /// 生成開始
        /// </summary>
        public void Begin()
        {
            float genIntervalTimeSec = GetRandomGenIntervalTimeSec();

            mWaitEnumerator = CoroutineManager.Instance.CallWaitForSeconds(genIntervalTimeSec, () =>
            {
                Generate();
            });
        }

        /// <summary>
        /// 一時停止
        /// </summary>
        public void Pause()
        {
            CoroutineManager.Instance.PauseCoroutie(mWaitEnumerator);
        }

        /// <summary>
        /// 再開
        /// </summary>
        public void Resume()
        {
            CoroutineManager.Instance.ResumeCoroutie(mWaitEnumerator);
        }

        /// <summary>
        /// 指定されたアイテムを非アクティブにする
        /// </summary>
        /// <param name="targetIdList"> 対象のアイテムの ID リスト </param>
        public void SetDeactiveItem(IReadOnlyList<int> targetIdList)
        {
            for (int i = 0; i < targetIdList.Count; i++)
            {
                var targetId = targetIdList[i];

                SetDeactiveItem(targetId);
            };
        }

        /// <summary>
        /// 指定されたアイテムを非アクティブにする
        /// </summary>
        /// <param name="targetId"> 対象のアイテムの ID </param>
        public void SetDeactiveItem(int targetId)
        {
            for (int i = 0; i < ActiveItemList.Count; i++)
            {
                var item = ActiveItemList[i];

                if (item.Id == targetId)
                {
                    item.SetActive(false);

                    mActiveItemList.Remove(item);
                    mWaitItemList.Add(item);

                    break;
                }
            }
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 生成
        /// </summary>
        private void Generate()
        {
            var item = mWaitItemList.Count > 0 ? mWaitItemList[0] : null;
            if (item == null) {
                return;
            }

            mWaitItemList.RemoveAt(0);

            mActiveItemList.Add(item);

            item.SetActive(true);

            var id          = mGeneratedCount;
            var itemType    = GetRandomItemType();
            var sprite      = mItemSpriteList[(int)itemType];
            var position    = GetRandomPosition();

            item.Setup(id, itemType, sprite, position);

            mGeneratedCount++;

            Begin();
        }

        /// <summary>
        /// ランダム選出したアイテム種別取得
        /// </summary>
        private ItemType GetRandomItemType()
        {
            int lotteryProb = Random.Range(1, mTotalGenProbability);

            for (int i = 0; i < mGenProbabilityList.Length; i++)
            {
                var probData = mGenProbabilityList[i];

                if (lotteryProb >= probData.ProbabilityMin && lotteryProb <= probData.ProbabilityMax)
                {
                    return probData.ItemType;
                }
            }

            return ItemType.None;
        }

        /// <summary>
        /// ランダム算出した生成座標取得
        /// </summary>
        private Vector3 GetRandomPosition()
        {
            return new Vector3(Random.Range(-TiltRaceSettings.WidthLimit, TiltRaceSettings.WidthLimit), GenerateItemTransform.localPosition.y, 0f);
        }

        /// <summary>
        /// ランダム算出した生成間隔時間（秒）取得
        /// </summary>
        private float GetRandomGenIntervalTimeSec()
        {
            float minWaitTimeSec = Mathf.Max(TiltRaceSettings.Item.BaseGenerateTimeSec - TiltRaceSettings.Item.GenerateTimeRangeSec, 0f);
            float maxWaitTimeSec = TiltRaceSettings.Item.BaseGenerateTimeSec + TiltRaceSettings.Item.GenerateTimeRangeSec;

            return Random.Range(minWaitTimeSec, maxWaitTimeSec);
        }
    }
}
