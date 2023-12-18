using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeLog : NodeAction
{
	[SerializeField]
	public string message;


	public NodeLog()
	{
		name = "Log";
	}


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
