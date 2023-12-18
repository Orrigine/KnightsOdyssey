using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    private int posX;
    private int posY;
    public bool Detected = false;

    private Coroutine _patrol;

    NavMeshAgent _nav;

    // Start is called before the first frame update
    void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        if(!Detected)
        {
           _patrol = StartCoroutine(Patrol());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Patrol()
    {
        while (true)
        {
            posX = Random.Range(-10, 10);
            posY = Random.Range(-10, 10);

            Debug.Log("Patrolling");

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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Detected = false;
            _patrol = StartCoroutine(Patrol());
        }
    }
}
