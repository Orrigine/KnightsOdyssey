using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStateMachine _stateMachine;
    [SerializeField] public GameObject enemyGO;
    [SerializeField] public Animator animator;
    NavMeshAgent _nav;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();
        _stateMachine = GetComponentInChildren<EnemyStateMachine>();

        _stateMachine.ChangeState(_stateMachine.patrolState);

        // FIXME: SENDING NULL
        // _stateMachine.ChangeState(_stateMachine._patrolState);


    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);


        if (_stateMachine.currentState is EnemyPatrolState)
        {
            animator.SetBool("isPatroling", true);

        }
        else
        {
            animator.SetBool("isPatroling", false);
        }
    }
}