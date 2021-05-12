using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1AttackState")]
public class Enemy1AttackState : Enemy1BaseState
{
    [SerializeField] private float grabDistance;
    [SerializeField] private float aggroLossDistance;
    [SerializeField] private float chaseSpeed;

    //timer och time som reguleras utav en difficulty level? hur l�nge man stannar i ljuset

    public override void Enter()
    {
        base.Enter();
        owner.anim.SetTrigger("roar");
    }

    public override void HandleUpdate()
    {
        if (Vector3.Distance(owner.transform.position, owner.playerTransform.position) > aggroLossDistance 
            || CheckIfPlayerAlive() == false)
            stateMachine.Transition<Enemy1PatrolState>();
        else if (Vector3.Distance(owner.transform.position, owner.playerTransform.position) < grabDistance)
        {
            //feedback
            //start timer?
            //anim

            GrabPlayer();
        }
        else
        {
            GrabPlayer();
            //ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        //owner.transform.LookAt(owner.vecToPlayer);
        owner.navAgent.speed = chaseSpeed;
        owner.navAgent.SetDestination(owner.vecToPlayer);
    }
    private void GrabPlayer()
    {
        owner.navAgent.isStopped = true;
        owner.playerStats.Die();
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
