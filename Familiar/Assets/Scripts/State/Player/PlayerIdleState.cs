using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerIdleState")]
public class PlayerIdleState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Idle State");
        player.grounded = true;
    }

    public override void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded)
            stateMachine.Transition<PlayerJumpState>();
        if (player.input.magnitude > 0 || player.velocity.magnitude > 0.1)
            stateMachine.Transition<PlayerMovingState>();
    }

    private void Idle()
    {

    }
}
