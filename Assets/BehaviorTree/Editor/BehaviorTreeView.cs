using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviorTreeView : GraphView
{
    public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits> {
    }


    public BehaviorTreeView()
    {

    }


    public void PopulateView(BehaviorTree tree)
    {
        // Create nodes.
        foreach (Node n in tree.nodes)
        {
            NodeView nodeView = new NodeView(n);
            AddElement(nodeView);
        }

        // Create connections.

    }
}
