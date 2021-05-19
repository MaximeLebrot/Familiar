using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1AttackState")]
public class Enemy1AttackState : Enemy1BaseState
{
    [SerializeField] private float grabDistance;
    [SerializeField] private float aggroLossDistance;
    [SerializeField] private float chaseSpeed;

    //timer och time som reguleras utav en difficulty level? hur länge man stannar i ljuset
    private float timer = 1.0f;
    private float time;
    public override void Enter()
    {
        base.Enter();
        owner.Anim.SetTrigger("roar");
    }

    public override void HandleUpdate()
    {
        if (CheckForDistance(aggroLossDistance, true)
            && CheckIfPlayerInFront() 
            && CheckIfPlayerAlive())
            AggroFeedback();

        if (CheckForDistance(aggroLossDistance, false)
            || CheckIfPlayerAlive() != true)
        {
            ResetAggro();
            stateMachine.Transition<Enemy1PatrolState>();
        }
        else if (CheckForDistance(grabDistance, true))
        {
            //feedback
            //start timer?
            //anim

            GrabPlayer();
        }
        else
        {
            //AggroTimer();
            GrabPlayer();
            //ChasePlayer();
        }
    }
    private void ResetAggro()
    {
        timer = 1.0f;
        //light.color...
    }
    private void AggroFeedback()
    {
        if (timer <= 0)
        {
            timer = time;
            GrabPlayer(); //kanske annan anim? kasta nåt? springa mot spelaren?
        }
        else
        {
            timer -= Time.deltaTime;

            //owner.Light.color.r = (1 - timer);
            //  Koppla till animation! som F2B
        }
    }
    private void ChasePlayer()
    {
        //owner.transform.LookAt(owner.vecToPlayer);
        owner.NavAgent.speed = chaseSpeed;
        owner.NavAgent.SetDestination(owner.VecToPlayer);
    }
    private void GrabPlayer()
    {
        owner.PlayerStats.Die();
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
