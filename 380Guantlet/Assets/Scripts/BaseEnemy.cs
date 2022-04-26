using Data;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public EventNetwork eventNetwork;
    public NavMeshAgent enemy;
    public GameObject player;
    protected ShortController _player;

    protected EnemyStateContext _enemyStateContext;

    //public int enemyHealth;
    public int enemyLevel;
    public int enemyDamage;

    protected int damageOne;
    protected int damageTwo;
    protected int damageThree;

    protected EnemyStartState _startState;
    protected EnemyStopState _stopState;
    protected EnemyAttackState _attackState;

    public bool isAttacking;

    private Material enemyMat;
    private Color enemyColor;

    protected void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        _player = player.GetComponent<ShortController>();
        enemyMat = this.GetComponent<MeshRenderer>().material;
        enemyColor = this.GetComponent<MeshRenderer>().material.color;
        
        enemy.speed = 0;

        _enemyStateContext = new EnemyStateContext(this);

        _startState = gameObject.AddComponent<EnemyStartState>();
        _stopState = gameObject.AddComponent<EnemyStopState>();
        _attackState = gameObject.AddComponent<EnemyAttackState>();

        enemy.updateRotation = true;
        _enemyStateContext.Transition(_stopState);
    }

    protected void Update()
    {
        enemy.destination = player.transform.position;
        if (enemy.remainingDistance <= enemy.stoppingDistance)
        {
            _enemyStateContext.Transition(_attackState);
        }
    }

    public virtual void AttackPattern()
    {
        print("attacking");
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Take Damage"))
        {
            enemyLevel--;
            LevelCheck();
        }

        if (GUI.Button(new Rect(10, 110, 150, 100), "Increase Level"))
        {
            enemyLevel++;
            LevelCheck();
        }

        /*if (GUI.Button(new Rect(10, 210, 150, 100), "Change Color"))
        {
            
        }

        /*GUI.color = Color.black;
        GUI.Label(new Rect(150, 0, 500, 20), "Player Health: " + _player.health);
        GUI.Label(new Rect(150, 20, 500, 20), "Enemy Level: " + enemyLevel);*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DetectRadius"))
            _enemyStateContext.Transition(_startState);

        /*if (other.gameObject.CompareTag("Weapon"))
            enemyLevel--;*/

        if (other.gameObject.CompareTag("Player"))
        {
            DamagePlayer();
            TakeDamage();
        }
    }

    private void DamagePlayer()
    {
        _player.health -= enemyDamage;
        eventNetwork.OnPlayerDamaged?.Invoke();
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DetectRadius"))
            _enemyStateContext.Transition(_startState);
    }

    protected void LevelCheck()
    {
        int number = enemyLevel;

        if (enemyLevel <= 0)
        {
            Release();
            return;
        }
        

        switch (number)
        {
            case 1:
                enemyDamage = damageOne;
                enemyColor.a = 0.7f;
                break;
            case 2:
                enemyDamage = damageTwo;
                enemyColor.a = 0.85f;
                break;
            case 3:
                enemyDamage = damageThree;
                enemyColor.a = 1.0f;
                break;
            default:
                break;
        }

        this.GetComponent<MeshRenderer>().material.color = enemyColor;
    }

    protected void TakeDamage()
    {
        enemyLevel--;
        eventNetwork.OnEnemyDamaged?.Invoke();
        LevelCheck();
    }

    protected void Release()
    {
        eventNetwork.OnEnemyKilled?.Invoke();
        Destroy(gameObject);
    }
}
