using UnityEngine;

[CreateAssetMenu(menuName = "Enemy2/Enemy2AttackState")]
public class Enemy2AttackState : Enemy2BaseState
{
    public float hitDistance;
    public int hitCooldown;
    public bool canHit;
    public float aggroDistance = 10.0f;
    public float spottingDistance = 50.0f;
    public float damage;
    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enemy2 Entered Attack State");
        Attack();
    }

    public override void HandleUpdate()
    {
        if (hitCooldown < 500)
            hitCooldown++;
        if (hitCooldown >= 500)
        {
            hitCooldown = 0;
            canHit = true;
        }
        if (/*Physics.Raycast(owner.transform.position, owner.vecToPlayer, spottingDistance, owner.collisionMask) 
            || */Vector3.Distance(owner.transform.position, owner.player.transform.position) > aggroDistance)
            stateMachine.Transition<Enemy2IdleState>();
        else if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < hitDistance)
            HitPlayer();
        else
            Attack();
        if (owner.health == 0)
            stateMachine.Transition<Enemy2DefeatState>();
        //if (Physics.Linecast(owner.transform.position, owner.vecToPlayer, owner.collisionMask))
        //stateMachine.Transition<Enemy2PatrolState>();
        //if (owner.zapped)
        //stateMachine.Transition<Enemy2DefeatState>();
    }
    private void Attack()
    {
        Debug.DrawLine(owner.transform.position, owner.vecToPlayer, Color.red);

        owner.navAgent.SetDestination(owner.player.transform.position);
    }
    private void HitPlayer()
    {
        owner.navAgent.ResetPath();
        if (canHit)
            DamagePlayer();
        canHit = false;
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