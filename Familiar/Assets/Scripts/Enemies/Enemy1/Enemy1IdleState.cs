using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1IdleState")]
public class Enemy1IdleState : Enemy1BaseState
{
    private bool shouldJustIdle;
    public override void Enter()
    {
        base.Enter();
        shouldJustIdle = owner.IsIdleEnemy;
    }

    public override void HandleUpdate()
    {
        //Debug.Log("enemy idle");
        if (shouldJustIdle != true)
            stateMachine.Transition<Enemy1PatrolState>();
        if (Vector3.Distance(owner.transform.position, owner.PlayerTransform.position) < spottingDistance
            && CheckForLOS()
            && CheckIfPlayerAlive()
            && CheckIfPlayerInFront())
                stateMachine.Transition<Enemy1AttackState>();
    }
}
