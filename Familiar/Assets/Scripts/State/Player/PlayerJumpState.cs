using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerJumpState")]
public class PlayerJumpState : PlayerBaseState
{
    //private float gravity = 30.0f;
    private bool hasDoubleJumped;

    public override void Enter()
    {
        Debug.Log("Entered Jump State");
        base.Enter();
        //player.gravity = gravity;
        Jump();
    }

    public override void HandleUpdate()
    {
        //Debug.Log("player jumping");
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
        if (player.IsGrounded && !player.jumping)
            stateMachine.Transition<PlayerMovingState>();
        if (player.IsGrounded && player.input.magnitude == 0 && player.velocity.magnitude < 0.1) 
            stateMachine.Transition<PlayerIdleState>();
        //falling state?
    }

    private void Jump()
    {
        player.jumping = true;
        player.Jump();
    }
    public override void Exit()
    {
        hasDoubleJumped = false;
    }
}
