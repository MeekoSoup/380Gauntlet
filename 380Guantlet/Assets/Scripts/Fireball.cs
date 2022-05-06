using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : EnemyProjectile
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player = other.GetComponent<ShortController>();
            _player.health -= 10;
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy.enemyLevel--;
        }

        if (other.gameObject.CompareTag("Item"))
            Destroy(other.gameObject);

    }

}
