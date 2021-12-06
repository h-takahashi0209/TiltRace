using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// �V�[�����
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class SceneBase : ExMonoBehaviour
    {
        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            // �풓�V�[������x�����ǂݍ���
            if (!SceneManager.IsLoadedResidentUIScene)
            {
                SceneManager.LoadAdditive(SceneType.ResidentScene);
            }

            SceneManager.SetLoadedResidentUIScene();

            // �풓�T�E���h��ǂݍ���
            SoundManager.LoadResidentSoundData();

            DoAwake();
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            DoStart();
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            DoUpdate();
        }

        /// <summary>
        /// OnDestroy
        /// </summary>
        private void OnDestroy()
        {
            DoOnDestroy();
        }


        //====================================
        //! �֐��iprotected virtual�j
        //====================================

        /// <summary>
        /// DoAwake
        /// </summary>
        protected virtual void DoAwake() {}

        /// <summary>
        /// DoStart
        /// </summary>
        protected virtual void DoStart() {}

        /// <summary>
        /// DoUpdate
        /// </summary>
        protected virtual void DoUpdate() {}

        /// <summary>
        /// DoOnDestroy
        /// </summary>
        protected virtual void DoOnDestroy() {}
    }
}
