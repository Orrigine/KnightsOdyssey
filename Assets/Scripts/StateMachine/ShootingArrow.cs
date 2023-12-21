using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ShootingArrow : StateMachineBehaviour
{
    PlayerAttack playerAttack;
    private float _timer = 0f;
    [SerializeField] private float _cooldown = 0.5f;
    EnemyPatrol enemyPatrol;
    GameObject _player;
    NavMeshAgent _agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerAttack = animator.GetComponent<PlayerAttack>();
        enemyPatrol = animator.GetComponent<EnemyPatrol>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_player.transform.position);
        _agent.isStopped = true;
        Vector2 direction = _player.transform.position - animator.transform.position;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);

        if (_timer < _cooldown)
        {
            _timer += Time.deltaTime;
        }
        else if (!enemyPatrol.Detected || _agent.path.corners.Length > 2)
        {   
            animator.SetBool("ReadyToShoot", false);
            animator.SetBool("OnChase", false);
        }
        else
        {
            _timer = 0f;
            playerAttack.Arrow(animator.transform.position, rotation);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
