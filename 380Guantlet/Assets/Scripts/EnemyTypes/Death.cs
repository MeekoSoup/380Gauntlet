using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Death : MonoBehaviour
{
    public EventNetwork eventNetwork;
    public NavMeshAgent enemy;
    public GameObject player;
    private PlayerOverseer _player;

    public bool isDrain;
    private int _drainedHealth;

    public int deathPoints;
    public int deathPointsLevel;

    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        AcquireTarget();

        enemy.speed = 0;

        deathPointsLevel = 0;
        DeathPointsCheck();
    }

    private void OnEnable()
    {
        eventNetwork.OnPlayerJoined += AcquireTarget;
        eventNetwork.OnPlayerKilled += AcquireTarget;
        eventNetwork.OnPlayerDisconnect += AcquireTarget;
        eventNetwork.OnLevelLoad += AcquireTarget;
        eventNetwork.OnPlayerUseNuke += NukeDeath;
    }

    private void NukeDeath(PlayerInput playerInput = null)
    {
        if (!playerInput)
        {
            Release();
            return;
        }

        var overseer = playerInput.GetComponent<PlayerOverseer>();
        if (!overseer)
        {
            Release();
            return;
        }

        overseer.playerData.score += deathPoints;
        Release();
    }

    private void OnDisable()
    {
        eventNetwork.OnPlayerJoined -= AcquireTarget;
        eventNetwork.OnPlayerKilled -= AcquireTarget;
        eventNetwork.OnPlayerDisconnect -= AcquireTarget;
        eventNetwork.OnLevelLoad -= AcquireTarget;
    }

    private void AcquireTarget(PlayerInput playerInput = null)
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        _player = player.GetComponent<PlayerOverseer>();
    }

    private void Update()
    {
        enemy.destination = player.transform.position;

        if (_drainedHealth >= 200)
            Release();

    }

    private void Release()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DetectRadius"))
            enemy.speed = 5;

        if (other.CompareTag("Player"))
        {
            isDrain = true;
            StartCoroutine(DeathAttack());
        }

        if (other.CompareTag("Weapon"))
            DeathPointsCheck();

        if (other.CompareTag("Potion"))
        {
            Release();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isDrain = false;
    }

    IEnumerator DeathAttack()
    {        
        _drainedHealth += 5;
        
        if (_player)
            _player.playerData.health -= 5;
        
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
