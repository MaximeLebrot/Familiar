using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerDeathState")]
public class PlayerDeathState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        owner.Anim.SetTrigger("die");
        //owner.anim.SetTrigger("death");
    }

    public override void HandleUpdate()
    {

    }
}