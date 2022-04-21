using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : BaseEnemy
{
    public void Start()
    {
        damageOne = 10;
        damageTwo = 20;
        damageThree = 30;

        LevelCheck();

        enemy.stoppingDistance = 0;
    }

    public override void AttackPattern()
    {
        print("ghost attack");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player.health -= enemyDamage;
            Destroy(this.gameObject);
        }
    }
}
