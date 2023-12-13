using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

[Serializable]
public abstract class State
{
    [Serializable]
    public abstract class Transition
    {
        [SerializeReference]
        public State stateBefore;
		public State stateAfter;

		public abstract bool Pass();
    }


    [SerializeField]
    private string _name;

	public string Name => _name;

	[SerializeField]
    private List<Transition> _transitions;


    public State(string name)
    {
        _name = name;
    }

    public void AddTransition(Transition transition)
    {
        _transitions.Add(transition);
	}

    public State Update()
    {
        OnUpdate();

        foreach (Transition t in _transitions)
        {
            if (t.Pass())
            {
                return t.stateAfter;
            }
        }

        return null;
    }


	public abstract void OnEnter();
	public abstract void OnLeave();
	public abstract void OnUpdate();
}
