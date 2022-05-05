using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : BaseEnemy
{
    public GameObject clubGO;
    private bool _delay = true;

    public void Start()
    {
        damageOne = 5;
        damageTwo = 8;
        damageThree = 10;

        LevelCheck();

        enemy.stoppingDistance = 3.4f;

        //player = GameObject.FindGameObjectWithTag("Player");

        clubGO.GetComponent<EnemyProjectile>().deathTimer = 0.3f;
        clubGO.GetComponent<EnemyProjectile>().speed = 3f;
        clubGO.GetComponent<EnemyProjectile>().enemy = this.GetComponent<Grunt>();
    }

    public override void AttackPattern()
    {
        if(enemy.remainingDistance > enemy.stoppingDistance && enemy.speed == 0)
        {
            _enemyStateContext.Transition(_startState);
        }

        if (_delay)
        {
            //_playerPos = player.transform.position;
            StartCoroutine(ClubAttack());
        }
    }

    private IEnumerator ClubAttack()
    {
        _delay = false;
        Instantiate(clubGO, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(2);
        _delay = true;
    }

}
