using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyPatrolState")]
public class Enemy1PatrolState : Enemy1BaseState
{
    private bool turn;
    private float patrolSpeed;
    private float spottingDistance = 50.0f;
    public LayerMask collisionMask;
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy Entered Patrol State");
        patrolSpeed = moveSpeed * Time.deltaTime;
    }

    public override void HandleUpdate()
    {
        Debug.Log("enemy patrolling");
        Patrol();
        //if (Vector3.Dot(owner.vecToPoint2.normalized, owner.vecToPlayer) > 0 && Physics.Raycast(owner.transform.position, owner.vecToPlayer, spottingDistance, collisionMask))
        //    stateMachine.Transition<Enemy1AttackState>();
        //if (Physics.Raycast(owner.transform.position, owner.vecToPlayer.normalized, spottingDistance, collisionMask))
        //    stateMachine.Transition<Enemy1AttackState>();
    }

    private void Patrol()
    {
        if (!turn)
            owner.transform.position = Vector3.MoveTowards(owner.transform.position, owner.transform2.position, patrolSpeed);
        else
            owner.transform.position = Vector3.MoveTowards(owner.transform.position, owner.transform1.position, patrolSpeed);
        if (owner.transform.position == owner.transform1.position || owner.transform.position == owner.transform2.position)
            turn = !turn;
    }
}
