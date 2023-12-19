using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyState
{
    public bool Detected = false;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _nav;
    private int posX;
    private int posY;

    public void Awake()
    {
        _animator = GetComponentInParent<Animator>();
        _nav = GetComponentInParent<NavMeshAgent>();
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        base.Exit();
    }
}