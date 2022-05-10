using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Death : MonoBehaviour
{
    public NavMeshAgent enemy;
    public GameObject player;
    private ShortController _player;

    private EnemyStateContext _enemyStateContext;
    private EnemyStartState _startState;
    private EnemyStopState _stopState;
    private EnemyAttackState _attackState;

    public bool isDrain;
    private int _drainedHealth;

    public int deathPoints;
    public int deathPointsLevel;

    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        _player = player.GetComponent<ShortController>();

        enemy.speed = 0;

        deathPointsLevel = 0;
        DeathPointsCheck();
    }

    private void Update()
    {
        enemy.destination = player.transform.position;

        if (_drainedHealth >= 200)
            Destroy(this.gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DetectRadius")
            enemy.speed = 5;

        if (other.gameObject.tag == "Player")
        {
            isDrain = true;
            StartCoroutine(DeathAttack());
        }

        if (other.gameObject.tag == "Weapon")
            DeathPointsCheck();

        if (other.gameObject.tag == "Potion")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isDrain = false;
    }

    IEnumerator DeathAttack()
    {        
        _drainedHealth += 5;
        _player.health -= 5;
        yield return new WaitForSeconds(0.1f);
        if (isDrain)
            StartCoroutine(DeathAttack());
    }

    private void DeathPointsCheck()
    {
        int number = deathPointsLevel;

        switch (number)
        {
            case 0:
                deathPoints = 1000;
                deathPointsLevel = 1;
                break;
            case 1:
                deathPoints = 2000;
                deathPointsLevel = 2;
                break;
            case 2:
                deathPoints = 1000;
                deathPointsLevel = 3;
                break;
            case 3:
                deathPoints = 4000;
                deathPointsLevel = 4;
                break;
            case 4:
                deathPoints = 2000;
                deathPointsLevel = 5;
                break;
            case 5:
                deathPoints = 6000;
                deathPointsLevel = 6;
                break;
            case 6:
                deathPoints = 8000;
                deathPointsLevel = 0;
                break;
            default:
                break;
        }
    }

    /*private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Death Cycle"))
        {
            DeathPointsCheck();
            Debug.Log(deathPoints);
            Debug.Log(deathPointsLevel);
        }
    }*/
}
