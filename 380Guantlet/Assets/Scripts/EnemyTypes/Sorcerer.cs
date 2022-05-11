using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorcerer : BaseEnemy
{
    public GameObject spellGO;
    private bool _delay = true;

    private void Start()
    {
        damageOne = 5;
        damageTwo = 8;
        damageThree = 10;

        LevelCheck();

        enemy.stoppingDistance = 2f;

        spellGO.GetComponent<EnemyProjectile>().deathTimer = 0.3f;
        spellGO.GetComponent<EnemyProjectile>().speed = 3f;
        spellGO.GetComponent<EnemyProjectile>().enemy = this.GetComponent<Sorcerer>();

        StartCoroutine(Blink());
    }

    public override void AttackPattern()
    {
        if (enemy.remainingDistance > enemy.stoppingDistance && enemy.speed == 0)
        {
            _enemyStateContext.Transition(_startState);
        }

        if (_delay && canBeHit)
        {
            StartCoroutine(SpellAttack());
        }   
    }

    private IEnumerator Blink()
    {
        yield return new WaitForSeconds(2f);
        enemyColor.a = 0;
        this.GetComponent<MeshRenderer>().material.color = enemyColor;
        canBeHit = false;
        yield return new WaitForSeconds(1f);
        LevelCheck();
        canBeHit = true;
        StartCoroutine(Blink());
    }

    private IEnumerator SpellAttack()
    {
        _delay = false;
        Instantiate(spellGO, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(2);
        _delay = true;
    }
}
