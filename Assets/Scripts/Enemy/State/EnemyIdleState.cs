using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#region EnemyIdleState
public class EnemyIdleState : EnemyState
{
    [SerializeField] private Animator _animator;

    public void Awake()
    {
        _animator = GetComponentInParent<Animator>();
        // _nav = GetComponentInParent<NavMeshAgent>();
    }
    public override void Enter()
    {
        _animator.SetBool("isIdling", true);
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        _animator.SetBool("isIdling", false);
        base.Exit();
    }
}
#endregion