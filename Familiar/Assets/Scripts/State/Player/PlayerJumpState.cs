using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerJumpState")]
public class PlayerJumpState : PlayerBaseState
{
    private float gravity = 30.0f;
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
        if (player.grounded && Physics.Raycast(owner.transform.position, Vector3.down, player.skinWidth + player.groundCheckDistance))
            stateMachine.Transition<PlayerMovingState>();
        if (player.grounded && player.input == Vector3.zero) 
            stateMachine.Transition<PlayerIdleState>();
        //falling state?
    }

    private void Jump()
    {
        player.Jump();
        player.grounded = false;
    }
}
