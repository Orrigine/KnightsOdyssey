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
            blackboard.SetValue<float>("Cooldown", blackboard.GetValue<float>("CoolDownValue"));
            Vector3 pos = blackboard.GetValue<Vector3>("Destination") - context.gameObject.transform.position;
            pos = pos.normalized * 2;
            pos += context.gameObject.transform.position;
            context.gameObject.GetComponent<PlayerAttack>().Attack(pos, context.gameObject.transform.rotation);
            return State.Success;
        }
        return State.Success;
    }
}
