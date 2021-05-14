using UnityEngine;

public class Enemy1BaseState : State
{
    [Tooltip("The move speed controls the nav agents speed and the animation speed: animSpeed = moveSpeed / 10;")]
    [SerializeField] protected float moveSpeed;
    [Tooltip("The distance from which the enemy can sense the player")]
    [SerializeField] protected float spottingDistance;
    [Tooltip("The angle in which the enemy can see the player. Calculation: dot > visionAngle = can see player. 0.707 = 90°")]
    [SerializeField] private float visionAngle;
    protected float animSpeed;

    private Vector3 direction;
    private float dot;
    private static int CollisionLayer = 7;

    protected Enemy1 owner;
    protected StateMachine stateMachine;

    public override void Enter()
    {
        owner.NavAgent.speed = moveSpeed;
        animSpeed = moveSpeed / 10;
        owner.Anim.speed = animSpeed;
    }

    public override void Initialize(StateMachine stateMachine, object owner)
    {
        this.owner = (Enemy1)owner;
        this.stateMachine = stateMachine;
    }

    protected bool CheckIfPlayerInFront()
    {
        direction = owner.VecToPlayer.normalized;
        dot = Vector3.Dot(direction, owner.transform.forward);
        if (dot > visionAngle)
            return true;
        return false;
    }

    protected bool CheckForLOS()
    {
        RaycastHit hit;
        if (Physics.Raycast(owner.transform.position, owner.VecToPlayer, out hit, 50.0f))
        {
            if (hit.collider.gameObject.layer == CollisionLayer)
            {
                Debug.DrawRay(owner.transform.position, owner.VecToPlayer, Color.red);
                return false;
            }
            Debug.DrawRay(owner.transform.position, owner.VecToPlayer, Color.cyan);
            return true;
        }
        Debug.DrawRay(owner.transform.position, owner.VecToPlayer, Color.red);
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
        if (Vector3.Distance(owner.transform.position, owner.PlayerTransform.position) < spottingDistance)
            return true;
        return false;
    }

    protected bool CheckIfPlayerAlive()
    {
        if (owner.PlayerStats.ded != true)
            return true;
        return false;
    }
}
