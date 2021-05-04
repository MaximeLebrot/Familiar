using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerMovingState")]
public class PlayerMovingState : PlayerBaseState
{
    //private float gravity = 20.0f;
    //private bool hasDoubleJumped = false;

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Entered Moving State");
        //player.gravity = gravity;
    }

    public override void HandleUpdate()
    {
        //Debug.Log("player moving");
        if (Input.GetButtonDown("Fire2") && PlayerHoldingState.CanGrabObject(player.GetComponent<Controller>()))
            stateMachine.Transition<PlayerHoldingState>();

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded)
            stateMachine.Transition<PlayerJumpState>();

        if (PlayerIdleState.IsPlayerIdle(player))
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

    public static bool IsPlayerMoving(Controller controller)
    {
        return !PlayerIdleState.IsPlayerIdle(controller); //might want to change this if we change what "idle" and "moving" mean
    }
}
