using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerJumpState")]
public class PlayerJumpState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Jump State");
        Jump();
    }

    public override void HandleUpdate()
    {
        //Debug.Log("player jumping");
        if (player.grounded)
            stateMachine.Transition<PlayerMovingState>();
        //falling state?
    }

    private void Jump()
    {
        player.Jump();
    }
}
