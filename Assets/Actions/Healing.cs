using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class Healing : ActionNode
{
    public float sleep = 3;
    public float startTime;
    protected override void OnStart() {
        context.gameObject.GetComponent<SpriteRenderer>().color = Color.green;

        startTime = Time.time;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate()
    {
        if (Time.time -startTime > sleep)
        {
            context.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            return State.Success;
        }
        return State.Running;
    }
}
