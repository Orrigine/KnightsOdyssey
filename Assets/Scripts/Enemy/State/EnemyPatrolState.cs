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
    private Coroutine _patrol;

    public void Awake()
    {
        // _animator = GetComponentInParent<Animator>();
        _nav = GetComponentInParent<NavMeshAgent>();
    }


    public override void Enter()
    {
        _animator.SetBool("isPatroling", true);
        if (!Detected && _patrol == null)
        {
            _patrol = StartCoroutine(Patrol());
        }
        else if (Detected)
        {
            StopCoroutine(_patrol);

        }
    }

    public override void Execute()
    {


    }

    public override void Exit()
    {
        // StopCoroutine(_patrol);
        _animator.SetBool("isPatroling", false);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine(_patrol);
            Detected = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Detected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Detected = false;
            _patrol = StartCoroutine(Patrol());
        }
    }
}
