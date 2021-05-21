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

    [Header("References")]
    [SerializeField, Tooltip("A reference to the \"Controller\" scripts attached to the player game object. Should be inputed manually")]
    private Controller playerController;

    [Tooltip("Controls whether the game is player in first person or not")]
    private bool firstPerson;
    [Tooltip("Controls whether the the camera is frozen or not")]
    private bool freezeCamera;
    [Tooltip("Controls whether the player can move the camera")]
    private bool cannotMoveCam;

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
        //Debug.Log("player awake");
        InitializeSequence();
    }

    void LateUpdate()
    {
        if (freezeCamera != true && cannotMoveCam != true)
        {
            HandleMouseInput();
        }
        HandleCamera();
    }

    private void HandleCamera()
    {
        CameraVec.x = Mathf.Clamp(CameraVec.x, maxAngleDown, maxAngleUp);
        transform.rotation = Quaternion.Euler(CameraVec.x, CameraVec.y, 0);

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

    private void InitializeSequence()
    {
        InitializePlayerController();
        InitializeMouseSensitivity();
        InitializeCameraVector();
        InitializeCursor();
    }

    private void InitializePlayerController()
    {
        if (playerController == null)
            playerController = GetComponentInParent<Controller>();
    }

    private void InitializeMouseSensitivity()
    {
        if (mouseSensitivity == 0)
            mouseSensitivity = Stats.Instance.MouseSensitivity;
    }

    private void InitializeCameraVector()
    {
        CameraVec = new Vector2(0, 0);
    }

    private void InitializeCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public bool FreezeCamera
    {
        get => freezeCamera;
        set => freezeCamera = value;
    }

    public bool CannotMoveCam
    {
        get => cannotMoveCam;
        set => cannotMoveCam = value;
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
