using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerJumpState")]
public class PlayerJumpState : PlayerBaseState
{
    private float gravity = 30.0f;
    //private bool hasDoubleJumped = false;

    public override void Enter()
    {
        Debug.Log("Entered Jump State");
        base.Enter();
        player.gravity = gravity;
        Jump();
    }

    public override void HandleUpdate()
    {
        //Debug.Log("player jumping");
        Debug.Log(player.grounded);

        if (player.grounded)
            stateMachine.Transition<PlayerMovingState>();
        if (player.grounded && player.input == Vector3.zero) 
            stateMachine.Transition<PlayerIdleState>();
        //falling state?
    }

    private void Jump()
    {
        player.Jump();
    }
}
