using AbilitySystem;
using UnityEngine;

public class Controller : MonoBehaviour
{ 
    [Header("Movement")]
    [SerializeField, Range(0f, 100f), Tooltip("Controls the acceleration")]
    private float acceleration;
    [SerializeField, Tooltip("Controls the maximum speed attainable")]
    private float maxSpeed;
    [Tooltip("Checks whether the player is jumping")]
    private bool isJumping;
    [Tooltip("Checks whether the player is grounded")]
    private bool isGrounded;
    [Tooltip("The transform component attached to the game object. Should be inputed manually")]
    private new Transform transform;
    [Tooltip("The string value of \"Horizontal\"")]
    private static string horizontal = "Horizontal";
    [Tooltip("The string value of \"Vertical\"")]
    private static string vertical = "Vertical";
    [Tooltip("The collision epsilon")]
    private const float collisionEpsilon = 0.001f;

    [Header("Jump settings")]
    [SerializeField, Tooltip("The gravity affecting the player. Calculated by: Vector3.down * gravity * Time.fixedDeltaTime")]
    private float gravity;
    [SerializeField, Tooltip("The jump height of the player. Added to the y-axis of the jump vector")]
    private float jumpHeight;
    [SerializeField, Tooltip("The amount of extra gravity applied while doing a short jump. Keep in mind that gravity is already being applied once.")]
    private float lowJumpCoefficient;
    [SerializeField, Tooltip("The amount of extra gravity applied while falling. Keep in mind that gravity is already being applied once.")]
    private float fastFallCoefficient;
    [Tooltip("The jump vector added to the velocity")]
    private Vector3 jumpVector;

    [Header("Physics")]
    [SerializeField, Range(0.0f, 1.0f), Tooltip("The static friction coefficient. Should never be below the kinetic one")]
    private float staticFrictionCoefficient;
    [SerializeField, Range(0.0f, 1.0f), Tooltip("The kinetic friction coefficient. Should never be below the static one")]
    private float kineticFrictionCoefficient;
    [SerializeField, Range(0.0f, 1.0f), Tooltip("The air resistance, the lower the resistance, the slower the player moves")]
    private float airResistance;
    [SerializeField, Range(0.0f, 0.1f), Tooltip("The thickness of the skin of the player")]
    private float skinWidth;
    [SerializeField, Range(0.0f, 1.0f), Tooltip("Project on plane uses a lerp between 0 = Vector3.up and 1 = hit.normal and uses the slope angle factor as the t value")]
    private float slopeAngleFactor;
    [SerializeField, Range(0.0f, 1.0f), Tooltip("The distance checked below the player for the ground check")]
    private float groundCheckDistance = 0.1f;
    [Tooltip("¨The magnitude of the input vector")]
    private float inputMagnitude;

    [Header("Collision")]
    [SerializeField, Tooltip("The layer mask used for the controller to recognize collision")]
    private LayerMask collisionMask;
    [Tooltip("The velocity of the player")]
    private Vector3 velocity;
    [Tooltip("The input of the player")]
    private Vector3 input;

    [Header("References")]
    [SerializeField, Tooltip("The child camera component of the player. Should be inputed manually")]
    private CameraHandler cam;
    [SerializeField, Tooltip("The collider component of the player. Should be inputed manually")]
    private CapsuleCollider col;
    [Tooltip("This bool controls whether the players transform updates")]
    private bool stopController;

    [Tooltip("The RaycastHit component set by the ground check")]
    private RaycastHit hit;

    void Awake()
    {
        InitializeSequence();
    }

    void Update()
    {
        hit = GroundCheck();
        input = GetMovementInput(hit);
    }

    private void FixedUpdate()
    {
        if (HorizontalVelocity.magnitude > maxSpeed)
        {
            Vector3 clampedVelocity = HorizontalVelocity.normalized * maxSpeed;
            velocity.x = clampedVelocity.x;
            velocity.z = clampedVelocity.z;
        }

        velocity += input * acceleration * Time.fixedDeltaTime;
        velocity += Vector3.down * gravity * Time.fixedDeltaTime;

        velocity *= Mathf.Pow(airResistance, Time.fixedDeltaTime);

        if (velocity.y < 0)
            velocity += Vector3.down * gravity * fastFallCoefficient * Time.fixedDeltaTime;

        if (velocity.y > 0.0f && !Input.GetKey(KeyCode.Space))
            velocity += Vector3.down * gravity * lowJumpCoefficient * Time.fixedDeltaTime;

        UpdateVelocity();

        if (!stopController)
            transform.position += velocity * Time.fixedDeltaTime;
    }

    private void LateUpdate()
    {
        transform.forward = new Vector3(cam.transform.forward.x, 0.0f, cam.transform.forward.z);
    }

    private RaycastHit GroundCheck()
    {
        isGrounded = Physics.CapsuleCast(
            GetPoint1(),
            GetPoint2(),
            col.radius,
            Vector3.down,
            out hit,
            groundCheckDistance,
            collisionMask
        );
        return hit;
    }

    private Vector3 GetMovementInput(RaycastHit hit)
    {
        input = Vector3.right * Input.GetAxisRaw(horizontal) + Vector3.forward * Input.GetAxisRaw(vertical);

        if (input.magnitude > 1.0f)
            input.Normalize();

        inputMagnitude = input.magnitude;

        Vector3 normal = isGrounded ? hit.normal : Vector3.up;

        input = Vector3.ProjectOnPlane(
            cam.transform.rotation * input,
            Vector3.Lerp(Vector3.up, normal, slopeAngleFactor)
            ).normalized * inputMagnitude;

        return input;
    }

    private void UpdateVelocity()
    {
        CastFunction();
        //if (CastFunction != räcker till)
        //OverlapFunction();
    }

    private void OverlapFunction()
    {
        Collider[] colliders = Physics.OverlapCapsule(GetPoint1(), GetPoint2(), col.radius, collisionMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Physics.ComputePenetration(
                colliderA: col,
                positionA: transform.position,
                rotationA: transform.rotation,
                colliderB: colliders[i],
                positionB: colliders[i].transform.position,
                rotationB: colliders[i].transform.rotation,
                direction: out Vector3 direction,
                distance: out float distance);
            //velocity += SurfaceProjection(velocity);
            CalculateVelocity(direction);
        }
    }

    private void CastFunction()
    {
        RaycastHit hitInfo;
        bool hit;

        hit = Physics.CapsuleCast(
            point1: GetPoint1(),
            point2: GetPoint2(),
            radius: col.radius + skinWidth,
            direction: velocity.normalized,
            hitInfo: out hitInfo,
            maxDistance: velocity.magnitude * Time.fixedDeltaTime,
            layerMask: collisionMask
            );

        if (velocity.magnitude < collisionEpsilon)
        {
            transform.position += Vector3.zero;
        }

        if (hit)
        {
            CalculateVelocity(hitInfo.normal.normalized);
            UpdateVelocity();
        }
        else
        {
            return;
            //velocity += input * acceleration * Time.deltaTime;
            //velocity *= Mathf.Pow(airResistance, Time.deltaTime);
        }
    }

    private void CalculateVelocity(Vector3 normal)
    {
        Vector3 normalForce = CalculateNormalForce(velocity, normal);
        velocity += normalForce;

        CalculateFriction(normalForce);
    }

    private Vector3 CalculateNormalForce(Vector3 velocity, Vector3 normal)
    {
        Vector3 projection;
        float dot = Vector3.Dot(velocity, normal);

        if (dot >= 0)
        {
            projection = Vector3.zero;
        }
        else
        {
            projection = Vector3.Dot(velocity, normal) * normal;
        }

        return -projection;
    }

    private void CalculateFriction(Vector3 normalForce)
    {
        if (velocity.magnitude < normalForce.magnitude * AdjustedStaticFrictionCoefficient)
            velocity = Vector3.zero;
        else
            velocity += (-1.0f) * velocity.normalized * normalForce.magnitude * kineticFrictionCoefficient;
    }

    private float GetDistanceToPoints()
    {
        return col.height * 0.5f - col.radius;
    }

    public Vector3 GetPoint1()
    {
        return col.center + Vector3.up * GetDistanceToPoints() + transform.position;
    }

    public Vector3 GetPoint2()
    {
        return col.center + Vector3.down * GetDistanceToPoints() + transform.position;
    }

    public void Jump()
    {
        velocity += jumpVector;
    }

    private void InitializeSequence()
    {
        InitializeJumpVector();
        InitializeCollider();
        InitializeCamera();
        InitilizeTransform();
    }
    private void InitializeJumpVector()
    {
        jumpVector = new Vector3(0.0f, jumpHeight, 0.0f);
    }
    private void InitializeCollider()
    {
        if (col == null)
            col = GetComponent<CapsuleCollider>();
    }
    private void InitializeCamera()
    {
        if (cam == null)
            cam = GetComponentInChildren<CameraHandler>();
    }
    private void InitilizeTransform()
    {
        if (transform == null)
            transform = GetComponent<Transform>();
    }

    public bool IsGrounded
    {
        get => isGrounded;
        set => isGrounded = value;
    }
    public bool IsJumping
    {
        get => isJumping;
        set => isJumping = value;
    }
    public Transform Transform
    {
        get => transform;
    }
    public Vector3 Velocity
    {
        get => velocity;
    }
    public Vector3 InputVector
    {
        get => input;
    }
    public CameraHandler Camera
    {
        get
        {
            return cam;
        }
    }
    public Vector3 HorizontalVelocity
    {
        get
        {
            return new Vector3(velocity.x, 0.0f, velocity.z);
        }
    }
    public LayerMask CollisionMask
    {
        get => collisionMask;
    }
    public bool StopController
    {
        get => stopController;
        set => stopController = value;
    }
    public Vector3 HorizontalInput
    {
        get
        {
            return new Vector3(input.x, 0.0f, input.z);
        }
    }
    public float GroundCheckMargin
    {
        get
        {
            return skinWidth + groundCheckDistance;
        }
    }
    public float AdjustedStaticFrictionCoefficient
    {
        get
        {
            if (GetComponent<Player>().CurrentState is PlayerJumpState)
                return 0.0f;
            else
                return staticFrictionCoefficient;
        }
    }
    //public bool IsGrounded
    //{
    //    get
    //    {
    //        return Physics.CapsuleCast(
    //                point1: GetPoint1(),
    //                point2: GetPoint2(),
    //                radius: col.radius,
    //                direction: Vector3.down,
    //                maxDistance: GroundCheckMargin,
    //                layerMask: collisionMask
    //                );
    //    }
    //}
    public float LowJumpCoefficient
    {
        get
        {
            return lowJumpCoefficient;
        }
    }
    public float Gravity
    {
        get
        {
            return gravity;
        }
    }
}