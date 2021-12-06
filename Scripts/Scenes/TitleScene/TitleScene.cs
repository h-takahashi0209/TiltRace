using UnityEngine;


namespace TakahashiH.Scenes.TitleScene
{
    /// <summary>
    /// �^�C�g�����
    /// </summary>
    public sealed class TitleScene : SceneBase
    {
        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// ��ʑS�̂𕢂��{�^��
        /// </summary>
        [SerializeField] private UIButton ScreenButton;


        //====================================
        //! �֐��iSceneBase�j
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
        //! �֐��iprivate�j
        //====================================

        /// <summary>
        /// ���̃V�[���֑J��
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
