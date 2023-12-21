using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeSetValue : NodeAction
{
	// The key/value pair from the blackboard instance.
	[SerializeReference]
	public BlackboardKeyValueBase key;

	// The update value.
	[SerializeReference]
	public BlackboardKeyValueBase setValue;


	public NodeSetValue()
	{
		name = "Set Value";
	}


	protected override void OnStart()
	{

	}

	protected override void OnStop()
	{

	}

	protected override State OnUpdate()
	{
		key.CopyFrom(setValue);

		return State.Success;
	}
}
