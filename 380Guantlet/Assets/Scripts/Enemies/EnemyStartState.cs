using UnityEngine;

public class EnemyStartState : MonoBehaviour, IEnemyState
{
    private BaseEnemy _baseEnemy;

    public void Handle(BaseEnemy baseEnemy)
    {
        if (!_baseEnemy)
            _baseEnemy = baseEnemy;

        //Debug.Log("Enemy Start State");
        baseEnemy.enemy.destination = baseEnemy.player.transform.position;
        baseEnemy.enemy.speed = 5;
        Debug.Log("StartState");
    }
}
