using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1IdleState")]
public class Enemy1IdleState : Enemy1BaseState
{
    public bool debug;
    [Tooltip("")]
    private bool shouldJustIdle;

    private float time = 1.0f;
    private float timer;

    public override void Enter()
    {
        base.Enter();
        shouldJustIdle = owner.IsIdleEnemy;
    }

    public override void HandleUpdate()
    {
        if (shouldJustIdle != true)
            stateMachine.Transition<Enemy1PatrolState>();
        if (CheckForDistanceFromFeet(collisionDistance, true)
            && CheckIfPlayerAlive() == true)
            stateMachine.Transition<Enemy1AttackState>();
        if (CheckForDistanceFromEyes(spottingDistance, true)
            && CheckForLOS()
            && CheckIfPlayerAlive()
            && CheckIfPlayerInFront())
                stateMachine.Transition<Enemy1AttackState>();
        if (debug)
            DebugTransitionToAttackState();
    }
    private void DebugTransitionToAttackState()
    {
        if (timer <= 0)
        {
            Debug.Log("Distance: " + (Vector3.Distance(owner.VisionOrigin.position, owner.PlayerTransform.position) < spottingDistance));
            Debug.Log("Raycast: " + CheckForLOS());
            Debug.Log("Alive: " + CheckIfPlayerAlive());
            Debug.Log("In front: " + CheckIfPlayerInFront());
            timer = time;
        }
        else
            timer -= Time.deltaTime;
    }
}
