using System;
using UnityEngine;


namespace TakahashiH
{
    /// <summary>
    /// �V�[�����Ƃ̃T�E���h�f�[�^
    /// </summary>
    [CreateAssetMenu(fileName = nameof(SceneSoundData), menuName = "ScriptableObjects/System/Sound/" + nameof(SceneSoundData))]
    public sealed class SceneSoundData : ScriptableObject
    {
        //====================================
        //! ��`
        //====================================

        /// <summary>
        /// AudioClip �P�̃f�[�^
        /// </summary>
        [Serializable]
        public class AudioClipData
        {
            public string       Name;
            public AudioClip    AudioClip;
        }


        //====================================
        //! �ϐ��ipublic�j
        //====================================

        /// <summary>
        /// SE �� AudioClip ���X�g
        /// </summary>
        public AudioClipData[] SeAudioClipDataList;

        /// <summary>
        /// BGM �� AudioClip ���X�g
        /// </summary>
        public AudioClipData[] BgmAudioClipDataList;
    }
}
