using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyDefeatState")]
public class Enemy1DefeatState : Enemy1BaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy Entered Defeat State");

    }

    public override void HandleUpdate()
    {
        //Debug.Log("enemy defeated");
    }

    private void Defeated()
    {

    }
}
