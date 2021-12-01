using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// 共通当たり判定インターフェース
    /// </summary>
    public interface ITiltRaceCollision
    {
        /// <summary>
        /// 座標
        /// </summary>
        Vector3 Position { get; }

        /// <summary>
        /// 横幅
        /// </summary>
        float Width { get; }

        /// <summary>
        /// 縦幅
        /// </summary>
        float Height { get; }
    }

    /// <summary>
    /// プレイヤーの車の当たり判定インターフェース
    /// </summary>
    public interface ITiltRacePlayerCarCollision : ITiltRaceCollision
    {
        /// <summary>
        /// 無敵状態か
        /// </summary>
        bool IsInvincible { get; }
    }

    /// <summary>
    /// 敵の車の当たり判定インターフェース
    /// </summary>
    public interface ITiltRaceEnemyCarCollision : ITiltRaceCollision
    {
        /// <summary>
        /// ダメージ量
        /// </summary>
        int Damage { get; }
    }

    /// <summary>
    /// アイテムの当たり判定インターフェース
    /// </summary>
    public interface ITiltRaceItemCollision : ITiltRaceCollision
    {
        /// <summary>
        /// ID
        /// </summary>
        int Id { get; }

        /// <summary>
        /// アイテム種別
        /// </summary>
        ItemType ItemType { get; }
    }
}
