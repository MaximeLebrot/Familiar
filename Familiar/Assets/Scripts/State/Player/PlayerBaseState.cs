using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    [SerializeField] protected float moveSpeed;

    protected Player owner;
    protected Controller player;
    protected StateMachine stateMachine;

    // Methods
    public override void Enter()
    {
        Debug.Log("Entered Base State");
        player = owner.playerController;
        player.acceleration = moveSpeed;
    }

    public override void Initialize(StateMachine stateMachine, object owner)
    {
        this.owner = (Player)owner;
        this.stateMachine = stateMachine;
        Debug.Log(owner.GetType());
    }

}
