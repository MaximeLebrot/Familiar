using UnityEngine;

[CreateAssetMenu(menuName = "Enemy2/Enemy2AttackState")]
public class Enemy2AttackState : Enemy2BaseState
{
    public float hitDistance = 2.0f;
    public int hitCooldown;
    public bool canHit;
    public float aggroDistance = 10.0f;
    public float spottingDistance = 50.0f;
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy2 Entered Attack State");
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
        if (Physics.Raycast(owner.transform.position, owner.vecToPlayer, spottingDistance, owner.collisionMask) 
            || Vector3.Distance(owner.transform.position, player.transform.position) > aggroDistance)
            stateMachine.Transition<Enemy2IdleState>();
        else if (Vector3.Distance(owner.transform.position, player.transform.position) < hitDistance)
            hitPlayer();
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

        owner.navAgent.SetDestination(player.transform.position);
    }
    private void hitPlayer()
    {
        owner.navAgent.ResetPath();
        if (canHit)
            player.health--;
        canHit = false;
    }
}