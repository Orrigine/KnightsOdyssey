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
        behaviourTreeInstance.SetBlackboardValue("Destination", playerPos.transform.position);
    }
}
