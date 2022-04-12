using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public NavMeshAgent enemy;

    protected EnemyStateContext _enemyStateContext;

    public int enemyHealth;
    public int enemyLevel;
    public int enemyDamage;

}
