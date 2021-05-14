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
        owner.Anim.SetTrigger("guardDeath");
        owner.NavAgent.isStopped = true;
        owner.RemoveNavMesh();
    }
}
