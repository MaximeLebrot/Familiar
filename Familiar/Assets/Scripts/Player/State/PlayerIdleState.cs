using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerIdleState")]
public class PlayerIdleState : PlayerBaseState
{
    private static readonly float playerVelocityIdleTolerance = 0.1f;

    public override void Enter()
    {
        base.Enter();
        owner.PlayerController.IsGrounded = true; // ?
    }

    public override void HandleUpdate()
    {
        if (Input.GetButtonDown("Fire2") && PlayerHoldingState.CanGrabObject(player.GetComponent<Controller>()))
            stateMachine.Transition<PlayerHoldingState>();

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded)
            stateMachine.Transition<PlayerJumpState>();

        if (player.InputVector.magnitude > 0 || player.Velocity.magnitude > 0.1)
            stateMachine.Transition<PlayerMovingState>();
    }

    public static bool IsPlayerIdle(Controller controller)
    {
        return (controller.InputVector.magnitude == 0.0f && controller.Velocity.magnitude <= playerVelocityIdleTolerance);
    }
}
