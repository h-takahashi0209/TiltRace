using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// ���ʓ����蔻��C���^�[�t�F�[�X
    /// </summary>
    public interface ITiltRaceCollision
    {
        /// <summary>
        /// ���W
        /// </summary>
        Vector3 Position { get; }

        /// <summary>
        /// ����
        /// </summary>
        float Width { get; }

        /// <summary>
        /// �c��
        /// </summary>
        float Height { get; }
    }

    /// <summary>
    /// �v���C���[�̎Ԃ̓����蔻��C���^�[�t�F�[�X
    /// </summary>
    public interface ITiltRacePlayerCarCollision : ITiltRaceCollision
    {
        /// <summary>
        /// ���G��Ԃ�
        /// </summary>
        bool IsInvincible { get; }
    }

    /// <summary>
    /// �G�̎Ԃ̓����蔻��C���^�[�t�F�[�X
    /// </summary>
    public interface ITiltRaceEnemyCarCollision : ITiltRaceCollision
    {
        /// <summary>
        /// �_���[�W��
        /// </summary>
        int Damage { get; }
    }

    /// <summary>
    /// �A�C�e���̓����蔻��C���^�[�t�F�[�X
    /// </summary>
    public interface ITiltRaceItemCollision : ITiltRaceCollision
    {
        /// <summary>
        /// ID
        /// </summary>
        int Id { get; }

        /// <summary>
        /// �A�C�e�����
        /// </summary>
        ItemType ItemType { get; }
    }
}
