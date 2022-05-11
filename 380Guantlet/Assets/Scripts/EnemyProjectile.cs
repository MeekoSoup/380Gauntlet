using System.Collections;
using System.Collections.Generic;
using Character;
using Data;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public EventNetwork eventNetwork;
    protected Rigidbody _projectile;
    public float deathTimer;
    public float speed;
    public BaseEnemy enemy;
    protected ShortController _player;
    public bool canPassWalls;

    private Vector3 playerTransform;

    public void Awake()
    {
        
        playerTransform = enemy.enemy.destination;
        playerTransform.y += 1;
        _projectile = this.GetComponent<Rigidbody>();
        _projectile.AddForce((playerTransform - transform.position).normalized * speed, ForceMode.VelocityChange);
        StartCoroutine(ProjectileDeath(deathTimer));
    }

    public IEnumerator ProjectileDeath(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        PlayerOverseer po = other.GetComponent<PlayerOverseer>();
        if (po)
        {
            po.playerData.health -= enemy.enemyDamage * enemy.enemyLevel;
            eventNetwork.OnPlayerDamaged?.Invoke();
            // _player = other.GetComponent<ShortController>();
            // _player.health -= enemy.enemyDamage;

            if (enemy.isProjectile)
                Destroy(this.gameObject);
        }
    }
}
