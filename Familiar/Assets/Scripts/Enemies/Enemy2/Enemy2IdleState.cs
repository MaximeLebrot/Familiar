using UnityEngine;

[CreateAssetMenu(menuName = "Enemy2/Enemy2IdleState")]
public class Enemy2IdleState : Enemy2BaseState
{
    [Tooltip("The distance from which the enemy aggros the player")]
    [SerializeField] private float aggroDistance;

    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (owner.navAgent.remainingDistance > 0.2f)
            GoToIdlePos();
        if (owner.navAgent.remainingDistance < 0.15f && owner.anim.GetBool("spiderWalk") == true)
            SetWalkAnim(false);
        if (Vector3.Distance(owner.transform.position, owner.playerTransform.position) < aggroDistance 
            && owner.playerStats.ded != true
            && CheckForLOS())
            stateMachine.Transition<Enemy2AttackState>();
        if (owner.GetHealth() == 0)
            stateMachine.Transition<Enemy2DefeatState>();
    }

    private void GoToIdlePos()
    {
        owner.navAgent.SetDestination(owner.GetIdlePosition());
    }
    private void SetWalkAnim(bool set)
    {
        owner.anim.SetBool("spiderWalk", set);
    }
}
