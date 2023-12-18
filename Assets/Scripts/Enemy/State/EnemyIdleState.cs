using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#region EnemyIdleState
public class EnemyIdleState : EnemyState
{
    [SerializeField] public Animator _animator;
    public override void Enter()
    {
        _animator.SetBool("isIdle", true);
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        _animator.SetBool("isIdle", false);
        base.Exit();
    }
}
#endregion