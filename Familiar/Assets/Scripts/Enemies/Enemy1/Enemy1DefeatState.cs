using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1DefeatState")]
public class Enemy1DefeatState : Enemy1BaseState
{
    public override void Enter()
    {
        base.Enter();
        Defeated();
    }

    public override void HandleUpdate()
    {

    }

    private void Defeated()
    {
        owner.anim.SetTrigger("guardDeath");
        owner.navAgent.isStopped = true;
        owner.RemoveNavMesh();
    }
}
