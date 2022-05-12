using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Thief : MonoBehaviour
{
    public Vector3 exit;
    public NavMeshAgent enemy;
    public GameObject player;
    private ShortController _player;
    public GameObject treasure;
    private bool _stole = false;

    private void Awake()
    {
        exit = GameObject.FindGameObjectWithTag("Exit").transform.position;
        enemy = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        _player = player.GetComponent<ShortController>();

        enemy.speed = 0;
        enemy.stoppingDistance = 0;
    }

    private void Update()
    {
        if(!_stole)
            enemy.destination = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DetectRadius")
            enemy.speed = 15;

        if (other.gameObject.tag == "Player" && !_stole)
            Steal();

        if(other.gameObject.tag == "Weapon")
        {
            Instantiate(treasure, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Exit")
            Destroy(this.gameObject);
    }

    private void Steal()
    {
        //Debug.Log("Stole Item");
        _stole = true;
        treasure = _player.inventoryItem;
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
