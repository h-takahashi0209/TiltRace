using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �A�C�e�������@
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRaceItemGenerator : ExMonoBehaviour
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// �A�C�e����ʂ��Ƃ̐����m���f�[�^
        /// </summary>
        private struct GenProbabilityData
        {
            /// <summary>
            /// �A�C�e�����
            /// </summary>
            public ItemType ItemType;

            /// <summary>
            /// �����m������
            /// </summary>
            public int ProbabilityMin;

            /// <summary>
            /// �����m�����
            /// </summary>
            public int ProbabilityMax;
        }


        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// �A�C�e���̐������W Transform
        /// </summary>
        [SerializeField] private Transform GenerateItemTransform;

        /// <summary>
        /// �A�C�e�����X�g
        /// </summary>
        [SerializeField] private TiltRaceItem[] ItemList;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �A�N�e�B�u�ȃA�C�e�����X�g
        /// </summary>
        private List<TiltRaceItem> mActiveItemList = new List<TiltRaceItem>();

        /// <summary>
        /// �ҋ@���̃A�C�e�����X�g
        /// </summary>
        private List<TiltRaceItem> mWaitItemList = new List<TiltRaceItem>();

        /// <summary>
        /// �A�C�e���̃X�v���C�g���X�g
        /// </summary>
        private Sprite[] mItemSpriteList = new Sprite[(int)ItemType.Sizeof];

        /// <summary>
        /// �A�C�e����ʂ��Ƃ̐����m�����X�g
        /// </summary>
        private GenProbabilityData[] mGenProbabilityList = new GenProbabilityData[(int)EnemyCarMovePatternType.Sizeof];

        /// <summary>
        /// �ҋ@�R���[�`��
        /// </summary>
        private IEnumerator mWaitEnumerator;

        /// <summary>
        /// �����m�����v
        /// </summary>
        private int mTotalGenProbability;

        /// <summary>
        /// ������
        /// </summary>
        private int mGeneratedCount;


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �A�N�e�B�u�ȃA�C�e�����X�g
        /// </summary>
        public IReadOnlyList<TiltRaceItem> ActiveItemList => mActiveItemList;


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            ItemList = this.GetRoot().GetComponentsInChildren<TiltRaceItem>(true);
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// ������
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
        /// �Z�b�g�A�b�v
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
        /// �����J�n
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
        /// �ꎞ��~
        /// </summary>
        public void Pause()
        {
            CoroutineManager.Instance.PauseCoroutie(mWaitEnumerator);
        }

        /// <summary>
        /// �ĊJ
        /// </summary>
        public void Resume()
        {
            CoroutineManager.Instance.ResumeCoroutie(mWaitEnumerator);
        }

        /// <summary>
        /// �w�肳�ꂽ�A�C�e�����A�N�e�B�u�ɂ���
        /// </summary>
        /// <param name="targetIdList"> �Ώۂ̃A�C�e���� ID ���X�g </param>
        public void SetDeactiveItem(IReadOnlyList<int> targetIdList)
        {
            for (int i = 0; i < targetIdList.Count; i++)
            {
                var targetId = targetIdList[i];

                SetDeactiveItem(targetId);
            };
        }

        /// <summary>
        /// �w�肳�ꂽ�A�C�e�����A�N�e�B�u�ɂ���
        /// </summary>
        /// <param name="targetId"> �Ώۂ̃A�C�e���� ID </param>
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
        //! �֐��iprivate�j
        //====================================

        /// <summary>
        /// ����
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
        /// �����_���I�o�����A�C�e����ʎ擾
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
        /// �����_���Z�o�����������W�擾
        /// </summary>
        private Vector3 GetRandomPosition()
        {
            return new Vector3(Random.Range(-TiltRaceSettings.WidthLimit, TiltRaceSettings.WidthLimit), GenerateItemTransform.localPosition.y, 0f);
        }

        /// <summary>
        /// �����_���Z�o���������Ԋu���ԁi�b�j�擾
        /// </summary>
        private float GetRandomGenIntervalTimeSec()
        {
            float minWaitTimeSec = Mathf.Max(TiltRaceSettings.Item.BaseGenerateTimeSec - TiltRaceSettings.Item.GenerateTimeRangeSec, 0f);
            float maxWaitTimeSec = TiltRaceSettings.Item.BaseGenerateTimeSec + TiltRaceSettings.Item.GenerateTimeRangeSec;

            return Random.Range(minWaitTimeSec, maxWaitTimeSec);
        }
    }
}
