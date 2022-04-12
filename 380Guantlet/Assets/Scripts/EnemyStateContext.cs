using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateContext
{
    public IEnemyState CurrentState
    {
        get; set;
    }

    private readonly BaseEnemy _baseEnemy;

    public EnemyStateContext(BaseEnemy baseEnemy)
    {
        _baseEnemy = baseEnemy;
    }

    public void Transition()
    {
        CurrentState.Handle(_baseEnemy);
    }

    public void Transition(IEnemyState state)
    {
        CurrentState = state;
        CurrentState.Handle(_baseEnemy);
    }
}
