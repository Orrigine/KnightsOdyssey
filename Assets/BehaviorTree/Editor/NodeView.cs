using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
	private Node _node;

	public NodeView(Node node)
	{
		_node = node;
		this.title = node.Name;
	}
}
