// create statemachine for enemy

using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

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
        enemyIdleState = GetComponentInChildren<EnemyIdleState>();
        patrolState = GetComponentInChildren<EnemyPatrolState>();
        enemyAttackState = GetComponentInChildren<EnemyAttackState>();
        Debug.Log("EnemyStateMachine Awake");
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
        Debug.Log("EnemyStateMachine Update");

        if (currentState != null)
        {
            currentState.Execute();
        }
    }
}
