using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1AttackState")]
public class Enemy1AttackState : Enemy1BaseState
{

    [SerializeField, Tooltip("The distance from which the enemy can grab the player")] 
    private float grabDistance;
    [SerializeField, Tooltip("The distance from which the enemy loses interest in the player")]
    private float aggroLossDistance;
    [SerializeField, Tooltip("The distance the light covers")]
    private float lightVisionDistance;

    //timer och time som reguleras utav en difficulty level? hur l�nge man stannar i ljuset
    //the higher the time the easier the game is
    [Tooltip("The maximum time the player can stand in the light before being caught")]
    private float time = 2.0f;
    [Tooltip("The timer controlled by how long the player stands in the lightVisionDistance")]
    private float timer;
    [Tooltip("The color that is set to the lantern of the enemy. Works as feedback for the player")]
    private Color color = new Color();

    public override void Enter()
    {
        timer = time;
        base.Enter();
        owner.Anim.SetTrigger("roar");

    }

    public override void HandleUpdate()
    {
        if (CheckForDistanceFromFeet(grabDistance, true))
            GrabPlayer();
        //vi kan / borde ha en annan variabel som �r light distance typ
        if (CheckForDistanceFromFeet(lightVisionDistance, true)
            && CheckIfPlayerInFront()
            && CheckIfPlayerAlive())
        {
            if (owner.Light != null)
                AggroFeedback();
        }
        if (CheckForDistanceFromFeet(aggroLossDistance, false)
            || CheckIfPlayerAlive() != true)
            ResetAggro();
        else
        {
            //AggroTimer();
            //GrabPlayer();
            //ChasePlayer();
        }
    }
    private void ResetAggro()
    {
        if (owner.Light != null)
        {
            timer = time;
            SetColor(timer);
        }
        stateMachine.Transition<Enemy1PatrolState>();
    }
    private void AggroFeedback()
    {
        owner.NavAgent.ResetPath();
        //owner.Anim.SetBool(Walk) //TODO sluta springa
        if (timer <= 0)
        {
            timer = time;
            GrabPlayer(); //kanske annan anim? kasta n�t? springa mot spelaren?
        }
        else
        {
            timer -= Time.deltaTime;
            SetColor(timer);
        }
    }
    private void SetColor(float timer)
    {
        //make sure timer does not go under the value of zero
        if (timer < 0)
            timer = 0;
        //sets the value of the green of RGB to a range between 0-1.
        float g = timer / time; 

        color.r = 1f;
        color.g = g;
        color.b = 0f;
        color.a = 1f;
        owner.Light.color = color;
    }
    private void GrabPlayer()
    {
        owner.PlayerStats.Die();
        ResetAggro();
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
