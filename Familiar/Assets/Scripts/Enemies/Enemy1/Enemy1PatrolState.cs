using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1PatrolState")]
public class Enemy1PatrolState : Enemy1BaseState
{
    [SerializeField, Tooltip("This distance from which the player is considered to be colliding with the enemy. Default value = 3.0f")]
    protected float collisionDistance;

    public override void Enter()
    {
        base.Enter();
        owner.NavAgent.autoBraking = false;
        Patrol();
    }

    public override void HandleUpdate()
    {
        if (!owner.NavAgent.pathPending && owner.NavAgent.remainingDistance < 0.5f)
            Patrol();
        if (CheckForDistanceFromFeet(collisionDistance, true)
            && CheckIfPlayerAlive() == true)
            stateMachine.Transition<Enemy1AttackState>();
        if (CheckForDistanceFromFeet(spottingDistance, true)
            && CheckForLOS()
            && CheckIfPlayerAlive()
            && CheckIfPlayerInFront())
            stateMachine.Transition<Enemy1AttackState>();
        if (owner.IsZapped)
            stateMachine.Transition<Enemy1DefeatState>(); 
    }

    private void Patrol()
    {
        if (owner.Points.Length == 0)
        {
            stateMachine.Transition<Enemy1IdleState>();
            return;
        }
        owner.NavAgent.destination = owner.Points[owner.DestPoint].position;
        owner.DestPoint = (owner.DestPoint + 1) % owner.Points.Length;
    }
}
