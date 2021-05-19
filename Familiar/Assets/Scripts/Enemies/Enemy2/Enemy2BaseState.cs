using UnityEngine;

public class Enemy2BaseState : State
{
    [SerializeField, Tooltip("The move speed controls the nav agents speed and the animation speed: animSpeed = moveSpeed / 10;")]
    protected float moveSpeed;
    [SerializeField, Tooltip("The animation speed")]
    protected float animSpeed;

    private static int CollisionLayer = 7; //the collision layer

    [Tooltip("A reference to the owner of this state machine")]
    protected Enemy2 owner;
    [Tooltip("A reference to this state machine")]
    protected StateMachine stateMachine;

    // Methods
    public override void Enter()
    {
        owner.NavAgent.speed = moveSpeed;
        if (animSpeed == 0)
            animSpeed = moveSpeed / 10;
        owner.Anim.speed = animSpeed;
    }

    public override void Initialize(StateMachine stateMachine, object owner)
    {
        this.owner = (Enemy2)owner;
        this.stateMachine = stateMachine;
    }
    protected bool CheckForLOS()
    {
        RaycastHit hit;
        if (Physics.Raycast(owner.Transform.position, owner.VecToPlayer, out hit, 50.0f))
        {
            if (hit.collider.gameObject.layer == CollisionLayer)
                return false;
        }
        return true;
    }

}
