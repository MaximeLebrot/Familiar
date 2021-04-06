using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Vector2 CameraVec;
    public float mouseSensitivity = 2.0f;
    public float maxAngleDown = -60.0f;
    public float maxAngleUp = 60.0f;
    public Controller playerController;
    public Vector3 offset;
    public bool firstPerson;

    private Vector3 cameraOffset = new Vector3(0, 2, -7);
    private void Awake()
    {
        CameraVec = new Vector2(0, 0);
        playerController = GetComponentInParent<Controller>();
    }
    void LateUpdate()
    {
        CameraVec.x -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        CameraVec.y += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        CameraVec.x = Mathf.Clamp(CameraVec.x, maxAngleDown, maxAngleUp);
        transform.rotation = Quaternion.Euler(CameraVec.x, CameraVec.y, 0);
        
        offset = transform.rotation * cameraOffset;
        offset = CheckCollision() + playerController.transform.position;

        if (firstPerson)
        {
            offset -= transform.rotation * cameraOffset;
        }

        transform.position = offset;
    }
    Vector3 CheckCollision()
    {
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(playerController.transform.position, offset.normalized, out hitInfo, offset.magnitude, playerController.collisionMask);
        if (hit)
        {
            Debug.DrawLine(playerController.transform.position, offset.normalized * hitInfo.distance, Color.blue);
            return offset.normalized * hitInfo.distance;
        }
        else
            return offset;
    }
}
