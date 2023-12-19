using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviorTreeEditorWindow : EditorWindow
{
	public VisualTreeAsset behaviourTreeXml;
	public StyleSheet behaviorTreeStyle;

	public static BehaviorTreeEditorWindow Instance;

	public BehaviorTreeView behaviorTreeView;


	[MenuItem("Blyat/suka")]
	public static void ShowWindow()
	{
		BehaviorTreeEditorWindow window = GetWindow<BehaviorTreeEditorWindow>();
		Instance = window;
	}

	public static void ShowWindow(BehaviorTree asset)
	{
		BehaviorTreeEditorWindow window = GetWindow<BehaviorTreeEditorWindow>();
		Instance = window;
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

		behaviorTreeView = root.Q<BehaviorTreeView>();

		behaviorTreeView.styleSheets.Add(behaviorTreeStyle);
	}



	private void OpenTree(BehaviorTree tree)
	{
		behaviorTreeView.OpenTree(tree);
	}
}
