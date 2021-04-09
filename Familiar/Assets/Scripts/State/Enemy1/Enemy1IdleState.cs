using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1IdleState")]
public class Enemy1IdleState : Enemy1BaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy1 Entered Idle State");

    }

    public override void HandleUpdate()
    {
        //Debug.Log("enemy idle");
        if (true) //if player nära?
            stateMachine.Transition<Enemy1PatrolState>();
    }

    private void Idle()
    {

    }
}
