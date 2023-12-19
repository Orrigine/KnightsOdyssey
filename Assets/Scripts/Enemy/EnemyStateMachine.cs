// create statemachine for enemy

using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct StateKV
{
    string name;
    EnemyState state;
}

public class EnemyStateMachine : MonoBehaviour
{
    public List<EnemyState> _states;
    public EnemyState currentState;
    public EnemyState remainState;
    public EnemyIdleState enemyIdleState;
    public EnemyPatrolState patrolState;
    public EnemyAttackState enemyAttackState;

    public void Awake()
    {
        _states = new();
        // Instantiate all the states
        _idleState = GetComponentInChildren<EnemyIdleState>();
        _patrolState = GetComponentInChildren<EnemyPatrolState>();
        _attackState = GetComponentInChildren<EnemyAttackState>();
    }

    public void Start()
    {

    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }

    void OnDestroy()
    {
        // FIXME: Doesn't reset to null the field
        currentState = null;
    }
}
