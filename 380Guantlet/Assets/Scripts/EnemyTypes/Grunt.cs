using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : BaseEnemy
{
    public GameObject club;
    private bool _delay = true;

    public void Start()
    {
        damageOne = 5;
        damageTwo = 8;
        damageThree = 10;

        LevelCheck();

        enemy.stoppingDistance = 3.4f;

        club.SetActive(false);
    }

    public override void AttackPattern()
    {
        if (_delay)
        {
            StartCoroutine(ClubAttack());
        }
    }

    private IEnumerator ClubAttack()
    {
        _delay = false;
        club.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        club.SetActive(false);
        yield return new WaitForSeconds(2);
        _delay = true;
    }
}
