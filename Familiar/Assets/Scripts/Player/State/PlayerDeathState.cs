using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerDeathState")]
public class PlayerDeathState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        owner.Anim.SetTrigger("die");
    }

    public override void HandleUpdate()
    {

    }
}