using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    private int posX;
    private int posY;
    public bool Detected = false;
    private int Timer = 0;
    private bool _triggerPatrol = false;
    private Coroutine _patrol;

    NavMeshAgent _nav;

    // Ajoutez une référence à EnemyStateMachine
    [SerializeField] private EnemyStateMachine _enemyStateMachine;

    // Start is called before the first frame update
    public void Start()
    {
        _nav = GetComponent<NavMeshAgent>();

    }


    // Update is called once per frame
    public void Update()
    {
        // // Vérifiez l'état de EnemyStateMachine
        if (_enemyStateMachine != null && _enemyStateMachine.currentState is EnemyPatrolState)
        {
            if (!Detected && _patrol == null)
            {
                // Debug.Log(this.gameObject.name + " is patrolling");
                _patrol = StartCoroutine(Patrol());

            }
        }

        if (Detected)
        {
            if (_patrol != null)
            {
                StopCoroutine(_patrol);
            }
            _triggerPatrol = false;
        }
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(this.gameObject.name + " : Player detected");
            if (_patrol != null)
            {
                StopCoroutine(_patrol);
            }
            Detected = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Detected = false;
            _patrol = StartCoroutine(Patrol());
        }
    }
}
