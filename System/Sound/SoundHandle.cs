
namespace TakahashiH
{
    public interface ISoundHandle
    {
        public SoundType    SoundType           { get; }
        public int          AudioSourceIndex    { get; }
    }

    /// <summary>
    /// �T�E���h�n���h��
    /// </summary>
    public struct SoundHandle : ISoundHandle
    {
        //====================================
        //! �ϐ��ipublic static�j
        //====================================

        /// <summary>
        /// ��f�[�^
        /// </summary>
        public static SoundHandle Empty = new SoundHandle(SoundType.None, -1);


        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �T�E���h�n���h��
        /// </summary>
        public SoundType SoundType { get; private set; }

        /// <summary>
        /// �I�[�f�B�I�\�[�X�̃C���f�b�N�X
        /// </summary>
        public int AudioSourceIndex { get; private set; }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="soundType">           �T�E���h���                      </param>
        /// <param name="audioSourceIndex">    �I�[�f�B�I�\�[�X�̃C���f�b�N�X    </param>
        public SoundHandle(SoundType soundType, int audioSourceIndex)
        {
            SoundType           = soundType;
            AudioSourceIndex    = audioSourceIndex;
        }
    }
}
