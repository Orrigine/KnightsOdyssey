using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class GetHealth : ActionNode
{
    LifeSystem lifeSystem;
    [Tooltip("Amount of Health")] public int Health;
    public bool isFullLife = false;

    protected override void OnStart()
    {
        lifeSystem = context.gameObject.GetComponent<LifeSystem>();
        Health = lifeSystem.CurrentLife;
        if (Health == lifeSystem.MaxLife)
        {
            isFullLife = true;
        }
        else
        {
            isFullLife = false;
        }
        blackboard.SetValue("FullLife", isFullLife);
    }

    protected override void OnStop() {
        blackboard.SetValue("CurrentHealth", Health);
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
