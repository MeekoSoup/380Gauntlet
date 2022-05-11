using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float spawnDelay;
    private BaseEnemy _baseEnemy;

    [Range(1, 3)]
    public int spawnerLevel;
    public bool isBones;

    private void Awake()
    {
        _baseEnemy = enemyToSpawn.GetComponent<BaseEnemy>();
        _baseEnemy.enemyLevel = spawnerLevel;
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
        if (other.gameObject.tag == "Weapon" && !isBones)
        {
            spawnerLevel--;
            _baseEnemy.enemyLevel = spawnerLevel;
            if (spawnerLevel <= 0)
                Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "Weapon" && isBones)
            Destroy(this.gameObject);

        if (other.gameObject.tag == "Fireball")
            Destroy(this.gameObject);
    }

    /*private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Spanwer Hit"))
        {
            spawnerLevel--;
            _baseEnemy.enemyLevel = spawnerLevel;
            if (spawnerLevel <= 0)
                Destroy(this.gameObject);
            Debug.Log(spawnerLevel);
        }
    }*/

}
