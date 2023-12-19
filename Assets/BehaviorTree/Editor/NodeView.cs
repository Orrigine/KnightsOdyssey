using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
	private Node _node;

	public NodeView(Node node)
	{
		_node = node;

		title = node.Name;
		capabilities &= ~(Capabilities.Snappable);

		style.left = node.position.x;
		style.top = node.position.y;
	}


	public override void SetPosition(Rect newPos)
	{
		newPos.x = EditorUtility.RoundTo(newPos.x, BehaviorTreeView.gridSnapSize);
		newPos.y = EditorUtility.RoundTo(newPos.y, BehaviorTreeView.gridSnapSize);

		base.SetPosition(newPos);

		_node.position = newPos.position;
	}
}
