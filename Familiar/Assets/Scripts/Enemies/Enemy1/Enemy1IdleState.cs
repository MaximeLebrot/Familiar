using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1IdleState")]
public class Enemy1IdleState : Enemy1BaseState
{
    [SerializeField] private bool shouldJustIdle;
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        //Debug.Log("enemy idle");
        if (shouldJustIdle != true)
            stateMachine.Transition<Enemy1PatrolState>();
        if (Vector3.Distance(owner.transform.position, owner.playerTransform.position) < spottingDistance
            && CheckForLOS()
            && CheckIfPlayerAlive()
            && CheckIfPlayerInFront())
                stateMachine.Transition<Enemy1AttackState>();
    }
}
