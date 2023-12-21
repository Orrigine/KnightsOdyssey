using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeCompareValue : NodeAction
{
	// The key/value pair from the blackboard instance.
	[SerializeReference]
	public BlackboardKeyValueBase key;

	// The value to be matched.
	[SerializeReference]
	public BlackboardKeyValueBase compareValue;


	public NodeCompareValue()
	{
		name = "Compare Value";
	}


	protected override void OnStart()
	{
		
	}

	protected override void OnStop()
	{

	}

	protected override State OnUpdate()
	{
		if (key.Equals(compareValue))
		{
			return State.Success;
		}

		return State.Failure;
	}
}
