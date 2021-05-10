using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Enemy1/Enemy1AttackState")]
public class Enemy1AttackState : Enemy1BaseState
{
    public float grabDistance;
    public float aggroLossDistance;
    private RaycastHit hit;
    public override void Enter()
    {
        base.Enter();
        owner.anim.SetTrigger("roar");
        owner.anim.speed = animSpeed;
        Debug.Log("Enemy1 Entered Attack State");
        //FoundPlayer(); //
        //ChasePlayer();
    }

    public override void HandleUpdate()
    {
        //if (Physics.Raycast(owner.transform.position, owner.vecToPlayer, 50.0f, owner.collisionMask))
        //    stateMachine.Transition<Enemy1PatrolState>();
        if (Vector3.Distance(owner.transform.position, owner.player.transform.position) > aggroLossDistance || playerStats.ded/*|| !Physics.Raycast(owner.transform.position, owner.vecToPlayer, out hit, spottingDistance, owner.collisionMask)*/)
            stateMachine.Transition<Enemy1PatrolState>();
        else if (Vector3.Distance(owner.transform.position, owner.player.transform.position) < grabDistance && canSeePlayer)
        {
            GrabPlayer();
        }
        else
        {
            ChasePlayer();
        }
        //if (Physics.Linecast(owner.transform.position, owner.vecToPlayer, owner.collisionMask))
        //stateMachine.Transition<Enemy1PatrolState>();
    }

    private void ChasePlayer()
    {
        //Debug.DrawLine(owner.transform.position, owner.vecToPlayer, Color.red);

        owner.navAgent.SetDestination(/*player.transform.position*/owner.vecToPlayer);
    }
    private void GrabPlayer()
    {
        playerStats.Die();
        canSeePlayer = false;
        stateMachine.Transition<Enemy1PatrolState>();
        //kanske teleport tillbaka?
        //owner.navAgent.acceleration = 0;
        //owner.navAgent.speed = 0;
        //owner.navAgent.isStopped = true;
        //owner.navAgent.ResetPath();

        //owner.StartCoroutine(stillensec());
        //playerController.velocity = Vector3.zero;
        //playerStats.Respawn(owner.playerRespawnLocation, 1.0f);
    }
}
