using UnityEngine;

public class EnemyStopState : MonoBehaviour, IEnemyState
{
    private BaseEnemy _baseEnemy;

    public void Handle(BaseEnemy baseEnemy)
    {
        if (!_baseEnemy)
            _baseEnemy = baseEnemy;

        baseEnemy.enemy.speed = 0;
        //Debug.Log("Stop State");
    }
}
