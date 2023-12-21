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

    [SerializeReference]
    public Vector2 position;

    protected string name;
    public string Name => name;

    private bool _started = false;


    public Node()
    {
        // Default name.
        name = this.GetType().Name;
    }

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
