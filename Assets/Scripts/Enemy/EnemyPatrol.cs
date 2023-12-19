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

    NavMeshAgent _nav;

    // Ajoutez une référence à EnemyStateMachine
    [SerializeField] private EnemyStateMachine _enemyStateMachine;

    // Start is called before the first frame update
    void Start()
    {
        _nav = GetComponent<NavMeshAgent>();

        // Obtenez le composant EnemyStateMachine

        _enemyStateMachine = GetComponentInChildren<EnemyStateMachine>();


        Debug.Log("EnemyPatrol Start");
        Debug.Log("State ======= ", _enemyStateMachine);
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifiez l'état de EnemyStateMachine
        if (_enemyStateMachine.currentState is EnemyPatrolState)
        {
            if (!Detected)
            {
                StartCoroutine(Patrol());
            }
        }

        if (Detected)
        {
            StopCoroutine(Patrol());
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

    private void OnTriggerEnter2D(Collider2D collision)
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
        }
    }
}
