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

    //timer och time som reguleras utav en difficulty level? hur länge man stannar i ljuset
    //the higher the time the easier the game is
    [Tooltip("The maximum time the player can stand in the light before being caught. Translated to seconds")]
    private float time;
    [Tooltip("The timer controlled by how long the player stands in the lightVisionDistance")]
    private float timer;
    [Tooltip("The color that is set to the lantern of the enemy. Works as feedback for the player")]
    private Color color = new Color();
    [Tooltip("Makes sure a section of the code only runs once")]
    private bool hasRan;

    private bool grabbingPlayer;
    private static readonly float aggroTime = 1.5f;
    private float aggroTimer;

    public override void Enter()
    {
        if (time == 0)
            InitializeDifficulty();
        owner.NavAgent.autoBraking = true;
        timer = time;
        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (CheckForDistanceFromFeet(grabDistance, true) 
            && CheckIfPlayerAlive() == true)
        {
            GrabPlayer();
        }
        if (grabbingPlayer == true)
        {
            GrabSequence();
        }
        else if (CheckForDistanceFromFeet(lightVisionDistance, true)
            && CheckIfPlayerInFront() == true
            && CheckIfPlayerAlive() == true)
        {
            if (owner.Light != null)
                AggroFeedback();
        }
        else if (CheckForDistanceFromFeet(aggroLossDistance, false)
            || CheckIfPlayerAlive() != true
            || CheckForLOS() != true
            || CheckIfPlayerInFront() != true)
            ResetAggro(true);
    }

    private void ResetAggro(bool doPatrol)
    {
        if (owner.IsIdleEnemy == true)
            stateMachine.Transition<Enemy1PatrolState>();
        hasRan = false;
        if (owner.Light != null)
        {
            if (timer < time)
            {
                InverseLight(timer += Time.deltaTime);
            }
            else
            {
                owner.Anim.SetBool("discover", false);
                timer = time;
                SetColorToRedWithTime(timer);
                if (doPatrol == true)
                    stateMachine.Transition<Enemy1PatrolState>();
            }
        }
    }

    private void InverseLight(float timer)
    {
        if (timer > time)
            timer = time;

        float g = timer / time;

        color.r = 1f;
        color.g = g;
        color.b = 0f;
        color.a = 1f;

        owner.Light.color = color;
    }

    private void AggroFeedback()
    {
        if (hasRan != true)
        {
            LookAtPlayer();
            hasRan = true;
        }

        if (timer <= 0)
            GrabPlayer();
        else
            SetColorToRedWithTime(timer -= Time.deltaTime);
    }

    private void SetColorToRedWithTime(float timer)
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
        if (owner.Light != null)
        {
            color.r = 1f;
            color.g = 0f;
            color.b = 0f;
            color.a = 1f;
            owner.Light.color = color;

            color.r = 1f;
            color.g = 1f;
            color.b = 0f;
            color.a = 1f;
        }
        timer = time;
        grabbingPlayer = true;
        aggroTimer = aggroTime;
        owner.Anim.SetTrigger("roar");
        owner.PlayerStats.Die();
    }

    private void GrabSequence()
    {
        LookAtPlayer();
        if (aggroTimer <= 0)
            grabbingPlayer = false;
        else
            aggroTimer -= Time.deltaTime;
    }

    private void LookAtPlayer()
    {
        owner.Anim.SetBool("discover", true);
        owner.Transform.LookAt(owner.PlayerTransform.position);
        //owner.Transform.LookAt(owner.VecToPlayer);
        owner.NavAgent.velocity = Vector3.zero;
    }

    private void InitializeDifficulty()
    {
        int difficulty = Stats.Instance.Difficulty;
        switch (difficulty)
        {
            case 1:
                SetEasyDifficulty();
                break;
            case 2:
                SetNormalDifficulty();
                break;
            case 3:
                SetHardDifficulty();
                break;
            case 4:
                SetImpossibleDifficulty();
                break;
        }
    }

    private void SetEasyDifficulty()
    {
        time = 3;
    }

    private void SetNormalDifficulty()
    {
        time = 2;
    }

    private void SetHardDifficulty()
    {
        time = 1;
    }

    private void SetImpossibleDifficulty()
    {
        time = 0.5f;
    }
}
