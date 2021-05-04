using UnityEngine;

[CreateAssetMenu(menuName = "Enemy2/Enemy2AttackState")]
public class Enemy2AttackState : Enemy2BaseState
{
    public float hitDistance;
    public float attackCooldown;
    public float aggroLossDistance;
    public float damage;
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy2 Entered Attack State");
        owner.canAttack = true;
        owner.navAgent.SetDestination(owner.player.transform.position);
    }

    public override void HandleUpdate()
    {
        //Chase();
        if (owner.health == 0)
        {
            stateMachine.Transition<Enemy2DefeatState>();
            return;
        }
        if (/*Physics.Raycast(owner.transform.position, owner.vecToPlayer, spottingDistance, owner.collisionMask) 
            || */Vector3.Distance(owner.transform.position, owner.player.transform.position) > aggroLossDistance)
            stateMachine.Transition<Enemy2IdleState>();
        else if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < hitDistance && owner.canAttack)
            HitPlayer();
        else
            Chase();
        //if (Physics.Linecast(owner.transform.position, owner.vecToPlayer, owner.collisionMask))
        //stateMachine.Transition<Enemy2PatrolState>();
        //if (owner.zapped)
        //stateMachine.Transition<Enemy2DefeatState>();
    }
    private void Chase()
    {
        //Debug.DrawLine(owner.transform.position, owner.vecToPlayer, Color.red);
        if(owner.navAgent.remainingDistance > 0.5f)
            owner.navAgent.SetDestination(owner.player.transform.position);
    }
    private void HitPlayer()
    {
        //canHit = false;
        owner.StartCoroutine(owner.AttackCooldown(attackCooldown));
        //owner.navAgent.ResetPath();
        DamagePlayer();
    }
    private void DamagePlayer()
    {
        playerStats.GetAbilitySystem().TryApplyAttributeChange(AbilitySystem.GameplayAttributes.PlayerHealth, -damage);
        Debug.Log("Health = " + playerStats.GetAbilitySystem().GetAttributeValue(AbilitySystem.GameplayAttributes.PlayerHealth));
        //eventsystem ska kalla till UI att uppdatera (samma ska gälla för mana)
        if (playerStats.GetAbilitySystem().GetAttributeValue(AbilitySystem.GameplayAttributes.PlayerHealth) <= 0)
        {
            playerStats.Die();
        }
    }
}