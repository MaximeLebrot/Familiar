using UnityEngine;

[CreateAssetMenu(menuName = "Enemy2/Enemy2IdleState")]
public class Enemy2IdleState : Enemy2BaseState
{
    [SerializeField] private float aggroDistance;
    [SerializeField] private float spottingDistance;

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enemy2 Entered Idle State");
    }

    public override void HandleUpdate()
    {
        if (owner.navAgent.remainingDistance > 0.2f)
            Idle();
        if (owner.navAgent.remainingDistance < 0.15f && owner.anim.GetBool("spiderWalk"))
            SetWalkAnim(false);
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < aggroDistance && !playerStats.ded
            /*&& !Physics.Raycast(owner.transform.position, owner.vecToPlayer, spottingDistance, owner.collisionMask)*/)
            stateMachine.Transition<Enemy2AttackState>();
        if (owner.health == 0)
            stateMachine.Transition<Enemy2DefeatState>();
        //if (owner.zapped)
        //    stateMachine.Transition<Enemy2DefeatState>();
    }

    private void Idle()
    {
        owner.navAgent.SetDestination(owner.idlePosition);
    }
    private void SetWalkAnim(bool set)
    {
        owner.anim.SetBool("spiderWalk", set);
    }
}
