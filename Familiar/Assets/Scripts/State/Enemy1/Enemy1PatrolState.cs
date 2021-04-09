using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1PatrolState")]
public class Enemy1PatrolState : Enemy1BaseState
{
    private bool turn;
    private float patrolSpeed;
    private float spottingDistance = 50.0f;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy1 Entered Patrol State");
        patrolSpeed = moveSpeed * Time.deltaTime;
    }

    public override void HandleUpdate()
    {
        //Debug.Log("enemy patrolling");
        Patrol();
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

    private void Patrol()
    {
        if (!turn)
            owner.navAgent.SetDestination(owner.patrolPoint1.position);
        else
            owner.navAgent.SetDestination(owner.patrolPoint2.position);
        if (turn && owner.transform.position.z == owner.patrolPoint2.position.z)
            turn = false;
        else if (!turn && owner.transform.position.z == owner.patrolPoint1.position.z)
            turn = true;
    }
}
