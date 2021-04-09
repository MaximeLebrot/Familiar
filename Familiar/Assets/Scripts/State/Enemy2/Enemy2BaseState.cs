using UnityEngine;

public class Enemy2BaseState : State
{
    [SerializeField] protected float moveSpeed;

    protected Enemy2 owner;
    protected Controller player;
    protected StateMachine stateMachine;

    // Methods
    public override void Enter()
    {
        //Debug.Log("Enemy2 Entered Base State");
        player = owner.playerController;
        owner.navAgent.speed = moveSpeed;
    }

    public override void Initialize(StateMachine stateMachine, object owner)
    {
        this.owner = (Enemy2)owner;
        this.stateMachine = stateMachine;
        Debug.Log("Initialized owner: " + owner.GetType());
        Debug.Log("Initialized stateMachine: " + stateMachine.GetType());
    }

}
