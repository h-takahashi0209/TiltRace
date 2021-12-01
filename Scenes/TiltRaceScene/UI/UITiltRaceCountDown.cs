using System;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - カウントダウン UI
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public sealed class UITiltRaceCountDown : ExMonoBehaviour
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// アニメーター
        /// </summary>
        [SerializeField] private Animator Animator;


        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 完了時コールバック
        /// </summary>
        public Action OnComplete { private get; set; }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 開始
        /// </summary>
        public void Begin()
        {
            this.SetActive(true);

            Animator.SetTrigger("Once");
        }

        /// <summary>
        /// アニメーションイベント - 初回カウントダウン
        /// </summary>
        public void AnimationEvent_FirstCountDown()
        {
            SoundManager.PlaySe(SoundDef.TiltRaceScene.Se.Engine .ToString());
            SoundManager.PlaySe(SoundDef.TiltRaceScene.Se.Count  .ToString());
        }

        /// <summary>
        /// アニメーションイベント - カウントダウン
        /// </summary>
        public void AnimationEvent_CountDown()
        {
            SoundManager.PlaySe(SoundDef.TiltRaceScene.Se.Count.ToString());
        }

        /// <summary>
        /// アニメーションイベント - 完了時コールバック呼び出し
        /// </summary>
        public void AnimationEvent_CallOnComplete()
        {
            SoundManager.PlaySe(SoundDef.TiltRaceScene.Se.Start.ToString());

            OnComplete?.Invoke();
        }
    }
}
