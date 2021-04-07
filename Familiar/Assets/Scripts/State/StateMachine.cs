using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine
{
    // Attributes
    //[SerializeField] private State[] states;

    private Dictionary<Type, State> stateDictionary = new Dictionary<Type, State>();
    private State currentState;
    public StateMachine(object owner, State[] states)
    {
        Debug.Assert(states.Length > 0);
        foreach (State state in states)
        {
            State instantiated = UnityEngine.Object.Instantiate(state);
            instantiated.Initialize(this, owner);
            stateDictionary.Add(state.GetType(), instantiated);
            if (!currentState)
                currentState = instantiated;
        }
        currentState?.Enter();
    }
    // Methods
    //protected virtual void Awake()
    //{
    //    foreach (State state in states)
    //    {
    //        State instance = Instantiate(state);
    //        instance.Initialize(this);
    //        stateDictionary.Add(instance.GetType(), instance);
    //        if (currentState == null)
    //            currentState = instance;
    //    }
    //    currentState.Enter();
    //}

    public void Transition<T>() where T : State
    {
        currentState.Exit();
        currentState = stateDictionary[typeof(T)];
        currentState.Enter();
    }

    public void HandleUpdate()
    {
        //currentState?.EvaluateTransitions();
        currentState.HandleUpdate();
    }


    

    //public void ChangeState<T>() where T : State
    //{
    //    if (stateDictionary.ContainsKey(typeof(T)))
    //    {
    //        State instance = stateDictionary[typeof(T)];
    //        currentState?.Exit();
    //        currentState = instance;
    //        currentState.Enter();
    //    }
    //}
}
