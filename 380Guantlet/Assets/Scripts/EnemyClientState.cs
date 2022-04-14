using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClientState : MonoBehaviour
{
    private BaseEnemy _baseEnemy;

    private void Start()
    {
        _baseEnemy = (BaseEnemy)FindObjectOfType(typeof(BaseEnemy));
    }
}
