using UnityEngine.SceneManagement;

using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;


namespace TakahashiH
{
    /// <summary>
    /// �V�[���Ǘ�
    /// </summary>
    public static class SceneManager
    {
        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �풓 UI �V�[����ǂݍ��ݍς݂�
        /// </summary>
        public static bool IsLoadedResidentUIScene { get; private set; }


        //====================================
        //! �֐��ipublic static�j
        //====================================

        /// <summary>
        /// �ǂݍ���
        /// </summary>
        /// <param name="sceneType"> �V�[����� </param>
        public static void Load(SceneType sceneType)
        {
            UnitySceneManager.LoadSceneAsync(sceneType.ToString(), LoadSceneMode.Single);
        }

        /// <summary>
        /// ���Z�ǂݍ���
        /// </summary>
        /// <param name="sceneType"> �V�[����� </param>
        public static void LoadAdditive(SceneType sceneType)
        {
            if (Exists(sceneType)) {
                return;
            }

            UnitySceneManager.LoadSceneAsync(sceneType.ToString(), LoadSceneMode.Additive);
        }

        /// <summary>
        /// �j��
        /// </summary>
        /// <param name="sceneType"> �V�[����� </param>
        public static void Unload(SceneType sceneType)
        {
            if (!Exists(sceneType)) {
                return;
            }

            UnitySceneManager.UnloadSceneAsync(sceneType.ToString());
        }

        /// <summary>
        /// �풓 UI �V�[����ǂݍ��ݍς݂Ƃ���
        /// </summary>
        public static void SetLoadedResidentUIScene()
        {
            IsLoadedResidentUIScene = true;
        }


        //====================================
        //! �֐��iprivate static�j
        //====================================

        /// <summary>
        /// �ǂݍ��ݍς݂̃V�[����
        /// </summary>
        /// <param name="sceneType"> �V�[����� </param>
        private static bool Exists(SceneType sceneType)
        {
            for (int i = 0; i < UnitySceneManager.sceneCount; i++)
            {
                var scene = UnitySceneManager.GetSceneAt(i);

                if(scene.name == sceneType.ToString())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
