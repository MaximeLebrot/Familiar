using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1IdleState")]
public class Enemy1IdleState : Enemy1BaseState
{
    [SerializeField, Tooltip("This distance from which the player is considered to be colliding with the enemy. Default value = 3.0f")]
    protected float collisionDistance;
    public bool debug;

    [Tooltip("Checks whether this enemy should just idle")]
    private bool shouldJustIdle;

    static float time = 2f;
    float timer = time;

    public override void Enter()
    {
        if (debug)
            Debug.Log("entering idle");
        base.Enter();
        shouldJustIdle = owner.IsIdleEnemy;
        owner.Transform.rotation = Quaternion.Euler(0f, 90f, 0f);
    }

    public override void HandleUpdate()
    {
        if (shouldJustIdle != true)
            stateMachine.Transition<Enemy1PatrolState>();
        if (CheckForDistanceFromFeet(collisionDistance, true)
            && CheckIfPlayerAlive() == true)
            stateMachine.Transition<Enemy1AttackState>();
        if (CheckForDistanceFromEyes(spottingDistance, true)
            && CheckForLOS()
            && CheckIfPlayerAlive()
            && CheckIfPlayerInFront())
                stateMachine.Transition<Enemy1AttackState>();
        //if (debug)
            //DebugChecks();
    }

    void DebugChecks()
    {
        if (timer <= 0)
        {
            timer = time;
            Debug.Log("Player in front " + CheckIfPlayerInFront());
            Debug.Log("Los: " + CheckForLOS());
            Debug.Log("Player in distance " + CheckForDistanceFromEyes(spottingDistance, true));
        }
        else
        {
            timer -= Time.deltaTime;
        }    
    }
}
