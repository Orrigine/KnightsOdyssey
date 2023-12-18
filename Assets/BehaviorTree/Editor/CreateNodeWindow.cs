using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;
using System;

public class CreateNodeWindow : ScriptableObject, ISearchWindowProvider
{
	BehaviorTreeView treeView;
	NodeView source;
	bool isSourceParent;


	public static void Show(BehaviorTreeView treeView, Vector2 mousePosition, NodeView source, bool isSourceParent = false)
	{
		Vector2 screenPoint = GUIUtility.GUIToScreenPoint(mousePosition);
		CreateNodeWindow searchWindowProvider = CreateInstance<CreateNodeWindow>();
		searchWindowProvider.Initialize(treeView, source, isSourceParent);
		SearchWindowContext windowContext = new SearchWindowContext(screenPoint, 240, 320);
		SearchWindow.Open(windowContext, searchWindowProvider);
	}


	public void Initialize(BehaviorTreeView treeView, NodeView source, bool isSourceParent)
	{
		this.treeView = treeView;
		this.source = source;
		this.isSourceParent = isSourceParent;
	}

	public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
	{
		var tree = new List<SearchTreeEntry>
		{
			new SearchTreeGroupEntry(new GUIContent("Create Node"), 0),
		};

		// Action nodes can only be added as children
		if (isSourceParent || source == null)
		{
			tree.Add(new SearchTreeGroupEntry(new GUIContent("Actions")) { level = 1 });
			var types = TypeCache.GetTypesDerivedFrom<NodeAction>();
			foreach (var type in types)
			{
				Action invoke = () => CreateNode(type, context);
				tree.Add(new SearchTreeEntry(new GUIContent(((Node) type.Instantiate()).Name)) { level = 2, userData = invoke });
			}
		}

		{
			tree.Add(new SearchTreeGroupEntry(new GUIContent("Composites")) { level = 1 });
			var types = TypeCache.GetTypesDerivedFrom<NodeComposite>();
			foreach (var type in types)
			{
				Action invoke = () => CreateNode(type, context);
				tree.Add(new SearchTreeEntry(new GUIContent(((Node) type.Instantiate()).Name)) { level = 2, userData = invoke });
			}
		}

		{
			tree.Add(new SearchTreeGroupEntry(new GUIContent("Decorators")) { level = 1 });
			var types = TypeCache.GetTypesDerivedFrom<NodeDecorator>();
			foreach (var type in types)
			{
				Action invoke = () => CreateNode(type, context);
				tree.Add(new SearchTreeEntry(new GUIContent(((Node) type.Instantiate()).Name)) { level = 2, userData = invoke });
			}
		}

		return tree;
	}

	public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
	{
		System.Action invoke = (System.Action)searchTreeEntry.userData;
		invoke();
		return true;
	}

	public void CreateNode(System.Type type, SearchWindowContext context)
	{
		BehaviorTreeEditorWindow editorWindow = BehaviorTreeEditorWindow.Instance;

		var windowMousePosition = editorWindow.rootVisualElement.ChangeCoordinatesTo(editorWindow.rootVisualElement.parent, context.screenMousePosition - editorWindow.position.position);
		var graphMousePosition = editorWindow.behaviorTreeView.contentViewContainer.WorldToLocal(windowMousePosition);
		var nodeOffset = new Vector2(-75, -20);
		var nodePosition = graphMousePosition + nodeOffset;

		NodeView createdNode;
		if (source != null)
		{
			if (isSourceParent)
			{
				createdNode = treeView.CreateNode(type, nodePosition, source);
			}
			else
			{
				createdNode = treeView.CreateNodeWithChild(type, nodePosition, source);
			}
		}
		else
		{
			createdNode = treeView.CreateNode(type, nodePosition, null);
		}

		treeView.SelectNode(createdNode);
	}
}
