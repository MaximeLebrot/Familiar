using UnityEngine;

[CreateAssetMenu(menuName = "Enemy2/Enemy2AttackState")]
public class Enemy2AttackState : Enemy2BaseState
{
    [SerializeField, Tooltip("The distance from which the enemy can hit the player")]
    private float hitDistance;
    [SerializeField, Tooltip("The cooldown between attacks")]
    private float attackCooldown;
    [SerializeField, Tooltip("The distance from which the enemy loses aggro")]
    private float aggroLossDistance;
    [SerializeField, Tooltip("The damage dealt by the enemy")]
    private float damage;

    public override void Enter()
    {
        base.Enter();
        owner.Anim.SetBool("spiderWalk", true);
        owner.CanAttack = true;
        owner.NavAgent.SetDestination(owner.PlayerTransform.position);
    }

    public override void HandleUpdate()
    {
        //Chase();
        if (owner.Health == 0)
        {
            stateMachine.Transition<Enemy2DefeatState>();
            return;
        }
        if (Vector3.Distance(owner.Transform.position, owner.PlayerTransform.position) > aggroLossDistance /* vector3distance from idle pos*/)
            stateMachine.Transition<Enemy2IdleState>();
        else if (Vector3.Distance(owner.Transform.position, owner.PlayerTransform.position) < hitDistance && owner.CanAttack)
            HitPlayer();
        else
            Chase();
    }
    private void Chase()
    {
        //Debug.DrawLine(owner.Transform.position, owner.vecToPlayer, Color.red);
        if (owner.NavAgent.remainingDistance > 0.5f)
            owner.NavAgent.SetDestination(owner.PlayerTransform.position);
    }
    private void HitPlayer()
    {
        owner.Anim.SetTrigger("spiderAttack");
        //canHit = false;
        owner.StartCoroutine(owner.AttackCooldown(attackCooldown));
        //owner.navAgent.ResetPath();
        DamagePlayer();
    }
    private void DamagePlayer()
    {
        owner.PlayerStats.TakeDamage(damage);
        if (owner.PlayerStats.AbilitySystem.GetAttributeValue(AbilitySystem.GameplayAttributes.PlayerHealth) <= 0)
        {
            owner.PlayerStats.Die();
            stateMachine.Transition<Enemy2IdleState>();
        }
    }
}