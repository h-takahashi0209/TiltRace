using UnityEngine;


namespace TakahashiH.Scenes.TitleScene
{
    /// <summary>
    /// タイトル画面
    /// </summary>
    public sealed class TitleScene : SceneBase
    {
        //====================================
        //! 変数（SerializeField）
        //====================================

        /// <summary>
        /// 画面全体を覆うボタン
        /// </summary>
        [SerializeField] private UIButton ScreenButton;


        //====================================
        //! 関数（SceneBase）
        //====================================

        /// <summary>
        /// DoStart
        /// </summary>
        protected override void DoStart()
        {
            UIFade.FadeIn(() =>
            {
                ScreenButton.OnClick = () => TransitionNextScene();
            });
        }


        //====================================
        //! 関数（private）
        //====================================

        /// <summary>
        /// 次のシーンへ遷移
        /// </summary>
        private void TransitionNextScene()
        {
            SoundManager.PlaySe(SoundDef.ResidentScene.Se.ButtonDecide.ToString());

            UIFade.FadeOut(() =>
            {
                SceneManager.Load(SceneType.TiltRaceScene);
            });
        }
    }
}
