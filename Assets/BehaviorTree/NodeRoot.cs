using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRoot : Node
{
	public NodeRoot()
	{
		name = "Root Node";
	}

    protected override void OnStart()
    {

    }

	protected override void OnStop()
	{

	}

	protected override State OnUpdate()
	{
		if (children.Count == 0)
		{
			return State.Success;
		}

		return children[0].Update();
	}
}
