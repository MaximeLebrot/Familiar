using UnityEngine;

[CreateAssetMenu(menuName = "Enemy2/Enemy2IdleState")]
public class Enemy2IdleState : Enemy2BaseState
{
    [SerializeField, Tooltip("The distance from which the enemy aggros the player")]
    private float aggroDistance;

    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (owner.NavAgent.remainingDistance > 0.2f)
            GoToIdlePos();
        if (owner.NavAgent.remainingDistance < 0.15f && owner.Anim.GetBool("spiderWalk") == true)
            SetWalkAnim(false);
        if (Vector3.Distance(owner.Transform.position, owner.PlayerTransform.position) < aggroDistance 
            && owner.PlayerStats.Dead != true
            && CheckForLOS())
            stateMachine.Transition<Enemy2AttackState>();
        if (owner.Health == 0)
            stateMachine.Transition<Enemy2DefeatState>();
    }

    private void GoToIdlePos()
    {
        owner.NavAgent.SetDestination(owner.IdlePosition);
    }
    private void SetWalkAnim(bool set)
    {
        owner.Anim.SetBool("spiderWalk", set);
    }
}
