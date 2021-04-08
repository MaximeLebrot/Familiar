using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerHoldingState")]
public class PlayerHoldingState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Holding State");
    }

    public override void HandleUpdate()
    {
        //Debug.Log("player holding");
        Hold();
    }

    private void Hold()
    {

    }
}
