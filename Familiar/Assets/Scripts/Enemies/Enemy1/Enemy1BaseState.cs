using UnityEngine;

public class Enemy1BaseState : State
{
    [SerializeField] protected float moveSpeed;

    protected Enemy1 owner;
    protected Controller playerController;
    protected AbilitySystem.Player playerStats;
    protected StateMachine stateMachine;

    // Methods
    public override void Enter()
    {
        //Debug.Log("Enemy1 Entered Base State");
        playerController = owner.player.GetComponent<Controller>();
        playerStats = owner.player.GetComponent<AbilitySystem.Player>();
        owner.navAgent.speed = moveSpeed;
    }

    public override void Initialize(StateMachine stateMachine, object owner)
    {
        this.owner = (Enemy1)owner;
        this.stateMachine = stateMachine;
        //Debug.Log("Initialized owner: " + owner.GetType());
        //Debug.Log("Initialized stateMachine: " + stateMachine.GetType());
    }

}
