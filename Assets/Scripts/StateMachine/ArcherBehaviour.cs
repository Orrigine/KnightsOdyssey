using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;
    GameObject playerPos;
    EnemyPatrol enemyPatrol;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        enemyPatrol = animator.GetComponent<EnemyPatrol>();
        playerPos = GameObject.FindGameObjectWithTag("Player");
        agent.isStopped = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(playerPos.transform.position);
        if (agent.path.corners.Length == 2 && enemyPatrol.Detected)
        {
            agent.isStopped = true;
            animator.SetBool("ReadyToShoot", true);
        }
        else
        {
            agent.isStopped = false;
            animator.SetBool("ReadyToShoot", false);
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
