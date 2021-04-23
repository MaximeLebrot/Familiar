using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Enemy1/Enemy1AttackState")]
public class Enemy1AttackState : Enemy1BaseState
{
    public float grabDistance;
    public float spottingDistance;
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy1 Entered Attack State");
        Attack();
    }

    public override void HandleUpdate()
    {
        //if (Physics.Raycast(owner.transform.position, owner.vecToPlayer, 50.0f, owner.collisionMask))
        //    stateMachine.Transition<Enemy1PatrolState>();
        if (Vector3.Distance(owner.transform.position, player.transform.position) > spottingDistance)
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
        //Debug.DrawLine(owner.transform.position, owner.vecToPlayer, Color.red);

        owner.navAgent.SetDestination(/*player.transform.position*/owner.vecToPlayer);
    }

    private void GrabPlayer()
    {
        player.velocity = Vector3.zero;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //load level eller respawn checkpoint
    }
}
