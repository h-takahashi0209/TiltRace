using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �G�̎Ԑ����@
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRaceEnemyCarGenerator : ExMonoBehaviour
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// �ړ��p�^�[�����Ƃ̐����m���f�[�^
        /// </summary>
        private struct GenProbabilityData
        {
            /// <summary>
            /// �ړ��p�^�[�����
            /// </summary>
            public EnemyCarMovePatternType MovePatternType;

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
        /// �Ԃ̐������W Transform
        /// </summary>
        [SerializeField] private Transform GenerateCarTransform;

        /// <summary>
        /// �ԃ��X�g
        /// </summary>
        [SerializeField] private TiltRaceEnemyCar[] CarList;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �A�N�e�B�u�Ȏԃ��X�g
        /// </summary>
        private List<TiltRaceEnemyCar> mActiveCarList = new List<TiltRaceEnemyCar>();

        /// <summary>
        /// �ҋ@���̎ԃ��X�g
        /// </summary>
        private List<TiltRaceEnemyCar> mWaitCarList = new List<TiltRaceEnemyCar>();

        /// <summary>
        /// �Ԃ̃X�v���C�g���X�g
        /// </summary>
        private List<Sprite> mCarSpriteList = new List<Sprite>();

        /// <summary>
        /// �ړ��p�^�[�����Ƃ̐����m�����X�g
        /// </summary>
        private GenProbabilityData[] mGenProbabilityList = new GenProbabilityData[(int)EnemyCarMovePatternType.Sizeof];

        /// <summary>
        /// �ҋ@�R���[�`��
        /// </summary>
        private IEnumerator mWaitEnumerator;

        /// <summary>
        /// ������Ԋu���ԁi�b�j
        /// </summary>
        private float mBaseGenIntervalTimeSec;

        /// <summary>
        /// ����x
        /// </summary>
        private float mBaseSpeed;

        /// <summary>
        /// ��X�P�[��
        /// </summary>
        private float mBaseScale;

        /// <summary>
        /// ���~���ԁi�b�j
        /// </summary>
        private float mBaseStopTimeSec;

        /// <summary>
        /// ��ړ����ԁi�b�j
        /// </summary>
        private float mBaseMoveTimeSec;

        /// <summary>
        /// ��z�[�~���O�̓��[�g
        /// </summary>
        private float mBaseHormingPowerRate;

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
        /// �A�N�e�B�u�Ȏԃ��X�g
        /// </summary>
        public IReadOnlyList<TiltRaceEnemyCar> ActiveCarList => mActiveCarList;


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            CarList = this.GetRoot().GetComponentsInChildren<TiltRaceEnemyCar>(true);
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// ������
        /// </summary>
        public void Initialize()
        {
            for (int i = 0; i < (int)CarColor.Sizeof; i++)
            {
                var carColor = (CarColor)i;

                if (carColor == TiltRaceSettings.Player.CarColor) {
                    continue;
                }

                var sprite = Resources.Load<Sprite>(Path.Scenes.TiltRaceScene.CarImage.Format(carColor));

                mCarSpriteList.Add(sprite);
            }

            for (int i = 0; i < CarList.Length; i++)
            {
                CarList[i].Initialize();
            }

            // �ݒ�t�@�C������ړ��p�^�[�����Ƃ̐����m�����擾
            for (int i = 0; i < (int)EnemyCarMovePatternType.Sizeof; i++)
            {
                var movePatternType = (EnemyCarMovePatternType)i;
                var probData        = TiltRaceSettings.Enemy.GenerateProbabilityList.FirstOrDefault(g => g.MovePatternType == movePatternType);

                mGenProbabilityList[i].MovePatternType = movePatternType;
                mGenProbabilityList[i].ProbabilityMin  = mTotalGenProbability + 1;
                mGenProbabilityList[i].ProbabilityMax  = mTotalGenProbability + probData.Probability;

                mTotalGenProbability += probData.Probability;
            }
        }

        /// <summary>
        /// �Z�b�g�A�b�v
        /// </summary>
        public void Setup()
        {
            mActiveCarList .Clear();
            mWaitCarList   .Clear();

            for (int i = 0; i < CarList.Length; i++)
            {
                var car = CarList[i];

                mWaitCarList.Add(car);

                car.SetActive(false);
            }

            mBaseGenIntervalTimeSec = TiltRaceSettings.Enemy.GenIntervalTimeSec .Def;
            mBaseSpeed              = TiltRaceSettings.Enemy.Speed              .Def;
            mBaseScale              = TiltRaceSettings.Enemy.Scale              .Def;
            mBaseMoveTimeSec        = TiltRaceSettings.Enemy.MoveTimeSec        .Def;
            mBaseStopTimeSec        = TiltRaceSettings.Enemy.StopTimeSec        .Def;
            mBaseHormingPowerRate   = TiltRaceSettings.Enemy.HormingPowerRate   .Def;
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

            for (int i = 0; i < ActiveCarList.Count; i++)
            {
                ActiveCarList[i].Pause();
            }
        }

        /// <summary>
        /// �ĊJ
        /// </summary>
        public void Resume()
        {
            CoroutineManager.Instance.ResumeCoroutie(mWaitEnumerator);

            for(int i = 0;i < ActiveCarList.Count;i++)
            {
                ActiveCarList[i].Resume();
            }
        }

        /// <summary>
        /// ���x���A�b�v
        /// </summary>
        public void LevelUp()
        {
            var enemySettings = TiltRaceSettings.Enemy;

            mBaseGenIntervalTimeSec = Mathf.Max(mBaseGenIntervalTimeSec - enemySettings.GenIntervalTimeSec.LevelUp, enemySettings.GenIntervalTimeSec.Min);

            mBaseSpeed              += enemySettings.Speed             .LevelUp;
            mBaseScale              += enemySettings.Scale             .LevelUp;
            mBaseMoveTimeSec        += enemySettings.MoveTimeSec       .LevelUp;
            mBaseStopTimeSec        += enemySettings.StopTimeSec       .LevelUp;
            mBaseHormingPowerRate   += enemySettings.HormingPowerRate  .LevelUp;
        }

        /// <summary>
        /// �w�肳�ꂽ�Ԃ��A�N�e�B�u�ɂ���
        /// </summary>
        /// <param name="targetIdList"> �Ώۂ̎Ԃ� ID ���X�g </param>
        public void SetDeactiveCar(IReadOnlyList<int> targetIdList)
        {
            for (int i = 0; i < targetIdList.Count; i++)
            {
                var targetId = targetIdList[i];

                for (int j = 0; j < ActiveCarList.Count; j++)
                {
                    var car = ActiveCarList[j];

                    if (car.Id == targetId)
                    {
                        car.SetActive(false);

                        mActiveCarList.Remove(car);
                        mWaitCarList.Add(car);
                    }
                }
            };
        }


        //====================================
        //! �֐��iprivate�j
        //====================================

        /// <summary>
        /// ����
        /// </summary>
        private void Generate()
        {
            var car = mWaitCarList.Count > 0 ? mWaitCarList[0] : null;
            if (car == null) {
                return;
            }

            mWaitCarList.RemoveAt(0);

            mActiveCarList.Add(car);

            car.SetActive(true);

            var id                  = mGeneratedCount;
            var sprite              = GetRandomSprite();
            var position            = GetRandomPosition();
            var patternType         = GetRandomMovePatternType();
            var damage              = TiltRaceSettings.Enemy.Damage;
            var speed               = GetRandomSpeed();
            var scale               = GetRandomScale();
            var moveTimeSec         = GetRandomMoveTimeSec();
            var stopTimeSec         = GetRandomStopTimeSec();
            var hormingPowerRate    = GetRandomHormingPowerRate();

            car.Setup(id, sprite, position, patternType, damage, speed, scale, moveTimeSec, stopTimeSec, hormingPowerRate);

            mGeneratedCount++;

            Begin();
        }

        /// <summary>
        /// �����_���I�o�����X�v���C�g�擾
        /// </summary>
        private Sprite GetRandomSprite()
        {
            int idx = Random.Range(0, mCarSpriteList.Count - 1);

            return mCarSpriteList[idx];
        }

        /// <summary>
        /// �����_���Z�o�����������W�擾
        /// </summary>
        private Vector3 GetRandomPosition()
        {
            return new Vector3(Random.Range(-TiltRaceSettings.WidthLimit, TiltRaceSettings.WidthLimit), GenerateCarTransform.localPosition.y, 0f);
        }

        /// <summary>
        /// �����_���I�o�����ړ��p�^�[����ʎ擾
        /// </summary>
        private EnemyCarMovePatternType GetRandomMovePatternType()
        {
            int lotteryProb = Random.Range(1, mTotalGenProbability);

            for (int i = 0; i < mGenProbabilityList.Length; i++)
            {
                var probData = mGenProbabilityList[i];

                if (lotteryProb >= probData.ProbabilityMin && lotteryProb <= probData.ProbabilityMax)
                {
                    return probData.MovePatternType;
                }
            }

            return EnemyCarMovePatternType.Liner;
        }

        /// <summary>
        /// �����_���Z�o���������Ԋu���ԁi�b�j�擾
        /// </summary>
        private float GetRandomGenIntervalTimeSec()
        {
            float minWaitTimeSec = Mathf.Max(mBaseGenIntervalTimeSec - TiltRaceSettings.Enemy.GenIntervalTimeSec.Range, TiltRaceSettings.Enemy.GenIntervalTimeSec.Min);
            float maxWaitTimeSec = mBaseGenIntervalTimeSec + TiltRaceSettings.Enemy.GenIntervalTimeSec.Range;

            return Random.Range(minWaitTimeSec, maxWaitTimeSec);
        }

        /// <summary>
        /// �����_���Z�o�������x�擾
        /// </summary>
        private float GetRandomSpeed()
        {
            float minSpeed = Mathf.Max(mBaseSpeed - TiltRaceSettings.Enemy.Speed.Range, TiltRaceSettings.Enemy.Speed.Min);
            float maxSpeed = mBaseSpeed + TiltRaceSettings.Enemy.Speed.Range;

            return Random.Range(minSpeed, maxSpeed);
        }

        /// <summary>
        /// �����_���Z�o�����X�P�[���擾
        /// </summary>
        private float GetRandomScale()
        {
            float minScale = Mathf.Max(mBaseScale - TiltRaceSettings.Enemy.Scale.Range, TiltRaceSettings.Enemy.Scale.Min);
            float maxScale = mBaseScale + TiltRaceSettings.Enemy.Scale.Range;

            return Random.Range(minScale, maxScale);
        }

        /// <summary>
        /// �����_���Z�o�����ړ����ԁi�b�j�擾
        /// </summary>
        private float GetRandomMoveTimeSec()
        {
            float minMoveTimeSec = Mathf.Max(mBaseMoveTimeSec - TiltRaceSettings.Enemy.MoveTimeSec.Range, TiltRaceSettings.Enemy.MoveTimeSec.Min);
            float maxMoveTimeSec = mBaseMoveTimeSec + TiltRaceSettings.Enemy.MoveTimeSec.Range;
            
            return Random.Range(minMoveTimeSec, maxMoveTimeSec);
        }

        /// <summary>
        /// �����_���Z�o������~���ԁi�b�j�擾
        /// </summary>
        private float GetRandomStopTimeSec()
        {
            float minStopTimeSec = Mathf.Max(mBaseStopTimeSec - TiltRaceSettings.Enemy.StopTimeSec.Range, TiltRaceSettings.Enemy.StopTimeSec.Min);
            float maxStopTimeSec = mBaseStopTimeSec + TiltRaceSettings.Enemy.StopTimeSec.Range;

            return Random.Range(minStopTimeSec, maxStopTimeSec);
        }

        /// <summary>
        /// �����_���Z�o�����z�[�~���O�̓��[�g�擾
        /// </summary>
        private float GetRandomHormingPowerRate()
        {
            float minHormingPowerRate = Mathf.Max(mBaseHormingPowerRate - TiltRaceSettings.Enemy.HormingPowerRate.Range, TiltRaceSettings.Enemy.HormingPowerRate.Min);
            float maxHormingPowerRate = mBaseHormingPowerRate + TiltRaceSettings.Enemy.HormingPowerRate.Range;

            return Random.Range(minHormingPowerRate, maxHormingPowerRate);
        }
    }
}
