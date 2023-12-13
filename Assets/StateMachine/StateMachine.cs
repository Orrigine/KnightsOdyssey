using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : ScriptableObject
{
    [SerializeField]
    private List<State> states;

    [SerializeField]
    private State _defaultState = null;

    private State _current = null;
    

    public StateMachine()
    {
        
    }

    public void Init()
    {
		_current = _defaultState;
        if (_current != null)
        {
            _current.OnEnter();
        }
	}

    public void Update()
    {
        State newState = _current.Update();
        if (newState != null)
        {
            _current.OnLeave();
            _current = newState;
            _current.OnEnter();
        }
    }


    public void AddState(State state)
    {
        states.Add(state);
    }

    public void AddTransition(State.Transition transition)
    {
        transition.stateBefore.AddTransition(transition);
    }
}
