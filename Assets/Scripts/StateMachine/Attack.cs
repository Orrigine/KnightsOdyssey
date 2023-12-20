using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : StateMachineBehaviour
{
    PlayerAttack playerAttack;
    NavMeshAgent agent;
    GameObject playerPos;
    private float _timer = 0f;
    [SerializeField] private float _cooldown = 0.5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerAttack = animator.GetComponent<PlayerAttack>();
        agent = animator.GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("Player");
        agent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(playerPos.transform.position);
        animator.SetFloat("AttackRange", agent.remainingDistance);
        Debug.Log(agent.remainingDistance);
        if(_timer < _cooldown)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _timer = 0f;
            Vector3 pos = playerPos.transform.position - animator.gameObject.transform.position;
            pos = pos.normalized * 2;
            pos += animator.gameObject.transform.position;
            playerAttack.Attack(pos, animator.transform.rotation);
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
