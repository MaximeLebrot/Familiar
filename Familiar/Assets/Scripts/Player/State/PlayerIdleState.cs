using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerIdleState")]
public class PlayerIdleState : PlayerBaseState
{
    private static readonly float playerVelocityIdleTolerance = 0.1f;

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Entered Idle State");
        player.isGrounded = true;
    }

    public override void HandleUpdate()
    {
        if (Input.GetButtonDown("Fire2") && PlayerHoldingState.CanGrabObject(player.GetComponent<Controller>()))
            stateMachine.Transition<PlayerHoldingState>();

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded)
            stateMachine.Transition<PlayerJumpState>();

        if (player.input.magnitude > 0 || player.velocity.magnitude > 0.1)
            stateMachine.Transition<PlayerMovingState>();
    }

    private void Idle()
    {

    }

    public static bool IsPlayerIdle(Controller controller)
    {
        return (controller.input.magnitude == 0.0f && controller.velocity.magnitude <= playerVelocityIdleTolerance);
    }
}
