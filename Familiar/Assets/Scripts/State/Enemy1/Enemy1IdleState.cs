using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyIdleState")]
public class Enemy1IdleState : Enemy1BaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy Entered Idle State");

    }

    public override void HandleUpdate()
    {
        Debug.Log("enemy idle");
        if (true) //if player nära?
            stateMachine.Transition<Enemy1PatrolState>();
    }

    private void Idle()
    {

    }
}
