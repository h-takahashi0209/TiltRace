
namespace TakahashiH.Scenes.Resident
{
    /// <summary>
    /// �풓�V�[��
    /// </summary>
    public sealed class ResidentScene : SceneBase
    {
        //====================================
        //! �֐��iSceneBase�j
        //====================================

        /// <summary>
        /// DoAwake
        /// </summary>
        protected override void DoAwake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
