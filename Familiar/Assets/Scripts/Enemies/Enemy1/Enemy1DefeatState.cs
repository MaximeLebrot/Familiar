using UnityEngine;

[CreateAssetMenu(menuName = "Enemy1/Enemy1DefeatState")]
public class Enemy1DefeatState : Enemy1BaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy1 Entered Defeat State");
        Defeated();
    }

    public override void HandleUpdate()
    {
        //Debug.Log("enemy defeated");
    }

    private void Defeated()
    {

    }
}
