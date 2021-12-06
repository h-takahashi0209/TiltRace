using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 敵の車生成機
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRaceEnemyCarGenerator : ExMonoBehaviour
    {
        //====================================
        //! 定義
        //====================================

        /// <summary>
        /// 移動パターンごとの生成確率データ
        /// </summary>
        private struct GenProbabilityData
        {
            /// <summary>
            /// 移動パターン種別
            /// </summary>
            public EnemyCarMovePatternType MovePatternType;

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
        /// 車の生成座標 Transform
        /// </summary>
        [SerializeField] private Transform GenerateCarTransform;

        /// <summary>
        /// 車リスト
        /// </summary>
        [SerializeField] private TiltRaceEnemyCar[] CarList;


        //====================================
        //! 変数（private）
        //====================================

        /// <summary>
        /// アクティブな車リスト
        /// </summary>
        private List<TiltRaceEnemyCar> mActiveCarList = new List<TiltRaceEnemyCar>();

        /// <summary>
        /// 待機中の車リスト
        /// </summary>
        private List<TiltRaceEnemyCar> mWaitCarList = new List<TiltRaceEnemyCar>();

        /// <summary>
        /// 車のスプライトリスト
        /// </summary>
        private List<Sprite> mCarSpriteList = new List<Sprite>();

        /// <summary>
        /// 移動パターンごとの生成確率リスト
        /// </summary>
        private GenProbabilityData[] mGenProbabilityList = new GenProbabilityData[(int)EnemyCarMovePatternType.Sizeof];

        /// <summary>
        /// 待機コルーチン
        /// </summary>
        private IEnumerator mWaitEnumerator;

        /// <summary>
        /// 基準生成間隔時間（秒）
        /// </summary>
        private float mBaseGenIntervalTimeSec;

        /// <summary>
        /// 基準速度
        /// </summary>
        private float mBaseSpeed;

        /// <summary>
        /// 基準スケール
        /// </summary>
        private float mBaseScale;

        /// <summary>
        /// 基準停止時間（秒）
        /// </summary>
        private float mBaseStopTimeSec;

        /// <summary>
        /// 基準移動時間（秒）
        /// </summary>
        private float mBaseMoveTimeSec;

        /// <summary>
        /// 基準ホーミング力レート
        /// </summary>
        private float mBaseHormingPowerRate;

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
        /// アクティブな車リスト
        /// </summary>
        public IReadOnlyList<TiltRaceEnemyCar> ActiveCarList => mActiveCarList;


        //====================================
        //! 関数（MonoBehaviour）
        //====================================

        /// <summary>
        /// Reset
        /// </summary>
        private void Reset()
        {
            CarList = this.GetRoot().GetComponentsInChildren<TiltRaceEnemyCar>(true);
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 初期化
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

            // 設定ファイルから移動パターンごとの生成確率を取得
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
        /// セットアップ
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

            for (int i = 0; i < ActiveCarList.Count; i++)
            {
                ActiveCarList[i].Pause();
            }
        }

        /// <summary>
        /// 再開
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
        /// レベルアップ
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
        /// 指定された車を非アクティブにする
        /// </summary>
        /// <param name="targetIdList"> 対象の車の ID リスト </param>
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
        //! 関数（private）
        //====================================

        /// <summary>
        /// 生成
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
        /// ランダム選出したスプライト取得
        /// </summary>
        private Sprite GetRandomSprite()
        {
            int idx = Random.Range(0, mCarSpriteList.Count - 1);

            return mCarSpriteList[idx];
        }

        /// <summary>
        /// ランダム算出した生成座標取得
        /// </summary>
        private Vector3 GetRandomPosition()
        {
            return new Vector3(Random.Range(-TiltRaceSettings.WidthLimit, TiltRaceSettings.WidthLimit), GenerateCarTransform.localPosition.y, 0f);
        }

        /// <summary>
        /// ランダム選出した移動パターン種別取得
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
        /// ランダム算出した生成間隔時間（秒）取得
        /// </summary>
        private float GetRandomGenIntervalTimeSec()
        {
            float minWaitTimeSec = Mathf.Max(mBaseGenIntervalTimeSec - TiltRaceSettings.Enemy.GenIntervalTimeSec.Range, TiltRaceSettings.Enemy.GenIntervalTimeSec.Min);
            float maxWaitTimeSec = mBaseGenIntervalTimeSec + TiltRaceSettings.Enemy.GenIntervalTimeSec.Range;

            return Random.Range(minWaitTimeSec, maxWaitTimeSec);
        }

        /// <summary>
        /// ランダム算出した速度取得
        /// </summary>
        private float GetRandomSpeed()
        {
            float minSpeed = Mathf.Max(mBaseSpeed - TiltRaceSettings.Enemy.Speed.Range, TiltRaceSettings.Enemy.Speed.Min);
            float maxSpeed = mBaseSpeed + TiltRaceSettings.Enemy.Speed.Range;

            return Random.Range(minSpeed, maxSpeed);
        }

        /// <summary>
        /// ランダム算出したスケール取得
        /// </summary>
        private float GetRandomScale()
        {
            float minScale = Mathf.Max(mBaseScale - TiltRaceSettings.Enemy.Scale.Range, TiltRaceSettings.Enemy.Scale.Min);
            float maxScale = mBaseScale + TiltRaceSettings.Enemy.Scale.Range;

            return Random.Range(minScale, maxScale);
        }

        /// <summary>
        /// ランダム算出した移動時間（秒）取得
        /// </summary>
        private float GetRandomMoveTimeSec()
        {
            float minMoveTimeSec = Mathf.Max(mBaseMoveTimeSec - TiltRaceSettings.Enemy.MoveTimeSec.Range, TiltRaceSettings.Enemy.MoveTimeSec.Min);
            float maxMoveTimeSec = mBaseMoveTimeSec + TiltRaceSettings.Enemy.MoveTimeSec.Range;
            
            return Random.Range(minMoveTimeSec, maxMoveTimeSec);
        }

        /// <summary>
        /// ランダム算出した停止時間（秒）取得
        /// </summary>
        private float GetRandomStopTimeSec()
        {
            float minStopTimeSec = Mathf.Max(mBaseStopTimeSec - TiltRaceSettings.Enemy.StopTimeSec.Range, TiltRaceSettings.Enemy.StopTimeSec.Min);
            float maxStopTimeSec = mBaseStopTimeSec + TiltRaceSettings.Enemy.StopTimeSec.Range;

            return Random.Range(minStopTimeSec, maxStopTimeSec);
        }

        /// <summary>
        /// ランダム算出したホーミング力レート取得
        /// </summary>
        private float GetRandomHormingPowerRate()
        {
            float minHormingPowerRate = Mathf.Max(mBaseHormingPowerRate - TiltRaceSettings.Enemy.HormingPowerRate.Range, TiltRaceSettings.Enemy.HormingPowerRate.Min);
            float maxHormingPowerRate = mBaseHormingPowerRate + TiltRaceSettings.Enemy.HormingPowerRate.Range;

            return Random.Range(minHormingPowerRate, maxHormingPowerRate);
        }
    }
}
