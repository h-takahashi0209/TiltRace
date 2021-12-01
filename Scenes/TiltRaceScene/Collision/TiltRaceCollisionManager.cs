using System;
using System.Collections.Generic;
using UnityEngine;


namespace TakahashiH.Scenes.TiltRace
{
    /// <summary>
    /// TiltRace - �����蔻��Ǘ�
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TiltRaceCollisionManager : IDisposable
    {
        //====================================
        //! �v���p�e�B
        //====================================

        /// <summary>
        /// �G�̎ԂƂ̃q�b�g��
        /// </summary>
        public Action<int> OnHitEnemyCar { private get; set; }

        /// <summary>
        /// �A�C�e���Ƃ̃q�b�g��
        /// </summary>
        public Action<int, ItemType> OnHitItem { private get; set; }


        //====================================
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �j��
        /// </summary>
        public void Dispose()
        {
            OnHitEnemyCar = null;
            OnHitItem     = null;
        }

        /// <summary>
        /// �Փ˔���
        /// </summary>
        /// <param name="playerCarCollision">       �v���C���[�̓����蔻��                    </param>
        /// <param name="enemyCarCollisionList">    �A�N�e�B�u�ȓG�̎Ԃ̓����蔻�胊�X�g      </param>
        /// <param name="itemCollisionList">        �A�N�e�B�u�ȃA�C�e���̓����蔻�胊�X�g    </param>
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
        //! �֐��ipublic�j
        //====================================

        /// <summary>
        /// �v���C���[�ƓG�̏Փ˔���
        /// </summary>
        /// <param name="playerCarCollision">       �v���C���[�̓����蔻��                  </param>
        /// <param name="enemyCarCollisionList">    �A�N�e�B�u�ȓG�̎Ԃ̓����蔻�胊�X�g    </param>
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

                    // �����̎Ԃɓ����ɏՓ˂����Ƃ��Ă��A1��Ƃ̏Փ˂Ƃ݂Ȃ�
                    // ���̂��߈�x�ł��Փ˂������_�Ŕ���͏I��
                    break;
                }
            }
        }

        /// <summary>
        /// �v���C���[�ƃA�C�e���̏Փ˔���
        /// </summary>
        /// <param name="playerCarCollision">    �v���C���[�̓����蔻��                    </param>
        /// <param name="itemCollisionList">     �A�N�e�B�u�ȃA�C�e���̓����蔻�胊�X�g    </param>
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
