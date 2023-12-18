using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviorTreeEditorWindow : EditorWindow
{
	public VisualTreeAsset behaviourTreeXml;
	public StyleSheet behaviorTreeStyle;

	private BehaviorTreeView _behaviorTreeView;


	[MenuItem("Blyat/suka")]
	public static void ShowWindow()
	{
		BehaviorTreeEditorWindow window = GetWindow<BehaviorTreeEditorWindow>();
	}

	public static void ShowWindow(BehaviorTree asset)
	{
		BehaviorTreeEditorWindow window = GetWindow<BehaviorTreeEditorWindow>();
		window.OpenTree(asset);
	}

	[OnOpenAsset]
	public static bool OnOpenAsset(int instanceId, int line)
	{
		if (Selection.activeObject is BehaviorTree)
		{
			ShowWindow(Selection.activeObject as BehaviorTree);
			return true;
		}
		return false;
	}


	public void CreateGUI()
	{
		VisualElement root = rootVisualElement;
		behaviourTreeXml.CloneTree(root);

		root.styleSheets.Add(behaviorTreeStyle);

		_behaviorTreeView = root.Q<BehaviorTreeView>();
	}



	private void OpenTree(BehaviorTree tree)
	{
		_behaviorTreeView?.PopulateView(tree);
	}
}
