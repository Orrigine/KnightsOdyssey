using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyState
{
    [SerializeField] public Animator _animator;
    [SerializeField] public NavMeshAgent _nav;
    private int posX;
    private int posY;
    public override void Enter()
    {
        _animator.SetBool("isPatroling", true);
        StartCoroutine(Patrol());
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void Exit()
    {
        _animator.SetBool("isPatroling", false);
        base.Exit();
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            posX = Random.Range(-10, 10);
            posY = Random.Range(-10, 10);

            Vector3 destination = new Vector3(posX + transform.position.x, posY + transform.position.y, 0);
            _nav.SetDestination(destination);

            yield return new WaitForSeconds(5f);
        }
    }
}