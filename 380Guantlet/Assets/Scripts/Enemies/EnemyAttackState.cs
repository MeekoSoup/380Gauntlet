using UnityEngine;

public class EnemyAttackState : MonoBehaviour, IEnemyState
{
    private BaseEnemy _baseEnemy;
    private Vector3 _newLook;

    public void Handle(BaseEnemy baseEnemy)
    {
        if (!_baseEnemy)
            _baseEnemy = baseEnemy;

        baseEnemy.isAttacking = true;
        baseEnemy.enemy.updateRotation = true;
        _newLook = baseEnemy.player.transform.position;
        _newLook.y += 1;
        transform.LookAt(_newLook);
        baseEnemy.AttackPattern();
    }
}
