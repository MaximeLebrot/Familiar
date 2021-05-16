using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerMovingState")]
public class PlayerMovingState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        owner.Anim.SetBool("isWalking", true);
    }

    public override void HandleUpdate()
    {
        if (Input.GetButtonDown("Fire2") && PlayerHoldingState.CanGrabObject(player.GetComponent<Controller>()))
            stateMachine.Transition<PlayerHoldingState>();

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded)
            stateMachine.Transition<PlayerJumpState>();

        if (PlayerIdleState.IsPlayerIdle(player))
            stateMachine.Transition<PlayerIdleState>();
    }
    public override void Exit()
    {
        owner.Anim.SetBool("isWalking", false);
    }

    public static bool IsPlayerMoving(Controller controller)
    {
        return !PlayerIdleState.IsPlayerIdle(controller); //might want to change this if we change what "idle" and "moving" mean
    }
}
