using UnityEngine;

public class PlayerBaseState : State
{
    [SerializeField, Tooltip("The movement speed of the player in this state")]
    protected float moveSpeed;

    [Tooltip("A reference to the \"Player\" script attached to the palyer game object")]
    protected AbilitySystem.Player owner;
    [Tooltip("A reference to the \"Controller\" script attached to the player game object")]
    protected Controller player;
    [Tooltip("A reference to the state machine instance attached to the player game object")]
    protected StateMachine stateMachine;

    // Methods
    public override void Enter()
    {
        if (player != owner.PlayerController)
            player = owner.PlayerController;
    }

    public override void Initialize(StateMachine stateMachine, object owner)
    {
        this.owner = (AbilitySystem.Player)owner;
        this.stateMachine = stateMachine;
    }

}
