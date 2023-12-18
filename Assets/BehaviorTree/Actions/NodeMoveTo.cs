using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMoveTo : Node
{
	public NodeMoveTo()
	{
		name = "Move To";
	}


	protected override void OnStart()
	{
	}

	protected override void OnStop()
	{
	}

	protected override State OnUpdate()
	{
		return State.Failure;
	}
}
