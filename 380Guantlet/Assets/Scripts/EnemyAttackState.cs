using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : MonoBehaviour, IEnemyState
{
    private BaseEnemy _baseEnemy;

    public void Handle(BaseEnemy baseEnemy)
    {
        if (!_baseEnemy)
            _baseEnemy = baseEnemy;

        baseEnemy.isAttacking = true;
        baseEnemy.enemy.updateRotation = true;
        transform.LookAt(baseEnemy.player.transform);
        baseEnemy.AttackPattern();
    }
}
