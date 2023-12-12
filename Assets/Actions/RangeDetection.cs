using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using Unity.VisualScripting;

[System.Serializable]

public class RangeDetection : ActionNode
{
    public GameObject playerPos;
    public NodeProperty<Vector2> m_playerPos;
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        playerPos = GameObject.FindGameObjectWithTag("Player");
        m_playerPos.Value = playerPos.transform.position;
        if (playerPos == null)
        {
            Debug.Log("Player is null");
            return State.Failure;
        }
        else if (Vector2.Distance(m_playerPos.Value, context.transform.position) < 10)
        {
            Vector3 m_playerPos2 = playerPos.transform.position;
            blackboard.SetValue("Destination", m_playerPos2);
            return State.Failure;
        }
        else
        {
            return State.Success;
            
        }
    }
}
