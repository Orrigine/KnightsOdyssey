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
        context.gameObject.GetComponent<PlayerAttack>().HugeAttack(context.gameObject.GetComponent<Transform>().position, context.gameObject.GetComponent<Transform>().rotation);
        return State.Success;
    }
}
