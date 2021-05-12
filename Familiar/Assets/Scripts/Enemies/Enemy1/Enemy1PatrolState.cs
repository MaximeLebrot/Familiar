using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1PatrolState")]
public class Enemy1PatrolState : Enemy1BaseState
{
    public bool canSeeDebug;
    private bool checkRaycastPlayer;

    private float time = 1.0f;
    private float timer;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy1 Entered Patrol State");
        owner.navAgent.autoBraking = false;
        Patrol();
    }

    public override void HandleUpdate()
    {
        if (!owner.navAgent.pathPending && owner.navAgent.remainingDistance < 0.5f)
            Patrol();

        if (CheckForDistance()
            && CheckForLOS()
            && CheckIfPlayerAlive()
            && CheckIfPlayerInFront())
            stateMachine.Transition<Enemy1AttackState>();
        if (owner.IsZapped)
            stateMachine.Transition<Enemy1DefeatState>(); 
        if (canSeeDebug)
        {
            if (CheckForLOS() == true)
                Debug.DrawRay(owner.transform.position, owner.vecToPlayer, Color.red);
            if (timer <= 0)
            {
                Debug.Log("Distance: " + CheckForDistance());
                Debug.Log("Raycast: " + CheckForLOS());
                Debug.Log("Alive: " + CheckIfPlayerAlive());
                Debug.Log("In front: " + CheckIfPlayerInFront());
                timer = time;
            }
            else
                timer -= Time.deltaTime;
        }

    }
    private void Patrol()
    {
        if (owner.points.Length == 0)
            return;

        owner.navAgent.destination = owner.points[owner.destPoint].position;

        owner.destPoint = (owner.destPoint + 1) % owner.points.Length;
    }
}
