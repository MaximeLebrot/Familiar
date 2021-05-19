using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1PatrolState")]
public class Enemy1PatrolState : Enemy1BaseState
{
    [SerializeField] private bool canSeeDebug;

    private float time = 1.0f;
    private float timer;

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
        //if (canSeeDebug)
            //DebugTransitionToAttackState();

    }
    private void DebugTransitionToAttackState()
    {
        if (CheckForLOS() == true)
            Debug.DrawRay(owner.Transform.position, owner.VecToPlayer, Color.red);
        if (timer <= 0)
        {
            Debug.Log("Distance: " + CheckForDistanceFromFeet(spottingDistance, true));
            Debug.Log("Raycast: " + CheckForLOS());
            Debug.Log("Alive: " + CheckIfPlayerAlive());
            Debug.Log("In front: " + CheckIfPlayerInFront());
            timer = time;
        }
        else
            timer -= Time.deltaTime;
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
