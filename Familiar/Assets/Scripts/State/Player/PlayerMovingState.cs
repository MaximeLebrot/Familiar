using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerMovingState")]
public class PlayerMovingState : PlayerBaseState
{
    private float gravity = 20.0f;
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Moving State");
        player.gravity = gravity;
    }

    public override void HandleUpdate()
    {
        //Debug.Log("player moving");
        if (player.input.magnitude == 0 && player.velocity.magnitude < 0.1)
            stateMachine.Transition<PlayerIdleState>();
        if (Input.GetKeyDown(KeyCode.Space) && player.grounded)
            stateMachine.Transition<PlayerJumpState>();
        if (Input.GetKeyDown(KeyCode.E))
            stateMachine.Transition<PlayerHoldingState>();
    }

    private void Moving()
    {
        
    }
}
