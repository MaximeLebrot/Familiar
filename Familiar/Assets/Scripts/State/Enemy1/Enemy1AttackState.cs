using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1AttackState")]
public class Enemy1AttackState : Enemy1BaseState
{
    public float grabDistance = 2.0f;
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy1 Entered Attack State");
        Attack();
    }

    public override void HandleUpdate()
    {
        if (Physics.Raycast(owner.transform.position, owner.vecToPlayer, 50.0f, owner.collisionMask))
            stateMachine.Transition<Enemy1PatrolState>();
        else if (Vector3.Distance(owner.transform.position, player.transform.position) < grabDistance)
        {
            GrabPlayer();
            owner.navAgent.ResetPath();
        }
        else
        {
            Attack();
        }
        //if (Physics.Linecast(owner.transform.position, owner.vecToPlayer, owner.collisionMask))
        //stateMachine.Transition<Enemy1PatrolState>();
    }

    private void Attack()
    {
        Debug.DrawLine(owner.transform.position, owner.vecToPlayer, Color.red);

        owner.navAgent.SetDestination(/*player.transform.position*/owner.vecToPlayer);
    }

    private void GrabPlayer()
    {
        player.velocity = Vector3.zero;
        //load level eller respawn checkpoint
    }
}
