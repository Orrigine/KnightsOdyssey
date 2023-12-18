using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInverter : Node
{
	public NodeInverter()
	{
		name = "Inverter";
	}


	protected override void OnStart()
	{
		
	}

	protected override void OnStop()
	{
		
	}

	protected override State OnUpdate()
	{
		if (children.Count == 0) return State.Failure;

		switch (children[0].Update())
		{
		case State.Running: return State.Running;
		case State.Failure: return State.Success;
		case State.Success: return State.Failure;
		}
		return State.Failure;
	}
}
