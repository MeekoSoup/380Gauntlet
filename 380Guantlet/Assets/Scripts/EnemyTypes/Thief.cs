using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character;
using UnityEngine;
using UnityEngine.AI;

public class Thief : MonoBehaviour
{
    public Vector3 exit;
    public NavMeshAgent enemy;
    public GameObject treasurePrefab;
    public GameObject potionPrefab;
    public GameObject dropItem;
    
    private PlayerOverseer _player;
    private bool _stole = false;
    private IEnumerator _enumerator;

    private void Start()
    {
        _enumerator = KeepChecking(5f);
    }

    private void Awake()
    {
        exit = GameObject.FindGameObjectWithTag("Exit").transform.position;
        enemy = this.GetComponent<NavMeshAgent>();
        AcquireValidTarget();

        enemy.speed = 0;
        enemy.stoppingDistance = 0;
    }

    private void AcquireValidTarget()
    {
        var players = GameObject.FindObjectsOfType<PlayerOverseer>().ToList();
        foreach (var overseer in players)
        {
            if (overseer.playerData.potions < 1) continue;
            _player = overseer;
            break;
        }

        if (!_player)
        {
            StartCoroutine(_enumerator);
        }
    }

    private IEnumerator KeepChecking(float checkInterval = 5f)
    {
        if (!_player)
        {
            yield return new WaitForSeconds(checkInterval);
            AcquireValidTarget();
        }
    }

    private void Update()
    {
        if(!_stole && _player)
            enemy.destination = _player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DetectRadius"))
            enemy.speed = 15;

        if (other.CompareTag("Player") && !_stole)
            Steal();

        if(other.CompareTag("Weapon"))
        {
            Instantiate(dropItem, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Exit"))
            Destroy(this.gameObject);
    }

    private void Steal()
    {
        //Debug.Log("Stole Item");
        _stole = true;
        if (_player)
        {
            if (_player.playerData.potions > 0)
            {
                dropItem = potionPrefab;
            }
            else
            {
                dropItem = treasurePrefab;
            }
        }

        enemy.destination = exit;
    }

    /*private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Steal"))
        {
            Steal();
        }

        if (GUI.Button(new Rect(10, 110, 150, 100), "Kill Thief"))
        {
            Instantiate(treasure, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }*/
}
