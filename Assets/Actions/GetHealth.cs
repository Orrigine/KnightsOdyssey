using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class GetHealth : ActionNode
{
    [Tooltip("Amount of Health")] public int Health = 1;
    public bool isFullLife = false;

    protected override void OnStart() {
       Health = context.gameObject.GetComponent<LifeSystem>().CurrentLife;
        if (Health == context.gameObject.GetComponent<LifeSystem>().MaxLife)
        {
            isFullLife = true;
            blackboard.SetValue("FullLife", true);
        }
        else
        {
            isFullLife = false;
        }
    }

    protected override void OnStop() {
        blackboard.SetValue("CurrentHealth", Health);
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
