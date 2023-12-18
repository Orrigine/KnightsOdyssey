using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyState
{
    [SerializeField] public Animator _animator;

    public override void Enter()
    {
        _animator.SetBool("isAttacking", true);
        StartCoroutine(Attack());
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        _animator.SetBool("isAttacking", false);
        base.Exit();
    }

    IEnumerator Attack()
    {
        while (true)
        {

            yield return new WaitForSeconds(5f);
        }
    }
}
