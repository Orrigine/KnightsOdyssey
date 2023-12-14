using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeSetValue : Node
{
	// The key/value pair from the blackboard instance.
	[SerializeReference]
	public BlackboardKeyValueBase key;

	// The update value.
	[SerializeReference]
	public BlackboardKeyValueBase setValue;

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
