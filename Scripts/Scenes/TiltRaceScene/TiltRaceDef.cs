
/// <summary>
/// TiltRace 共通定義
/// </summary>
namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// 車の色
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
    /// 敵の車の移動パターン種別
    /// </summary>
    public enum EnemyCarMovePatternType
    {
        None            = -1,
        Liner           ,       // 直退
        LinerAndStop    ,       // 直退と停止を繰り返す
        Horming         ,       // 追従
        Zigzag          ,       // ジグザグ移動
        ZigzagAndStop   ,       // ジグザグ移動と停止を繰り返す
        Sizeof          ,
    }

    /// <summary>
    /// アイテム種別
    /// </summary>
    public enum ItemType
    {
        None            = -1,
        RecoveryLife    ,       // ライフ回復
        SpeedUp         ,       // 速度上昇
        SpeedDown       ,       // 速度減少
        Sizeof          ,
    }
}
