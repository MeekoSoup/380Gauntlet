using System;
using Character;
using Control;
using Data;
using Enemies;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class BaseEnemy : MonoBehaviour
{
    public EventNetwork eventNetwork;
    public NavMeshAgent enemy;
    public GameObject player;
    public bool isProjectile;
    public int enemyLevel;
    public int enemyDamage;
    public bool isAttacking = false;
    public int score = 10;
    public bool canBeHit = true;
    
    protected int damageOne;
    protected int damageTwo;
    protected int damageThree;
    
    protected EnemyStateContext _enemyStateContext;
    
    protected EnemyStartState _startState;
    protected EnemyStopState _stopState;
    protected EnemyAttackState _attackState;
    
    protected Color enemyColor;
    protected Material enemyMat;

    protected void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        // _player = player.GetComponent<ShortController>();
        // enemyMat = this.GetComponent<MeshRenderer>().material;
        // enemyColor = this.GetComponent<MeshRenderer>().material.color;
        
        enemy.speed = 0;

        _enemyStateContext = new EnemyStateContext(this);

        _startState = gameObject.AddComponent<EnemyStartState>();
        _stopState = gameObject.AddComponent<EnemyStopState>();
        _attackState = gameObject.AddComponent<EnemyAttackState>();

        enemy.updateRotation = true;
        _enemyStateContext.Transition(_stopState);
        this.GetComponent<Rigidbody>().maxAngularVelocity = 0;
    }

    private void OnEnable()
    {
        eventNetwork.OnPlayerJoined += UpdatePlayer;
        eventNetwork.OnPlayerUseNuke += NukeEnemy;
    }

    private void OnDisable()
    {
        eventNetwork.OnPlayerJoined -= UpdatePlayer;
        eventNetwork.OnPlayerUseNuke -= NukeEnemy;
    }

    private void NukeEnemy(PlayerInput playerInput = null)
    {
        Debug.Log($"{playerInput.gameObject.name} just used a potion!");
        
        if (!playerInput)
        {
            Release();
            return;
        }

        var dist = Vector3.Distance(transform.position, playerInput.transform.position);

        var overseer = playerInput.GetComponent<PlayerOverseer>();
        if (!overseer)
        {
            Release();
            return;
        }

        overseer.playerData.score += score;
        
        var role = overseer.playerData.role;
        switch (role)
        {
            case PlayerRole.None:
                Debug.Log("What?");
                break;
            case PlayerRole.Elf:
                TakeDamage();
                if (dist < 20f)
                {
                    TakeDamage();
                }
                break;
            case PlayerRole.Valkyrie:
                if (dist < 20f)
                {
                    TakeDamage();
                    TakeDamage();
                }
                break;
            case PlayerRole.Warrior:
                if (dist < 15f)
                {
                    TakeDamage();
                }
                break;
            case PlayerRole.Wizard:
                TakeDamage();
                if (dist < 20f)
                {
                    Release();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdatePlayer(PlayerInput playerInput = null)
    {
        if (playerInput)
            player = playerInput.gameObject;
        else
            player = FindObjectOfType<PlayerMovement>().gameObject;

        UpdateDestination();
        // player = GameObject.FindGameObjectWithTag("Player");
    }

    protected void Update()
    {
        // if we haven't found a player yet, do nothing.
        if (!player)
            return;

        UpdateDestination();
    }

    private void UpdateDestination()
    {
        enemy.destination = player.transform.position;
        if (enemy.remainingDistance <= enemy.stoppingDistance && isAttacking)
        {
            _enemyStateContext.Transition(_attackState);
        }
    }

    public virtual void AttackPattern()
    {
        print("attacking");
    }

//     private void OnGUI()
//     {
//         if (GUI.Button(new Rect(10, 10, 150, 100), "Take Damage"))
//         {
//             enemyLevel--;
//             LevelCheck();
//         }
//
//         if (GUI.Button(new Rect(10, 110, 150, 100), "Increase Level"))
//         {
//             enemyLevel++;
//             LevelCheck();
//         }
//
//         /*if (GUI.Button(new Rect(10, 210, 150, 100), "Change Color"))
//         {
//             
//         }
//
//         /*GUI.color = Color.black;
//         GUI.Label(new Rect(150, 0, 500, 20), "Player Health: " + _player.health);
//         GUI.Label(new Rect(150, 20, 500, 20), "Enemy Level: " + enemyLevel);*/
//     }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DetectRadius"))
        {
            if (!isAttacking)
                isAttacking = !isAttacking;

            _enemyStateContext.Transition(_startState);
        }
            

        /*if (other.gameObject.CompareTag("Weapon"))
            enemyLevel--;*/

        if (other.gameObject.CompareTag("Player"))
        {
            DamagePlayer(other.gameObject);
            TakeDamage();
        }
    }

    private void DamagePlayer(GameObject playerObject)
    {
        // _player.health -= enemyDamage;
        var po = playerObject.GetComponent<PlayerOverseer>();
        if (po)
        {
            po.playerData.health -= enemyDamage * enemyLevel;
        }
        eventNetwork.OnPlayerDamaged?.Invoke();
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DetectRadius"))
            _enemyStateContext.Transition(_startState);
    }

    protected void LevelCheck()
    {
        if (enemyLevel <= 0)
        {
            Release();
            return;
        }

        int number = enemyLevel;
        var appearance = GetComponent<EnemyAppearanceController>();
        Color color = Color.white;
        
        switch (number)
        {
            case 1:
                enemyDamage = damageOne;
                color.a = .7f;
                break;
            case 2:
                enemyDamage = damageTwo;
                color.a = .85f;
                break;
            case 3:
                enemyDamage = damageThree;
                color.a = 1f;
                break;
            default:
                break;
        }
        
        if (appearance)
            appearance.SetAlpha(color.a);
        
        // this.GetComponent<MeshRenderer>().material.color = enemyColor;
    }

    public virtual void TakeDamage()
    {
        enemyLevel--;
        eventNetwork.OnEnemyDamaged?.Invoke();
        LevelCheck();
    }

    protected virtual void Release()
    {
        eventNetwork.OnEnemyKilled?.Invoke();
        Destroy(gameObject);
    }
}
