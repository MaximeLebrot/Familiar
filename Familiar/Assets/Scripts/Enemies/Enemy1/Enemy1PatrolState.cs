using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1PatrolState")]
public class Enemy1PatrolState : Enemy1BaseState
{
    public float spottingDistance = 10.0f;

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enemy1 Entered Patrol State");
        owner.navAgent.autoBraking = false;
        Patrol();
    }

    public override void HandleUpdate()
    {
        if (!owner.navAgent.pathPending && owner.navAgent.remainingDistance < 0.5f)
            Patrol();
        //if (!Physics.Raycast(owner.transform.position/* + new Vector3(0, 5, 0)*/, owner.vecToPlayer, spottingDistance, owner.collisionMask))
        //    stateMachine.Transition<Enemy1AttackState>();
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < spottingDistance
            /*&& Vector3.Dot(owner.navAgent.velocity, player.transform.position) > 0*/)
            stateMachine.Transition<Enemy1AttackState>();
        if (owner.IsZapped)
            stateMachine.Transition<Enemy1DefeatState>(); //zappedstate
        //if (!Physics.Linecast(owner.transform.position, owner.vecToPlayer, owner.collisionMask))
        //stateMachine.Transition<Enemy1AttackState>();
        //if (Vector3.Dot(owner.vecToPoint2.normalized, owner.vecToPlayer) > 0 && Physics.Raycast(owner.transform.position, owner.vecToPlayer, spottingDistance, collisionMask))
        //    stateMachine.Transition<Enemy1AttackState>();
    }

    private void Patrol()
    {
        if (owner.points.Length == 0)
            return;

        owner.navAgent.destination = owner.points[owner.destPoint].position;

        owner.destPoint = (owner.destPoint + 1) % owner.points.Length;
    }
}
