using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;

    public string nextLevel;

    private float spawnOffset = 0;
    private List<GameObject> _players = new List<GameObject>();

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "ChangeLevel"))
        {
            LevelChange();
        }
    }

    private void LevelChange()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            _players.Add(player);
        }

        if(nextLevel == "Level2")
        {
            level1.SetActive(false);
            level2.SetActive(true);
        }

        foreach (GameObject player in _players)
        {
            player.transform.position = new Vector3(spawnOffset, 0, 0);
            spawnOffset++;
        }
    }
}
