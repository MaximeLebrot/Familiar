using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerMovingState")]
public class PlayerMovingState : PlayerBaseState
{
    //private float gravity = 20.0f;
    private bool hasDoubleJumped = false;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Moving State");
        //player.gravity = gravity;
    }

    public override void HandleUpdate()
    {
        if (!player.IsGrounded && !hasDoubleJumped && Input.GetKeyDown(KeyCode.Space))
        {
            player.Jump();
            hasDoubleJumped = true;
            return;
        }

        if (player.IsGrounded)
        {
            hasDoubleJumped = false;
        }

        //Debug.Log("player moving");
        if (player.input.magnitude == 0 && player.velocity.magnitude < 0.1)
            stateMachine.Transition<PlayerIdleState>();
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded)
            stateMachine.Transition<PlayerJumpState>();
        if (Input.GetKeyDown(KeyCode.E))
            stateMachine.Transition<PlayerHoldingState>();
    }

    public override void Exit()
    {
        hasDoubleJumped = false;
    }

    private void Moving()
    {
        
    }
}
