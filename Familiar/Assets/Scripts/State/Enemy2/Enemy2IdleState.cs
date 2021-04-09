using UnityEngine;

[CreateAssetMenu(menuName = "Enemy2/Enemy2IdleState")]
public class Enemy2IdleState : Enemy2BaseState
{
    public float aggroDistance = 15.0f;
    public float spottingDistance = 50.0f;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy2 Entered Idle State");

    }

    public override void HandleUpdate()
    {
        Idle();
        if (Vector3.Distance(owner.transform.position, player.transform.position) < aggroDistance 
            && !Physics.Raycast(owner.transform.position, owner.vecToPlayer, spottingDistance, owner.collisionMask))
            stateMachine.Transition<Enemy2AttackState>();
        if (owner.health == 0)
            stateMachine.Transition<Enemy2DefeatState>();
        //if (owner.zapped)
        //    stateMachine.Transition<Enemy2DefeatState>();
    }

    private void Idle()
    {
        if (owner.navAgent.remainingDistance > 0.1f)
            owner.navAgent.SetDestination(owner.idlePosition.position);
    }
}
