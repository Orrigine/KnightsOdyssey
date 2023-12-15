using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineInstance : MonoBehaviour
{
    [SerializeReference]
    private StateMachine _stateMachineAsset;

    public StateMachine stateMachine = null;


    // Start is called before the first frame update
    void Start()
    {
        if (_stateMachineAsset)
        {
            stateMachine = _stateMachineAsset.Clone();
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (stateMachine)
        {
            stateMachine.Update();
        }
    }
}
