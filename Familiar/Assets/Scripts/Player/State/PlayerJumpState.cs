using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerJumpState")]
public class PlayerJumpState : PlayerBaseState
{
    //private float gravity = 30.0f;
    //private bool hasDoubleJumped;
    private float previousPlayerKineticFriction;

    public override void Enter()
    {
        Debug.Log("Entered Jump State");
        base.Enter();
        //player.gravity = gravity;
        previousPlayerKineticFriction = player.kineticFrictionCoefficient;
        player.kineticFrictionCoefficient = 0.0f;
        Jump();
    }

    public override void HandleUpdate()
    {
        if (owner.ded)
            stateMachine.Transition<PlayerDeathState>();
        if (!Input.GetKey(KeyCode.Space))
            player.isJumping = false;

        //adjusts jump velocity
        //Debug.Log("player jumping");
        //if (!player.IsGrounded && !hasDoubleJumped && Input.GetKeyDown(KeyCode.Space))
        //{
        //    player.Jump();
        //    hasDoubleJumped = true;
        //    return;
        //}

        //if (player.IsGrounded)
        //{
        //    hasDoubleJumped = false;
        //}
        if (player.IsGrounded && player.input.magnitude == 0 && player.velocity.magnitude < 0.1) 
            stateMachine.Transition<PlayerIdleState>();

        if (player.IsGrounded && !player.isJumping)
            stateMachine.Transition<PlayerMovingState>();

        //falling state?
    }

    private void Jump()
    {
        player.isJumping = true;
        player.Jump();
    }

    public override void Exit()
    {
        player.kineticFrictionCoefficient = previousPlayerKineticFriction;
        player.isJumping = false;
        //hasDoubleJumped = false;
    }
}
