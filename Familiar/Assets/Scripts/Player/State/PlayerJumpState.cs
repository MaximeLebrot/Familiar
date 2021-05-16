using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerJumpState")]
public class PlayerJumpState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        Jump();
    }

    public override void HandleUpdate()
    {
        if (owner.Dead == true)
            stateMachine.Transition<PlayerDeathState>();

        if (!Input.GetKey(KeyCode.Space))
            player.IsJumping = false;

        if (player.IsGrounded == true && player.InputVector.magnitude == 0 && player.Velocity.magnitude < 0.1) 
            stateMachine.Transition<PlayerIdleState>();

        if (player.IsGrounded == true && player.IsJumping != true)
            stateMachine.Transition<PlayerMovingState>();
    }

    private void Jump()
    {
        player.IsJumping = true;
        player.Jump();
    }

    public override void Exit()
    {
        player.IsJumping = false;
    }
}
