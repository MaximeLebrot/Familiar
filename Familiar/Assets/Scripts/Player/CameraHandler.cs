using System.Collections;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField, Tooltip("The mouse sensitivity of the player")]
    private float mouseSensitivity;
    [SerializeField, Tooltip("The maximum angle the player can tilt the camera down")]
    private float maxAngleDown = -60.0f;
    [SerializeField, Tooltip("The maximum angle the player can tilt the camera up")]
    private float maxAngleUp = 60.0f;
    [SerializeField, Tooltip("The adjusted offset of the camera when colliding")]
    private float cameraAdjustmentOffset;
    [SerializeField, Tooltip("The starting offset of the camera")]
    private Vector3 cameraOffset;
    [SerializeField, Tooltip("!!Set this to be equal to the y-value of the parent's rotation!!")] private float startingYRotation;

    [Header("References")]
    [SerializeField, Tooltip("A reference to the \"Controller\" scripts attached to the player game object. Should be inputed manually")]
    private Controller playerController;
    private new Transform transform;

    [Tooltip("Controls whether the game is player in first person or not")]
    private bool firstPerson;
    [Tooltip("Controls whether the the camera is frozen or not")]
    private bool freezeCamera;
    [Tooltip("Controls whether the player can move the camera")]
    private bool freezeCam = true;

    [Tooltip("The position of the camera in the world")]
    private Vector3 pos;
    [Tooltip("The camera vector2 which gets the mouse input from the player")]
    private Vector2 CameraVec;
    [Tooltip("The RaycastHit struct carrying information about the collision of the camera")]
    private RaycastHit hitInfo;

    private static readonly string MouseY = "Mouse Y";
    private static readonly string MouseX = "Mouse X";

    private void Awake()
    {
        StartCoroutine(StopInputAtStart());
        InitializeSequence();
    }

    void LateUpdate()
    {
        if (freezeCamera != true && freezeCam != true)
        {
            HandleMouseInput();
        }
        HandleCamera();
    }

    IEnumerator StopInputAtStart()
    {
        yield return new WaitForSeconds(1.0f);
        freezeCam = false;
    }

    private void HandleCamera()
    {
        CameraVec.x = Mathf.Clamp(CameraVec.x, maxAngleDown, maxAngleUp);
        transform.rotation = Quaternion.Euler(CameraVec.x, CameraVec.y, 0.0f);

        pos = transform.rotation * cameraOffset;
        pos = CheckCollision() + playerController.transform.position;

        if (firstPerson == true)
            pos -= transform.rotation * cameraOffset;

        transform.position = pos;
    }

    private void HandleMouseInput()
    {
        CameraVec.x -= Input.GetAxisRaw(MouseY) * mouseSensitivity;
        CameraVec.y += Input.GetAxisRaw(MouseX) * mouseSensitivity;
    }

    private Vector3 CheckCollision()
    {
        bool hit = Physics.Raycast(playerController.transform.position, pos.normalized, out hitInfo, pos.magnitude, playerController.CollisionMask);

        if (hit)
            return pos.normalized * (hitInfo.distance - cameraAdjustmentOffset);
        else
            return pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(new Ray(playerController.transform.position, transform.forward));
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(playerController.transform.position, -(transform.position - playerController.transform.position)));
    }

    private void InitializeSequence()
    {
        InitializePlayerController();
        InitializeMouseSensitivity();
        InitializeCameraVector();
        InitializeCursor();
        InitializeTransform();
    }

    private void InitializePlayerController()
    {
        if (playerController == null)
            playerController = GetComponentInParent<Controller>();
    }

    private void InitializeMouseSensitivity()
    {
        if (Stats.Instance != null)
        {
            if (mouseSensitivity == 0)
                mouseSensitivity = Stats.Instance.MouseSensitivity;
        }
    }

    private void InitializeCameraVector()
    {
        CameraVec = new Vector2(playerController.transform.eulerAngles.x, startingYRotation);
    }

    private void InitializeCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void InitializeTransform()
    {
        if (transform == null)
            transform = gameObject.transform;
    }

    public Transform Transform
    {
        get => transform;
    }

    public bool FreezeCamera
    {
        get => freezeCamera;
        set => freezeCamera = value;
    }

    public bool FreezeCam
    {
        get => freezeCam;
        set => freezeCam = value;
    }

    public bool FirstPerson
    {
        get => firstPerson;
        set => firstPerson = value;
    }

    public float MouseSensitivity
    {
        get => mouseSensitivity;
        set => mouseSensitivity = value;
    }
}
