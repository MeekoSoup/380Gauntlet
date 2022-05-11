using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    protected Rigidbody _projectile;
    public float deathTimer;
    public float speed;
    public BaseEnemy enemy;
    protected ShortController _player;
    public bool canPassWalls;

    private Transform playerTransform;

    public void Awake()
    {
        
        playerTransform = enemy.transform;
        _projectile = this.GetComponent<Rigidbody>();
        _projectile.AddForce((playerTransform.position - transform.position).normalized * speed, ForceMode.VelocityChange);
        StartCoroutine(ProjectileDeath(deathTimer));
    }

    public IEnumerator ProjectileDeath(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _player = other.GetComponent<ShortController>();
            _player.health -= enemy.enemyDamage;

            if (enemy.isProjectile)
                Destroy(this.gameObject);
        }
            
    }
}
