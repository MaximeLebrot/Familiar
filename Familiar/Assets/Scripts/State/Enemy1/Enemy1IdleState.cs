using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1IdleState")]
public class Enemy1IdleState : Enemy1BaseState
{
    private float spottingDistance = 50.0f;
    public bool shouldJustIdle;
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy1 Entered Idle State");
        //owner.transform.position = new Vector3(-54, 0, 0);
    }

    public override void HandleUpdate()
    {
        //Debug.Log("enemy idle");
        if (!shouldJustIdle) //if player nära?
            stateMachine.Transition<Enemy1PatrolState>();
        if (!Physics.Raycast(owner.transform.position, owner.vecToPlayer, spottingDistance, owner.collisionMask))
            stateMachine.Transition<Enemy1AttackState>();
    }

    private void Idle()
    {

    }
}
