using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
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
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        Instantiate(enemyToSpawn, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(2);
        StartCoroutine(SpawnEnemies());
    }
}
