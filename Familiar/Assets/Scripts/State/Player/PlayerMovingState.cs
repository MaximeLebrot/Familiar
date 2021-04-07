using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerMovingState")]
public class PlayerMovingState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Moving State");
    }

    public override void HandleUpdate()
    {
        Debug.Log("player moving");
        if (player.input.magnitude == 0 && player.velocity.magnitude < 0.1)
            stateMachine.Transition<PlayerIdleState>();
        if (Input.GetKeyDown(KeyCode.Space) && player.grounded)
            stateMachine.Transition<PlayerJumpState>();
    }

    private void Moving()
    {
        
    }
}
