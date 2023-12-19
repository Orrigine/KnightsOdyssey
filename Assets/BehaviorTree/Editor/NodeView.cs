using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
	private Node _node;

	private Port _inputPort;
	private Port _outputPort;

	public NodeView(Node node)
	{
		_node = node;

		title = node.Name;
		capabilities &= ~(Capabilities.Snappable);

		style.left = node.position.x;
		style.top = node.position.y;

		CreatePorts();
	}


	public override void SetPosition(Rect newPos)
	{
		newPos.x = Round(newPos.x, BehaviorTreeView.gridSnapSize);
		newPos.y = Round(newPos.y, BehaviorTreeView.gridSnapSize);

		base.SetPosition(newPos);

		_node.position = newPos.position;
	}


	private void CreatePorts()
	{
		if (_node is NodeAction) {
			_inputPort = new NodePort(Direction.Input, Port.Capacity.Single);
			_outputPort = null;
		}
		else if (_node is NodeComposite) {
			_inputPort = new NodePort(Direction.Input, Port.Capacity.Single);
			_outputPort = new NodePort(Direction.Output, Port.Capacity.Multi);
		}
		else if (_node is NodeDecorator) {
			_inputPort = new NodePort(Direction.Input, Port.Capacity.Single);
			_outputPort = new NodePort(Direction.Output, Port.Capacity.Single);
		}
		else if (_node is NodeRoot) {
			_inputPort = null;
			_outputPort = new NodePort(Direction.Output, Port.Capacity.Single);
		}

		if (_inputPort != null) {
			_inputPort.portName = "";
			inputContainer.Add(_inputPort);
		}
		if (_outputPort != null) {
			_outputPort.portName = "";
			outputContainer.Add(_outputPort);
		}
	}

	public static float Round(float value, int nearestInteger)
	{
		return (Mathf.FloorToInt(value / nearestInteger)) * nearestInteger;
	}
}
