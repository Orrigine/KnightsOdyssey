using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    private int posX;
    private int posY;
    public bool Aggro = false;

    NavMeshAgent _nav;

    // Start is called before the first frame update
    void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        InvokeRepeating("Patrol", 0, 5);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Patrol()
    {
        // patrol on a random range between -10 and 10 without going out of bounds
        posX = Random.Range(-10, 10);
        posY = Random.Range(-10, 10);
        if (posX > 10)
        {
            posX = 10;
        }
        else if (posX < -10)
        {
            posX = -10;
        }
        if (posY > 10)
        {
            posY = 10;
        }
        else if (posY < -10)
        {
            posY = -10;
        }
        Vector3 destination = new Vector3(posX + transform.position.x, posY + transform.position.y, 0);
        _nav.SetDestination(destination);



        // out of bounds check

    }
}
