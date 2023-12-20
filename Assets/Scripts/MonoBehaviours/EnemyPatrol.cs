using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    private int posX;
    private int posY;
    public bool Detected = false;
<<<<<<< HEAD:Assets/Scripts/Enemy/EnemyPatrol.cs
    private int Timer = 0;
    private bool _triggerPatrol = false;
    private Coroutine Coroutine;
=======

    private Coroutine _patrol;
>>>>>>> origin:Assets/Scripts/MonoBehaviours/EnemyPatrol.cs

    NavMeshAgent _nav;

    // Ajoutez une référence à EnemyStateMachine
    [SerializeField] private EnemyStateMachine _enemyStateMachine;

    // Start is called before the first frame update
    public void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
<<<<<<< HEAD:Assets/Scripts/Enemy/EnemyPatrol.cs

        // Obtenez le composant EnemyStateMachine

        //_enemyStateMachine = GetComponentInParent<EnemyStateMachine>();

=======
        if(!Detected)
        {
           _patrol = StartCoroutine(Patrol());
        }
>>>>>>> origin:Assets/Scripts/MonoBehaviours/EnemyPatrol.cs
    }

    // Update is called once per frame
    public void Update()
    {
<<<<<<< HEAD:Assets/Scripts/Enemy/EnemyPatrol.cs
        // Vérifiez l'état de EnemyStateMachine
        if (_enemyStateMachine != null && _enemyStateMachine.currentState is EnemyPatrolState)
        {
            if (!Detected && Coroutine == null)
            {
                // _enemyStateMachine
                // _enemyStateMachine.ChangeState(_enemyStateMachine.patrolState);
                Coroutine = StartCoroutine(Patrol());
                // Debug.LogWarning("Coroutine is null");
            }
        }

        if (Detected)
        {
            StopCoroutine(Coroutine);
            _triggerPatrol = false;
        }
=======

>>>>>>> origin:Assets/Scripts/MonoBehaviours/EnemyPatrol.cs
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
            StopCoroutine(_patrol);
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
