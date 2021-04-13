using UnityEngine;

public class Player : MonoBehaviour
{
    public Controller playerController;
    public State[] states;

    private StateMachine stateMachine;

    private Vector3 crosshairOffset = new Vector3(0,2,0);

    protected void Awake()
    {
        //Debug.Log("Player Awake");
        playerController = GetComponent<Controller>();
        stateMachine = new StateMachine(this, states);
    }

    private void Update()
    {
        stateMachine.HandleUpdate();
        Crosshair();
    }
    void Crosshair()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Ray playerRay = new Ray(transform.position, transform.forward);

            //Ray camRay = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
            //Debug.DrawRay(camRay.origin, camRay.direction*100, Color.magenta, 1f);

            //Debug.DrawRay(playerRay.origin, camRay.direction * 100, Color.yellow, 1f);
            //Ray ray = new Ray(transform.position, getCrosshairFromCamera());
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan, 2f);
            //Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.height / 2, Screen.width / 2));
            //RaycastHit hitPoint;

            //if (Physics.Raycast(camRay, out hitPoint, 100.0f/*LayerMask*/))
            //{


            //    Ray playerRay = new Ray(transform.position, hitPoint.point);
            //    Debug.DrawRay(playerRay.origin, playerRay.direction * 100, Color.green, 1f);

                //RaycastHit hit;
                //if (Physics.Raycast(playerRay, out hit, 100.0f/*LayerMask*/))
                //{
                //    if (hit.point == hitPoint.point)
                //    {
                //        Debug.Log("Spelaren har lign of sight");
                //    }
                //}
            //}

            //RaycastHit hit;
            //if (Physics.Raycast(Camera.main.transform.position, Vector3.one * 0.5f, out hit, 50.0f, playerController.collisionMask))
            //{
            //    Debug.Log("hit");
            //    Debug.Log(hit.distance);
            //}
            //Debug.DrawRay(Camera.main.transform.position, getMiddleOfScreen(), Color.cyan, 1f);
        }
    }
    private Vector3 getMiddleOfScreen()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
    }
    private Vector3 getCrosshairFromCamera()
    {
        return -playerController.cam.offset + playerController.transform.position + crosshairOffset;
    } 
}
