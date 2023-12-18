using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class BossDetection : MonoBehaviour
{
    public BehaviourTreeInstance behaviourTreeInstance;
    private GameObject playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (behaviourTreeInstance != null)
            {
                behaviourTreeInstance.SetBlackboardValue("Destination", playerPos.transform.position);
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
        }
    }
}
