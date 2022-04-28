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
        
        club.GetComponent<EnemyProjectile>().deathTimer = 0.3f;
    }

    public override void AttackPattern()
    {
        if(enemy.remainingDistance > enemy.stoppingDistance)
        {
            _enemyStateContext.Transition(_startState);
        }

        if (_delay)
        {
            _playerPos = player.transform.position;
            StartCoroutine(ClubAttack());
        }
    }

    private IEnumerator ClubAttack()
    {
        _delay = false;
        Instantiate(club, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        _delay = true;
    }

    public override void ProjectileMove(Rigidbody club)
    {
        //club.velocity = Vector3.forward.normalized * 3f;
        club.AddForce((_playerPos - club.transform.position).normalized * 3f, ForceMode.VelocityChange);
    }
}
