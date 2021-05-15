using UnityEngine;

[CreateAssetMenu(menuName = "Enemy2/Enemy2AttackState")]
public class Enemy2AttackState : Enemy2BaseState
{
    [Tooltip("The distance from which the enemy can hit the player")]
    [SerializeField] private float hitDistance;
    [Tooltip("The cooldown between attacks")]
    [SerializeField] private float attackCooldown;
    [Tooltip("The distance from which the enemy loses aggro")]
    [SerializeField] private float aggroLossDistance;
    [Tooltip("The damage dealt by the enemy")]
    [SerializeField] private float damage;
    public override void Enter()
    {
        base.Enter();
        owner.anim.SetBool("spiderWalk", true);
        owner.SetCanAttack(true);
        owner.navAgent.SetDestination(owner.playerTransform.position);
    }

    public override void HandleUpdate()
    {
        //Chase();
        if (owner.GetHealth() == 0)
        {
            stateMachine.Transition<Enemy2DefeatState>();
            return;
        }
        if (Vector3.Distance(owner.transform.position, owner.playerTransform.position) > aggroLossDistance /* vector3distance from idle pos*/)
            stateMachine.Transition<Enemy2IdleState>();
        else if (Vector3.Distance(owner.transform.position, owner.playerTransform.position) < hitDistance && owner.GetCanAttack())
            HitPlayer();
        else
            Chase();
    }
    private void Chase()
    {
        //Debug.DrawLine(owner.transform.position, owner.vecToPlayer, Color.red);
        if(owner.navAgent.remainingDistance > 0.5f)
            owner.navAgent.SetDestination(owner.playerTransform.position);
    }
    private void HitPlayer()
    {
        owner.anim.SetTrigger("spiderAttack");
        //canHit = false;
        owner.StartCoroutine(owner.AttackCooldown(attackCooldown));
        //owner.navAgent.ResetPath();
        DamagePlayer();
    }
    private void DamagePlayer()
    {
        owner.playerStats.TakeDamage(damage);
        if (owner.playerStats.AbilitySystem.GetAttributeValue(AbilitySystem.GameplayAttributes.PlayerHealth) <= 0)
        {
            owner.playerStats.Die();
            stateMachine.Transition<Enemy2IdleState>();
        }
    }
}