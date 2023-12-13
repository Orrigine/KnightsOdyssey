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

    public State Update()
    {
        return OnUpdate();
    }

    protected abstract void OnStart();
	protected abstract void OnStop();
	protected abstract State OnUpdate();
}
