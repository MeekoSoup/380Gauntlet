using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody _projectile;
    public float deathTimer;
    public BaseEnemy enemy;
    private ShortController _player;

    private void Awake()
    {
        _projectile = this.GetComponent<Rigidbody>();
        enemy.ProjectileMove(_projectile);
        StartCoroutine(ProjectileDeath(deathTimer));
    }

    private IEnumerator ProjectileDeath(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _player = other.GetComponent<ShortController>();
            _player.health -= enemy.enemyDamage;
        }
            
    }
}
