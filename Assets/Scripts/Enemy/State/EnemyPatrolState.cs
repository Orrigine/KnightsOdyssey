using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyState
{
    public bool Detected = false;
    private Animator _animator;
    private NavMeshAgent _nav;
    private int posX;
    private int posY;

    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();
    }


    public override void Enter()
    {
        _animator.SetBool("isPatroling", true);
        if (!Detected)
        {
            StartCoroutine(Patrol());
        }
    }

    public override void Execute()
    {
        if (Detected)
        {
            StopCoroutine(Patrol());
        }
        else if (!Detected)
        {
            StartCoroutine(Patrol());
        }
    }

    public override void Exit()
    {
        _animator.SetBool("isPatroling", false);
        StopCoroutine(Patrol());
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