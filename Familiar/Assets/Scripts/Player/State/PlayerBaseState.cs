using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    [SerializeField] protected float moveSpeed;

    protected AbilitySystem.Player owner;
    protected Controller player;
    protected StateMachine stateMachine;

    // Methods
    public override void Enter()
    {

    }

    public override void Initialize(StateMachine stateMachine, object owner)
    {
        this.owner = (AbilitySystem.Player)owner;
        this.stateMachine = stateMachine;
        //Debug.Log("Initialized owner: " + owner.GetType());
        //Debug.Log("Initialized stateMachine: " + stateMachine.GetType());
    }

}
