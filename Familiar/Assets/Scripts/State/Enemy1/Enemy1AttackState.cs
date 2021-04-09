using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyAttackState")]
public class Enemy1AttackState : Enemy1BaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy Entered Attack State");
        Attack();
    }

    public override void HandleUpdate()
    {
        if (Physics.Raycast(owner.transform.position, owner.vecToPlayer, 50.0f, owner.collisionMask))
            stateMachine.Transition<Enemy1PatrolState>();
        //if (Physics.Linecast(owner.transform.position, owner.vecToPlayer, owner.collisionMask))
        //stateMachine.Transition<Enemy1PatrolState>();
        Attack();
        //Debug.Log("enemy attacking");
    }

    private void Attack()
    {

        owner.navAgent.SetDestination(player.transform.position);
    }
}
