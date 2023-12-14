using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class PlayerFocus : MonoBehaviour
{
    public BehaviourTreeInstance behaviourTreeInstance;
    public GameObject playerPos;
    BlackboardKey<Vector3> key;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
        if (collision.gameObject.tag == "Player")
        {

            if (behaviourTreeInstance != null)
            {
                behaviourTreeInstance.SetBlackboardValue("Destination", playerPos.transform.position);
            }
            else
            {
                GetComponentInParent<EnemyPatrol>().Aggro = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (behaviourTreeInstance != null)
            {
                //behaviourTreeInstance.SetBlackboardValue("Destination", transform.position);
            }
            else
            {
                GetComponentInParent<Animator>().SetBool("OnChase", false);
                GetComponentInParent<EnemyPatrol>().Aggro = false;
            }
        }
    }
}
