using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerHoldingState")]
public class PlayerHoldingState : PlayerBaseState
{
    private GrabObjectScript gos;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered Holding State");

        if (gos == null)
            gos = player.GetComponent<GrabObjectScript>();

        gos.ToggleGrab();
    }

    public override void HandleUpdate()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            gos.ToggleGrab();
        }

        if (gos.CarriedObject == null)
        {
            if (PlayerIdleState.IsPlayerIdle(player))
                stateMachine.Transition<PlayerIdleState>();

            if (PlayerMovingState.IsPlayerMoving(player))
                stateMachine.Transition<PlayerMovingState>();
        }
    }

    public static bool CanGrabObject(Controller controller)
    {
        return CanGrabObject(out _, controller);
    }

    public static bool CanGrabObject(out RaycastHit[] hitArray, Controller controller)
    {
        hitArray = Physics.CapsuleCastAll(
            point1: controller.GetPoint1(),
            point2: controller.GetPoint2(),
            radius: controller.gameObject.GetComponent<CapsuleCollider>().radius,
            direction: controller.transform.forward,
            maxDistance: controller.gameObject.GetComponent<CapsuleCollider>().radius * GrabObjectScript.GrabRange
            );

        foreach (RaycastHit rh in hitArray)
        {
            if (rh.collider.gameObject.CompareTag("Moveable") || rh.collider.gameObject.CompareTag("Key")) //Emils dumma ändringar
                return true;
        }

        return false;
    }
}
