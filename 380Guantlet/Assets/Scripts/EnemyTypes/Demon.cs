using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : BaseEnemy
{
    public GameObject biteGO;
    public GameObject fireballGO;
    private bool _delay = true;

    private void Start()
    {
        damageOne = 5;
        damageTwo = 8;
        damageThree = 10;

        LevelCheck();

        enemy.stoppingDistance = 7.5f;

        biteGO.GetComponent<EnemyProjectile>().deathTimer = 0.3f;
        biteGO.GetComponent<EnemyProjectile>().speed = 3f;
        biteGO.GetComponent<EnemyProjectile>().enemy = this.GetComponent<Demon>();

        fireballGO.GetComponent<Fireball>().deathTimer = 10f;
        fireballGO.GetComponent<Fireball>().speed = 5f;
        fireballGO.GetComponent<Fireball>().enemy = this.GetComponent<Demon>();
    }

    public override void AttackPattern()
    {
        if (enemy.remainingDistance > enemy.stoppingDistance)
        {
            _enemyStateContext.Transition(_startState);
        }

        if (_delay)
        {
            if(enemy.remainingDistance <= 2f)
            {
                StartCoroutine(BiteAttack());
            }
            else
            {
                StartCoroutine(FireballAttack());
            }
        }
    }

    private IEnumerator BiteAttack()
    {
        _delay = false;
        Instantiate(biteGO, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(2);
        _delay = true;
    }

    private IEnumerator FireballAttack()
    {
        _delay = false;
        Instantiate(fireballGO, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(2);
        _delay = true;
    }
}
