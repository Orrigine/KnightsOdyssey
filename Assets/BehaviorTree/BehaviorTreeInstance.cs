using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BehaviorTreeInstance : MonoBehaviour
{
    [SerializeReference]
    private BehaviorTree _behaviorTreeAsset = null;

    public BehaviorTree behaviorTree = null;


    // Start is called before the first frame update
    void Start()
    {
        if (_behaviorTreeAsset)
        {
            behaviorTree = _behaviorTreeAsset.Clone();
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (behaviorTree)
        {
			behaviorTree.Update();
        }
    }

    public T GetBlackboardValue<T>(string key)
    {
		BlackboardKeyValue<T> b = behaviorTree.blackboard.Find<T>(key);
		if (b != null)
		{
            return b.Value;
		}
        return default(T);
	}

    public void SetBlackboardValue<T>(string key, T value)
    {
        BlackboardKeyValue<T> b = behaviorTree.blackboard.Find<T>(key);
        if (b != null)
        {
            b.Value = value;
        }
	}
}
