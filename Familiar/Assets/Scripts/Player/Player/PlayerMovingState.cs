using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerMovingState")]
public class PlayerMovingState : PlayerBaseState
{
    //private float gravity = 20.0f;
    //private bool hasDoubleJumped = false;

    public override void Enter()
    {
        base.Enter();
        owner.anim.SetBool("isWalking", true);
        //Debug.Log("Entered Moving State");
        //player.gravity = gravity;
    }

    public override void HandleUpdate()
    {
        //Debug.Log("player moving");
        if (owner.ded)
            stateMachine.Transition<PlayerDeathState>();
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded)
            stateMachine.Transition<PlayerJumpState>();

        if (player.input.magnitude == 0 && player.velocity.magnitude < 0.1)
            stateMachine.Transition<PlayerIdleState>();

        

        //if (Input.GetButtonDown("Fire2"))
           // stateMachine.Transition<PlayerHoldingState>();
    }

    public override void Exit()
    {

    }

    private void Moving()
    {
        
    }
}
