using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    public GameObject player;

    protected EnemyStateContext _enemyStateContext;

    public int enemyHealth;
    public int enemyLevel;
    public int enemyDamage;

    protected EnemyStartState _startState;
    protected EnemyStopState _stopState;
    protected EnemyAttackState _attackState;

    protected bool isAttacking;

    protected void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy.speed = 0;

        _enemyStateContext = new EnemyStateContext(this);

        _startState = gameObject.AddComponent<EnemyStartState>();
        _stopState = gameObject.AddComponent<EnemyStopState>();
        _attackState = gameObject.AddComponent<EnemyAttackState>();
    }

    protected void Start()
    {
        enemy.updateRotation = true;
        _enemyStateContext.Transition(_stopState);
        
    }

    protected void Update()
    {
        if(enemy.remainingDistance <= enemy.stoppingDistance)
        {
            _enemyStateContext.Transition(_attackState);
        }           
    }

    public virtual void AttackPattern()
    {
        Debug.Log("Enemy Attacking");

    }

    /*private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Enemy Start State"))
        {
            _enemyStateContext.Transition(_startState);           
        }

        if (GUI.Button(new Rect(10, 110, 150, 100), "Enemy Stop State"))
        {
            _enemyStateContext.Transition(_stopState);
        }

        if (GUI.Button(new Rect(10, 210, 150, 100), "Enemy Attack State"))
        {
            _enemyStateContext.Transition(_attackState);
        }
    }*/

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DetectRadius"))
            _enemyStateContext.Transition(_startState);
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DetectRadius"))
            _enemyStateContext.Transition(_startState);
    }
}
