using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] public GameObject enemyGO;
    [SerializeField] public Animator animator;
    NavMeshAgent _nav;

    // Start is called before the first frame update
    void Start()
    {
        enemyGO = GetComponent<GameObject>();
        animator = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

        // if the GameObject is patroling, make him face the direction he is going
        if (animator.GetBool("isPatroling"))
        {
            if (_nav.destination.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (_nav.destination.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            // animate the GameObject
            // animator

            // }
        }
    }
}