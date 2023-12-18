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
        enemyGO = GetComponent<GameObject>();
        animator = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();

        _stateMachine.ChangeState(new EnemyPatrolState());
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);


        if (_stateMachine.currentState is EnemyPatrolState)
        {
            // rotate enemy to face direction of movement
            Vector3 direction = _nav.velocity;
            if (direction.magnitude > 0.1f)
            {
                // just rotate right and left

            }
        }
        else
        {
            animator.SetBool("isPatroling", false);
        }
    }
}