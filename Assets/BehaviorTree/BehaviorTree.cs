using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Knight's Odyssey/Behavior Tree")]
public class BehaviorTree : ScriptableObject
{
	[SerializeReference]
	public Blackboard blackboard = new Blackboard();

	[SerializeReference]
	public List<Node> nodes = new List<Node>();

	[SerializeReference]
	public NodeRoot rootNode;


    public BehaviorTree()
    {
        rootNode = new NodeRoot();
        nodes.Add(rootNode);
    }

	public void Update()
	{
		rootNode.Update();
	}

	public BehaviorTree Clone()
	{
		return Instantiate(this);
	}
}
