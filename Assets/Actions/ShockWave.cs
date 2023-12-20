using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using static UnityEditor.Experimental.GraphView.GraphView;

[System.Serializable]
public class ShockWave : ActionNode
{
    private int _iteration = 1;
    protected override void OnStart() {
        Vector3 pos = blackboard.GetValue<Vector3>("Destination") - context.gameObject.transform.position;
        pos = pos.normalized * _iteration * 2;
        pos += context.gameObject.transform.position;
        Vector3 direction = blackboard.GetValue<Vector3>("Destination") - context.transform.position;
        context.gameObject.GetComponent<PlayerAttack>().Shockwave(pos, Quaternion.FromToRotation(Vector3.down, direction));
        _iteration++;
        blackboard.SetValue("Iteration", _iteration);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (_iteration == 4) {
            _iteration = 0;
            blackboard.SetValue("Iteration", _iteration);
            return State.Failure;
        }
        return State.Success;
    }
}