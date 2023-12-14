using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Node
{
    public enum State
    {
        Running,
        Success,
        Failure
    };

    [SerializeReference]
    [HideInInspector]
    protected List<Node> children = new List<Node>();

    private bool _started = false;

    public State Update()
    {
        if (!_started)
        {
            _started = true;
            OnStart();
        }

        State s = OnUpdate();

        if (s != State.Running)
        {
			OnStop();
			_started = false;
        }

        return s;
    }

    protected abstract void OnStart();
	protected abstract void OnStop();
	protected abstract State OnUpdate();
}
