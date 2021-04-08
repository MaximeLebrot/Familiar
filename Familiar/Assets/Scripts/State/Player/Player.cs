using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Controller playerController;
    public State[] states;

    private StateMachine stateMachine;

    private Vector3 crosshairOffset = new Vector3(0,2,0);

    protected void Awake()
    {
        Debug.Log("Player Awake");
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
            Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
            //Ray ray = new Ray(transform.position, getCrosshairFromCamera());
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan, 2f);
        }
    }
    private Vector3 getCrosshairFromCamera()
    {
        return -playerController.cam.offset + playerController.transform.position + crosshairOffset;
    } 
}
