using UnityEngine;

public class Enemy1BaseState : State
{
    [SerializeField] protected float moveSpeed;

    protected Enemy1 owner;
    protected Controller player;
    protected StateMachine stateMachine;

    // Methods
    public override void Enter()
    {
        //Debug.Log("Enemy Entered Base State");
        player = owner.playerController;
        owner.moveSpeed = moveSpeed;
    }

    public override void Initialize(StateMachine stateMachine, object owner)
    {
        this.owner = (Enemy1)owner;
        this.stateMachine = stateMachine;
        Debug.Log("Initialized owner: " + owner.GetType());
        Debug.Log("Initialized stateMachine: " + stateMachine.GetType());
    }

}
