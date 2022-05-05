using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobber : BaseEnemy
{
    public GameObject rockGO;
    private bool _delay = true;

    void Start()
    {
        damageOne = 3;
        damageTwo = 3;
        damageThree = 3;

        LevelCheck();

        enemy.stoppingDistance = 10f;

        isProjectile = true;

        rockGO.GetComponent<EnemyProjectile>().deathTimer = 20f;
        rockGO.GetComponent<EnemyProjectile>().speed = 4f;
        rockGO.GetComponent<EnemyProjectile>().enemy = this.GetComponent<Lobber>();
        rockGO.GetComponent<EnemyProjectile>().canPassWalls = true; ;
    }

    public override void AttackPattern()
    {
        if (enemy.remainingDistance > enemy.stoppingDistance && enemy.speed == 0)
        {
            _enemyStateContext.Transition(_startState);
        }

        if (_delay)
        {
            StartCoroutine(RockAttack());
        }
    }

    private IEnumerator RockAttack()
    {
        _delay = false;
        Instantiate(rockGO, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(2);
        _delay = true;
    }
}
