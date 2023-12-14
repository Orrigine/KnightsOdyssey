using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    private int posX;
    private int posY;
    public bool Aggro = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Patrol", 0, 5);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Patrol()
    {
        Debug.Log("Patrolling");
        posX = Random.Range(-10, 10);
        posY = Random.Range(-10, 10);
        GetComponent<NavMeshAgent>().destination = new Vector3(posX + transform.position.x, posY + transform.position.y, 0);
    }
}
