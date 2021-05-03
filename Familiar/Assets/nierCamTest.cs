using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nierCamTest : MonoBehaviour
{
    private CameraHandler cam;
    public Vector3 position;
    public Vector3 rotation;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CameraHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //inte reagera på movement
            Camera.main.transform.position = position;
            Camera.main.transform.rotation = Quaternion.Euler(rotation.x, 0, 0);
            cam.freezeCamera = true;
            cam.isInNierCam = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            cam.freezeCamera = false;
            cam.isInNierCam = false;
        }
    }
}
