using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSequence : Node
{
	private int _current;


	protected override void OnStart()
	{
		_current = 0;
	}

	protected override void OnStop()
	{
		
	}

	protected override State OnUpdate()
	{
		for (int i = _current; i < children.Count; ++i)
		{
			_current = i;
			Node child = children[_current];

			switch (child.Update())
			{
				case State.Running: return State.Running;
				case State.Failure: return State.Failure;
				case State.Success: continue;
			}
		}

		return State.Success;
	}
}
