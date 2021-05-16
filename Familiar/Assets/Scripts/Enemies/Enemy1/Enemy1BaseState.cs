using UnityEngine;

public class Enemy1BaseState : State
{
    [SerializeField, Tooltip("The move speed controls the nav agents speed and the animation speed: animSpeed = moveSpeed / 10;")]
    protected float moveSpeed;
    [SerializeField, Tooltip("The distance from which the enemy can sense the player. Starts from the \"Eyes\" component")]
    protected float spottingDistance;
    [SerializeField, Tooltip("This distance from which the player is considered to be colliding with the enemy. Default value = 3.0f")]
    protected float collisionDistance;
    [SerializeField, Tooltip("The angle in which the enemy can see the player. Calculation: dot > visionAngle = can see player. 0.707 = 90°")]
    private float visionAngle;
    [SerializeField, Tooltip("The playback speed of the animator. Default value = moveSpeed / 10")]
    private float animSpeed;

    [Tooltip("The direction of the vector to the player")]
    private Vector3 directionToPlayer;
    [Tooltip("The layer of collision")]
    private static int CollisionLayer = 7;

    [Tooltip("A reference to the owner of this state")]
    protected Enemy1 owner;
    [Tooltip("A reference to the state machine instace tied to this object")]
    protected StateMachine stateMachine;

    public override void Enter()
    {
        SetDefaultValues();
    }

    public override void Initialize(StateMachine stateMachine, object owner)
    {
        this.owner = (Enemy1)owner;
        this.stateMachine = stateMachine;
    }

    private void SetDefaultValues()
    {
        owner.NavAgent.speed = moveSpeed;
        if (animSpeed == 0)
            animSpeed = moveSpeed / 10;
        owner.Anim.speed = animSpeed;
        if (collisionDistance == 0)
            collisionDistance = 3.0f;
    }
    protected bool CheckIfPlayerInFront()
    {
        directionToPlayer = owner.VecToPlayer.normalized;
        directionToPlayer.y = 0;
        return Vector3.Dot(directionToPlayer.normalized, owner.Transform.forward) > visionAngle;
    }

    protected bool CheckForLOS()
    {
        RaycastHit hit;
        if (Physics.Raycast(owner.VisionOrigin.position, owner.VecToPlayer - owner.HeightOffset, out hit, 50.0f))
        {
            if (hit.collider.gameObject.layer != CollisionLayer)
                return true;
        }
        return false;
        //!Physics.Raycast(owner.transform.position, owner.vecToPlayer, spottingDistance, owner.collisionMask) //old old way
        //if (Physics.Raycast(owner.transform.position, owner.vecToPlayer, out hit, 50.0f)) //old way
        //{
        //    if (hit.transform.CompareTag("Player"))/*hit.collider.gameObject.tag == "Player"*/
        //    {
        //        return true;
        //    }
        //}
        //return false;
    }

    protected bool CheckForDistance()
    {
        return (Vector3.Distance(owner.Transform.position, owner.PlayerTransform.position) < spottingDistance);
    }

    protected bool CheckIfPlayerAlive()
    {
        if (owner.PlayerStats.Dead != true)
            return true;
        return false;
    }
}
