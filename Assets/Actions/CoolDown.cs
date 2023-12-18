using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CoolDown : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(blackboard.GetValue<float>("Cooldown") > 0)
        {
            blackboard.SetValue<float>("Cooldown", blackboard.GetValue<float>("Cooldown") - Time.deltaTime);
            return State.Success;
        }
        else if(blackboard.GetValue<float>("Cooldown") <= 0)
        {
            blackboard.SetValue<float>("Cooldown", blackboard.GetValue<float>("CoolDownValue"));
            return State.Success;
        }
        return State.Success;
    }
}
