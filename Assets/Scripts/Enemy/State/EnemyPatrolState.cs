using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyState
{
    public bool Detected = false;
    [SerializeField] public Animator _animator;
    [SerializeField] private NavMeshAgent _nav;
    private int posX;
    private int posY;

    public void Awake()
    {
        // _animator = GetComponentInParent<Animator>();
        _nav = GetComponentInParent<NavMeshAgent>();
    }


    public override void Enter()
    {
        _animator.SetBool("isPatroling", true);
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        _animator.SetBool("isPatroling", false);
    }
}