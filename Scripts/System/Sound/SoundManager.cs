using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// �T�E���h�Ǘ�
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SoundManager : ExMonoBehaviour
    {
        //====================================
        //! �ϐ��iprivate static�j
        //====================================
        private static SoundManager msInstance;


        //====================================
        //! �ϐ��iSerializeField�j
        //====================================

        /// <summary>
        /// SE �� AudioSource ���X�g
        /// </summary>
        [SerializeField] private AudioSource[] SeAudioSourceList;

        /// <summary>
        /// BGM �� AudioSource
        /// </summary>
        [SerializeField] private AudioSource[] BgmAudioSourceList;


        //====================================
        //! �ϐ��iprivate�j
        //====================================

        /// <summary>
        /// �풓�T�E���h�f�[�^
        /// </summary>
        private SceneSoundData mResidentSoundData;

        /// <summary>
        /// �V�[�����Ƃ̃T�E���h�f�[�^
        /// </summary>
        private SceneSoundData mSceneSoundData;


        //====================================
        //! �֐��iMonoBehaviour�j
        //====================================

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            if(msInstance && msInstance != this)
            {
                Destroy(this);
            }

            msInstance = this;
        }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �V�[�����Ƃ̃T�E���h�f�[�^�ǂݍ���
        /// </summary>
        /// <param name="sceneType"> �V�[����� </param>
        public static void LoadSceneSoundData(SceneType sceneType)
        {
            if (msInstance == null || msInstance.mSceneSoundData != null) {
                return;
            }

            var sceneSoundData = Resources.Load<SceneSoundData>(Path.Common.SoundData.Format(sceneType.ToString()));
            if (sceneSoundData == null) {
                return;
            }

            msInstance.mSceneSoundData = sceneSoundData;
        }

        /// <summary>
        /// �풓�T�E���h�f�[�^�ǂݍ���
        /// </summary>
        public static void LoadResidentSoundData()
        {
            if (msInstance == null || msInstance.mResidentSoundData != null) {
                return;
            }

            var sceneSoundData = Resources.Load<SceneSoundData>(Path.Common.SoundData.Format(SceneType.ResidentScene.ToString()));
            if (sceneSoundData == null) {
                return;
            }

            msInstance.mResidentSoundData = sceneSoundData;
        }

        /// <summary>
        /// �V�[�����Ƃ̃T�E���h�f�[�^�j��
        /// </summary>
        public static void DisposeSceneSoundData()
        {
            if (msInstance == null) {
                return;
            }

            msInstance.mSceneSoundData = null;
        }

        /// <summary>
        /// SE �Đ�
        /// </summary>
        /// <param name="soundName">    �T�E���h��          </param>
        /// <param name="isLoop">       ���[�v�����邩      </param>
        /// <returns>                   �T�E���h�n���h��    </returns>
        public static ISoundHandle PlaySe(string soundName, bool isLoop = false)
        {
            return Play(SoundType.Se, soundName, isLoop);
        }

        /// <summary>
        /// BGM �Đ�
        /// </summary>
        /// <param name="soundName">    �T�E���h��          </param>
        /// <param name="isLoop">       ���[�v�����邩      </param>
        /// <returns>                   �T�E���h�n���h��    </returns>
        public static ISoundHandle PlayBgm(string soundName, bool isLoop = false)
        {
            return Play(SoundType.Bgm, soundName, isLoop);
        }

        /// <summary>
        /// ��~
        /// </summary>
        /// <param name="soundHandle"> �T�E���h�n���h�� </param>
        public static void Stop(ISoundHandle soundHandle)
        {
            if (msInstance == null) {
                return;
            }

            var audioSource = GetAudioSource(soundHandle);
            if (audioSource == null) {
                return;
            }

            audioSource.Stop();
        }

        /// <summary>
        /// �ꎞ��~
        /// </summary>
        /// <param name="soundHandle"> �T�E���h�n���h�� </param>
        public static void Pause(ISoundHandle soundHandle)
        {
            if (msInstance == null) {
                return;
            }

            var audioSource = GetAudioSource(soundHandle);
            if (audioSource == null) {
                return;
            }

            audioSource.Pause();
        }

        /// <summary>
        /// �ĊJ
        /// </summary>
        /// <param name="soundHandle"> �T�E���h�n���h�� </param>
        public static void Resume(ISoundHandle soundHandle)
        {
            if (msInstance == null) {
                return;
            }

            var audioSource = GetAudioSource(soundHandle);
            if (audioSource == null) {
                return;
            }

            audioSource.UnPause();
        }


        //====================================
        //! �֐��iprivate static�j
        //====================================

        /// <summary>
        /// �Đ�
        /// </summary>
        /// <param name="soundType">    �T�E���h���        </param>
        /// <param name="soundName">    �T�E���h��          </param>
        /// <param name="isLoop">       ���[�v�����邩      </param>
        /// <returns>                   �T�E���h�n���h��    </returns>
        private static ISoundHandle Play(SoundType soundType, string soundName, bool isLoop = false)
        {
            var audioClip = GetAudioClip(soundType, soundName);
            if (audioClip == null)
            {
                return SoundHandle.Empty;
            }

            int audioSourceIdx = soundType switch
            {
                SoundType.Se    => msInstance.SeAudioSourceList  .FindIndex(a => !a.isPlaying),
                SoundType.Bgm   => msInstance.BgmAudioSourceList .FindIndex(a => !a.isPlaying),
                _               => -1,
            };

            if (audioSourceIdx < 0)
            {
                return SoundHandle.Empty;
            }

            var audioSource = soundType switch
            {
                SoundType.Se    => msInstance.SeAudioSourceList[audioSourceIdx],
                SoundType.Bgm   => msInstance.BgmAudioSourceList[audioSourceIdx],
                _               => null,
            };

            audioSource.loop = isLoop || soundType == SoundType.Bgm;
            audioSource.clip = audioClip;

            audioSource.Play();

            return new SoundHandle(soundType, audioSourceIdx);
        }

        /// <summary>
        /// �I�[�f�B�I�N���b�v�擾
        /// </summary>
        /// <param name="soundType">    �T�E���h���    </param>
        /// <param name="soundName">    �T�E���h��      </param>
        private static AudioClip GetAudioClip(SoundType soundType, string soundName)
        {
            AudioClip audioClip = null;

            if (msInstance.mSceneSoundData != null)
            {
                audioClip = soundType switch
                {
                    SoundType.Se    => msInstance.mSceneSoundData.SeAudioClipDataList  .FirstOrDefault(s => s.Name == soundName)?.AudioClip ?? null,
                    SoundType.Bgm   => msInstance.mSceneSoundData.BgmAudioClipDataList .FirstOrDefault(s => s.Name == soundName)?.AudioClip ?? null,
                    _               => null,
                };
            }

            if (audioClip == null && msInstance.mResidentSoundData != null)
            {
                audioClip = soundType switch
                {
                    SoundType.Se    => msInstance.mResidentSoundData.SeAudioClipDataList  .FirstOrDefault(s => s.Name == soundName)?.AudioClip ?? null,
                    SoundType.Bgm   => msInstance.mResidentSoundData.BgmAudioClipDataList .FirstOrDefault(s => s.Name == soundName)?.AudioClip ?? null,
                    _               => null,
                };
            }

            return audioClip;
        }

        /// <summary>
        /// �I�[�f�B�I�\�[�X�擾
        /// </summary>
        /// <param name="soundHandle"> �T�E���h�n���h�� </param>
        private static AudioSource GetAudioSource(ISoundHandle soundHandle)
        {
            if (soundHandle == null)
            {
                return null;
            }

            return soundHandle.SoundType switch
            {
                SoundType.Se  => msInstance.SeAudioSourceList  .ElementAtOrDefault(soundHandle.AudioSourceIndex),
                SoundType.Bgm => msInstance.BgmAudioSourceList .ElementAtOrDefault(soundHandle.AudioSourceIndex),
                _             => null,
            };
        }
    }
}
