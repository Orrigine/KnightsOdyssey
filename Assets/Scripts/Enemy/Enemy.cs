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
    public void Start()
    {
        animator = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();
        // _stateMachine = GetComponentInChildren<EnemyStateMachine>();

        _stateMachine.ChangeState(_stateMachine.patrolState);

        // FIXME: SENDING NULL
        // _stateMachine.ChangeState(_stateMachine._patrolState);

    }

    // Update is called once per frame
    public void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

        if (_stateMachine.currentState is EnemyPatrolState)
        {
            bool facingRight = _nav.velocity.x >= 0;
            enemyGO.transform.localScale = new Vector3(facingRight ? -1.0F : 1.0F, 1.0F, 1.0F);


            if (_nav.remainingDistance <= _nav.stoppingDistance && !_nav.pathPending)
            {
                _stateMachine.ChangeState(_stateMachine.enemyIdleState);
            }

            // Switch to Attack
            if (_stateMachine.patrolState.Detected)
            {
                _stateMachine.ChangeState(_stateMachine.enemyAttackState);
            }
        }
        else if (_stateMachine.currentState is EnemyIdleState)
        {
            if (_nav.remainingDistance > _nav.stoppingDistance && !_nav.pathPending)
            {
                _stateMachine.ChangeState(_stateMachine.patrolState);
            }
        }
    }
}