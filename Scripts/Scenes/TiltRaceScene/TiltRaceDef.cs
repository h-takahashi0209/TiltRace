
/// <summary>
/// TiltRace ���ʒ�`
/// </summary>
namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// �Ԃ̐F
    /// </summary>
    public enum CarColor
    {
        None    = -1,
        Aqua    ,
        Blue    ,
        Green   ,
        Orange  ,
        Purple  ,
        Red     ,
        Yellow  ,
        Sizeof  ,
    }

    /// <summary>
    /// �G�̎Ԃ̈ړ��p�^�[�����
    /// </summary>
    public enum EnemyCarMovePatternType
    {
        None            = -1,
        Liner           ,       // ����
        LinerAndStop    ,       // ���ނƒ�~���J��Ԃ�
        Horming         ,       // �Ǐ]
        Zigzag          ,       // �W�O�U�O�ړ�
        ZigzagAndStop   ,       // �W�O�U�O�ړ��ƒ�~���J��Ԃ�
        Sizeof          ,
    }

    /// <summary>
    /// �A�C�e�����
    /// </summary>
    public enum ItemType
    {
        None            = -1,
        RecoveryLife    ,       // ���C�t��
        SpeedUp         ,       // ���x�㏸
        SpeedDown       ,       // ���x����
        Sizeof          ,
    }
}
