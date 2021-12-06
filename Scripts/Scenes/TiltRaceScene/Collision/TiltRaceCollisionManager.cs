using System;
using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - 当たり判定管理
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRaceCollisionManager : IDisposable
    {
        //====================================
        //! プロパティ
        //====================================

        /// <summary>
        /// 敵の車とのヒット時
        /// </summary>
        public Action<int> OnHitEnemyCar { private get; set; }

        /// <summary>
        /// アイテムとのヒット時
        /// </summary>
        public Action<int, ItemType> OnHitItem { private get; set; }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            OnHitEnemyCar = null;
            OnHitItem     = null;
        }

        /// <summary>
        /// 衝突判定
        /// </summary>
        /// <param name="playerCarCollision">       プレイヤーの当たり判定                    </param>
        /// <param name="enemyCarCollisionList">    アクティブな敵の車の当たり判定リスト      </param>
        /// <param name="itemCollisionList">        アクティブなアイテムの当たり判定リスト    </param>
        public void CheckHit
        (
            ITiltRacePlayerCarCollision                 playerCarCollision      ,
            IReadOnlyList<ITiltRaceEnemyCarCollision>   enemyCarCollisionList   ,
            IReadOnlyList<ITiltRaceItemCollision>       itemCollisionList
        )
        {
            CheckHitEnemy (playerCarCollision, enemyCarCollisionList);
            CheckHitItem  (playerCarCollision, itemCollisionList);
        }


        //====================================
        //! 関数（public）
        //====================================

        /// <summary>
        /// プレイヤーと敵の衝突判定
        /// </summary>
        /// <param name="playerCarCollision">       プレイヤーの当たり判定                  </param>
        /// <param name="enemyCarCollisionList">    アクティブな敵の車の当たり判定リスト    </param>
        private void CheckHitEnemy(ITiltRacePlayerCarCollision playerCarCollision, IReadOnlyList<ITiltRaceEnemyCarCollision> enemyCarCollisionList)
        {
            if (playerCarCollision.IsInvincible) {
                return;
            }

            for (int i = 0; i < enemyCarCollisionList.Count; i++)
            {
                var enemyCarCollision = enemyCarCollisionList[i];

                bool isHit =
                (
                    playerCarCollision.Position.x >= enemyCarCollision.Position.x - enemyCarCollision.Width  / 2
                &&  playerCarCollision.Position.x <= enemyCarCollision.Position.x + enemyCarCollision.Width  / 2
                &&  playerCarCollision.Position.y >= enemyCarCollision.Position.y - enemyCarCollision.Height / 2
                &&  playerCarCollision.Position.y <= enemyCarCollision.Position.y + enemyCarCollision.Height / 2
                );

                if (isHit)
                {
                    OnHitEnemyCar(enemyCarCollision.Damage);

                    // 複数の車に同時に衝突したとしても、1台との衝突とみなす
                    // そのため一度でも衝突した時点で判定は終了
                    break;
                }
            }
        }

        /// <summary>
        /// プレイヤーとアイテムの衝突判定
        /// </summary>
        /// <param name="playerCarCollision">    プレイヤーの当たり判定                    </param>
        /// <param name="itemCollisionList">     アクティブなアイテムの当たり判定リスト    </param>
        private void CheckHitItem(ITiltRaceCollision playerCarCollision, IReadOnlyList<ITiltRaceItemCollision> itemCollisionList)
        {
            for (int i = 0; i < itemCollisionList.Count; i++)
            {
                var itemCollision = itemCollisionList[i];

                bool isHit =
                (
                    playerCarCollision.Position.x >= itemCollision.Position.x - itemCollision.Width  / 2
                &&  playerCarCollision.Position.x <= itemCollision.Position.x + itemCollision.Width  / 2
                &&  playerCarCollision.Position.y >= itemCollision.Position.y - itemCollision.Height / 2
                &&  playerCarCollision.Position.y <= itemCollision.Position.y + itemCollision.Height / 2
                );

                if (isHit)
                {
                    OnHitItem(itemCollision.Id, itemCollision.ItemType);
                }
            }
        }
    }
}
