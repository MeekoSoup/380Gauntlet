using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEditor.AI;

public class LevelTransition : MonoBehaviour
{
    public EventNetwork eventNetwork;
    public GameObject currentLevel;
    public GameObject nextLevel;

    private float spawnOffset = 0;
    private List<GameObject> _players = new List<GameObject>();


    private void OnEnable()
    {
        NavMeshBuilder.ClearAllNavMeshes();
        NavMeshBuilder.BuildNavMesh();
    }

    // private void OnGUI()
    // {
    //     if (GUI.Button(new Rect(10, 10, 150, 100), "ChangeLevel"))
    //     {
    //         LevelChange();
    //     }
    // }

    private void LevelChange()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            _players.Add(player);
        }

        currentLevel.SetActive(false);
        nextLevel.SetActive(true);
        
        /*NavMeshBuilder.ClearAllNavMeshes();
        NavMeshBuilder.BuildNavMesh();*/

        foreach (GameObject player in _players)
        {
            player.transform.position = new Vector3(spawnOffset, 0, 0);
            spawnOffset+=2;
        }
        eventNetwork.OnLevelLoad?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            LevelChange();
    }
}
