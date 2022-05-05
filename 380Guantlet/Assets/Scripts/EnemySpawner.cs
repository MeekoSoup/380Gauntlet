using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float spawnDelay;
    private BaseEnemy _baseEnemy;

    [Range(1, 3)]
    public int enemyLevel;

    private void Awake()
    {
        _baseEnemy = enemyToSpawn.GetComponent<BaseEnemy>();
        _baseEnemy.enemyLevel = enemyLevel;
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemies(spawnDelay));
    }

    private IEnumerator SpawnEnemies(float seconds)
    {
        Instantiate(enemyToSpawn, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(seconds);
        StartCoroutine(SpawnEnemies(spawnDelay));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
            Destroy(this.gameObject);

        if (other.gameObject.tag == "Fireball")
            Destroy(this.gameObject);
    }
}
