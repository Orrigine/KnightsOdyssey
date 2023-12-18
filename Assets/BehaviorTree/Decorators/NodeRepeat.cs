using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NodeRepeat : Node
{
	public bool restartOnSuccess = true;
	public bool restartOnFailure = false;
	public int maxRepeats = 0;

	int iterationCount = 0;


	public NodeRepeat()
	{
		name = "Repeat";
	}


	protected override void OnStart()
	{
		iterationCount = 0;
	}

	protected override void OnStop()
	{

	}

	protected override State OnUpdate()
	{
		if (children.Count == 0) return State.Failure;

		switch (children[0].Update())
		{
		case State.Running: break;
		case State.Failure:
			if (restartOnFailure)
			{
				iterationCount++;
				if (iterationCount >= maxRepeats && maxRepeats > 0)
					return State.Failure;
				else
					return State.Running;
			}
			else
			{
				return State.Failure;
			}
		case State.Success:
			if (restartOnSuccess)
			{
				iterationCount++;
				if (iterationCount >= maxRepeats && maxRepeats > 0)
					return State.Success;
				else
					return State.Running;
			}
			else
			{
				return State.Success;
			}
		}
		return State.Running;
	}
}
