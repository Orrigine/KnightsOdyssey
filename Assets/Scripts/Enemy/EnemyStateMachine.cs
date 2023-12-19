// create statemachine for enemy

using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeReference] public List<EnemyState> _states;
    public EnemyState currentState;
    public EnemyState remainState;

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
