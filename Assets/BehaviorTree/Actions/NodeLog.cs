using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeLog : Node
{
	[SerializeField]
	public string message;

	protected override void OnStart()
	{
	}

	protected override void OnStop()
	{
	}

	protected override State OnUpdate()
	{
		Debug.Log(message);
		return State.Success;
	}
}
