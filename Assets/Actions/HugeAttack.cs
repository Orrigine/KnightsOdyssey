using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class HugeAttack : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        Vector3 pos = blackboard.GetValue<Vector3>("Destination") - context.gameObject.transform.position;
        pos = pos.normalized * 2;
        pos += context.gameObject.transform.position;
        Vector3 direction = blackboard.GetValue<Vector3>("Destination") - context.transform.position;
        context.gameObject.GetComponent<PlayerAttack>().HugeAttack(pos, Quaternion.FromToRotation(Vector3.left, direction));
        return State.Success;
    }
}
