using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class ShortRangeAttack : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(blackboard.GetValue<float>("Cooldown") <= 0)
        {
            blackboard.SetValue<float>("Cooldown", 1.5f);
            context.gameObject.GetComponent<PlayerAttack>().Attack(context.gameObject.GetComponent<Transform>().position, context.gameObject.GetComponent<Transform>().rotation);
            Debug.Log("Attack");
            return State.Success;
        }
        return State.Success;
    }
}
