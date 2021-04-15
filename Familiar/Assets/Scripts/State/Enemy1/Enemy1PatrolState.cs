using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1PatrolState")]
public class Enemy1PatrolState : Enemy1BaseState
{
    private float spottingDistance = 50.0f;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy1 Entered Patrol State");
        owner.navAgent.autoBraking = false;
        GoToNextPoint();
    }

    public override void HandleUpdate()
    {
        if (!owner.navAgent.pathPending && owner.navAgent.remainingDistance < 0.5f)
            GoToNextPoint();
        //Debug.Log("enemy patrolling");
        //Patrol();
        //RaycastHit hit;
        //if (Physics.Linecast(owner.transform.position, player.transform.position, out hit, owner.collisionMask))
        //{
        //    if (hit.collider.CompareTag("Player"))
        //    {
        //        stateMachine.Transition<Enemy1AttackState>();
        //    }
        //}
        //Ray rayToPlayer = new Ray(owner.transform.position, owner.vecToPlayer);
        //Debug.DrawRay(rayToPlayer.origin, rayToPlayer.direction * 100, Color.red);
        if (!Physics.Raycast(owner.transform.position, owner.vecToPlayer, spottingDistance, owner.collisionMask))
            stateMachine.Transition<Enemy1AttackState>();
        if (owner.zapped)
            stateMachine.Transition<Enemy2DefeatState>();
        //if (!Physics.Linecast(owner.transform.position, owner.vecToPlayer, owner.collisionMask))
        //stateMachine.Transition<Enemy1AttackState>();
        //if (Vector3.Dot(owner.vecToPoint2.normalized, owner.vecToPlayer) > 0 && Physics.Raycast(owner.transform.position, owner.vecToPlayer, spottingDistance, collisionMask))
        //    stateMachine.Transition<Enemy1AttackState>();
    }

    private void GoToNextPoint()
    {
        if (owner.points.Length == 0)
            return;

        owner.navAgent.destination = owner.points[owner.destPoint].position;

        owner.destPoint = (owner.destPoint + 1) % owner.points.Length;
    }
}
