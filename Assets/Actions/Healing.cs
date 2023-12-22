using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class Healing : ActionNode
{
    public float sleep = 3;
    public float startTime;
    LifeSystem lifeSystem;
    protected override void OnStart() {
        lifeSystem = context.gameObject.GetComponentInChildren<LifeSystem>();

        startTime = Time.time;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate()
    {
        if (Time.time -startTime > sleep)
        {
            if (lifeSystem.CurrentLife < lifeSystem.MaxLife)
            {
                lifeSystem.CurrentLife += 1;
            }
            return State.Success;
        }
        return State.Running;
    }
}
