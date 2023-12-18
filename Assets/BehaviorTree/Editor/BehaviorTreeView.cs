using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviorTreeView : GraphView
{
    public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits> {
    }

	public static int gridSnapSize = 16;

	private BehaviorTree _tree;


	public BehaviorTreeView()
    {
        Insert(0, new GridBackground());
    }


	public void OpenTree(BehaviorTree tree)
	{
		ClearView();
		_tree = tree;
		PopulateView();
	}

	private void ClearView()
	{
		DeleteElements(graphElements.ToList());
	}

	private void PopulateView()
    {
		ClearView();

        // Create nodes.
        foreach (Node n in _tree.nodes)
        {
            NodeView nodeView = new NodeView(n);
            AddElement(nodeView);
        }

        // Create connections.

    }

	public NodeView CreateNode(System.Type type, Vector2 position, NodeView parentView)
	{
		// Update model
		Node node = (Node) type.Instantiate();
		node.position = position;

		_tree.AddNode(node);

		if (parentView != null)
		{
			//serializer.AddChild(parentView.node, node);
		}

		// Update View
		NodeView nodeView = CreateNodeView(node);
		if (parentView != null)
		{
			//AddChild(parentView, nodeView);
		}

		return nodeView;
	}

	public NodeView CreateNodeWithChild(System.Type type, Vector2 position, NodeView childView)
	{
		// Update Model
		Node node = (Node) type.Instantiate();
		node.position = position;

		// Delete the childs previous parent
		/*foreach (var connection in childView.input.connections)
		{
			var childParent = connection.output.node as NodeView;
			serializer.RemoveChild(childParent.node, childView.node);
		}
		// Add as child of new node.
		serializer.AddChild(node, childView.node);

		// Update View
		NodeView nodeView = CreateNodeView(node);
		if (nodeView != null)
		{
			AddChild(nodeView, childView);
		}*/

		return null;
	}

	NodeView CreateNodeView(Node node)
	{
		NodeView nodeView = new NodeView(node);
		AddElement(nodeView);
		//nodeView.OnNodeSelected = OnNodeSelected;
		return nodeView;
	}

	public void SelectNode(NodeView nodeView)
	{
		ClearSelection();
		if (nodeView != null)
		{
			AddToSelection(nodeView);
		}
	}


	public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
	{
		CreateNodeWindow.Show(this, evt.mousePosition, null);
	}
}
