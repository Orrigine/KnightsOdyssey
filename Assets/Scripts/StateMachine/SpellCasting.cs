using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpellCasting : StateMachineBehaviour
{
    PlayerAttack playerAttack;
    private float _timer = 0f;
    private float _timer2 = 0f;
    private Vector2 pos;
    [SerializeField] private float _cooldown = 0.5f;
    EnemyPatrol enemyPatrol;
    GameObject _player;
    private bool _casting;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerAttack = animator.GetComponent<PlayerAttack>();
        enemyPatrol = animator.GetComponent<EnemyPatrol>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_timer < _cooldown)
        {
            _timer += Time.deltaTime;
        }
        else if (!enemyPatrol.Detected)
        {
            animator.SetBool("ReadyToShoot", false);
            animator.SetBool("OnChase", false);
        }
        else
        {
            _timer2 += Time.deltaTime;
            if (!_casting)
            {
                playerAttack.FirePit(_player.transform.position, _player.transform.rotation, true);
                pos = _player.transform.position;
                _casting = true;
            }
            if(_timer2 > 2) 
            {   
                _timer = 0f; _timer2 = 0f;
                playerAttack.FirePit(pos, _player.transform.rotation, false);
                _casting = false;
            }
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
