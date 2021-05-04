using UnityEngine;

[CreateAssetMenu(menuName = "Enemy2/Enemy2IdleState")]
public class Enemy2IdleState : Enemy2BaseState
{
    public float aggroDistance;
    public float spottingDistance;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy2 Entered Idle State");
    }

    public override void HandleUpdate()
    {
        Idle();
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < aggroDistance 
            /*&& !Physics.Raycast(owner.transform.position, owner.vecToPlayer, spottingDistance, owner.collisionMask)*/)
            stateMachine.Transition<Enemy2AttackState>();
        if (owner.health == 0)
            stateMachine.Transition<Enemy2DefeatState>();
        //if (owner.zapped)
        //    stateMachine.Transition<Enemy2DefeatState>();
    }

    private void Idle()
    {
        if (owner.navAgent.remainingDistance > 0.1f)
            owner.navAgent.SetDestination(owner.idlePosition);
    }
}
