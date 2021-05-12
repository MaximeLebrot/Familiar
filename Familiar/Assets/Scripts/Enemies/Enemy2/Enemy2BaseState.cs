using UnityEngine;

public class Enemy2BaseState : State
{
    [Tooltip("The move speed controls the nav agents speed and the animation speed: animSpeed = moveSpeed / 10;")]
    [SerializeField] protected float moveSpeed;
    protected float animSpeed;

    private static int CollisionLayer = 7; //the collision layer

    protected Enemy2 owner;
    protected StateMachine stateMachine;

    // Methods
    public override void Enter()
    {
        owner.navAgent.speed = moveSpeed;
        animSpeed = moveSpeed / 10;
        owner.anim.speed = animSpeed;
    }

    public override void Initialize(StateMachine stateMachine, object owner)
    {
        this.owner = (Enemy2)owner;
        this.stateMachine = stateMachine;
    }
    protected bool CheckForLOS()
    {
        RaycastHit hit;
        if (Physics.Raycast(owner.transform.position, owner.vecToPlayer, out hit, 50.0f))
        {
            if (hit.collider.gameObject.layer == CollisionLayer)
                return false;
        }
        return true;
    }

}
