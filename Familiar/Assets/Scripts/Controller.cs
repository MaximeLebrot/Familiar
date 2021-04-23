using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private const float collisionEpsilon = 0.001f;
    public int health = 10;
    [SerializeField, Range(0f, 100f)]
    public float acceleration = 20.0f;
    public float deceleration = 40.0f;
    public float turnSpeedModifier = 10.0f;
    public float maxSpeed = 7.0f;
    public float jumpHeight = 10.0f;
    public bool grounded;

    [Range(0.0f, 1.0f)]
    public float staticFrictionCoefficient = 0.65f;
    [Range(0.0f, 1.0f)]
    public float kineticFrictionCoefficient = 0.4f;
    [Range(0.0f, 1.0f)]
    public float airResistance = 0.2f;

    public float skinWidth = 0.01f;
    public float gravity = 20.0f;
    public float collisionMargin;
    public float slopeAngleFactor;
    private const float groundCheckDistance = 0.1f;

    public LayerMask collisionMask;
    public Vector3 velocity;
    public Vector3 input;
    public RaycastHit hit;

    public CameraHandler cam;
    public CapsuleCollider col;

    //public Vector3 Gravity
    //{
    //    get
    //    {
    //        return gravity;
    //    }
    //}

    public float GroundCheckMargin
    {
        get
        {
            return skinWidth + groundCheckDistance;
        }
    }

    void Awake()
    {
        col = GetComponent<CapsuleCollider>();
        cam = GetComponentInChildren<CameraHandler>();
    }

    void Update()
    {
        hit = GroundCheck();
        input = GetMovementInput(hit);

        velocity += input * acceleration * Time.deltaTime;
        velocity += Vector3.down * gravity * Time.deltaTime;

        velocity *= Mathf.Pow(airResistance, Time.deltaTime);

        UpdateVelocity();

        transform.position += velocity * Time.deltaTime;
        transform.forward = new Vector3(cam.transform.forward.x, 0.0f, cam.transform.forward.z);
    }

    private RaycastHit GroundCheck()
    {
        //RaycastHit hit;
        grounded = Physics.CapsuleCast(
            GetPoint1(),
            GetPoint2(),
            col.radius,
            Vector3.down,   
            out hit,
            groundCheckDistance + collisionMargin,
            collisionMask
        );
        return hit;
    }

    public bool IsGrounded
    {
        get
        {
            return (
                Physics.CapsuleCast(
                    point1: GetPoint1(),
                    point2: GetPoint2(),
                    radius: col.radius,
                    direction: Vector3.down,
                    maxDistance: GroundCheckMargin,
                    layerMask: collisionMask
                    )
                );
        }
    }

    private Vector3 SurfaceProjection(Vector3 movement)
    {
        Vector3 dir = movement.normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, 3.0f, collisionMask))
        {
            Debug.DrawRay(hit.point, hit.normal, Color.green);
            Vector3 projected = Vector3.ProjectOnPlane(velocity, hit.normal);
            Debug.DrawRay(hit.point, projected, Color.blue);
            return projected;
        }
        else
            return Vector3.zero;
    }

    void UpdateVelocity()
    {
        //CastFunction();
        //if (CastFunction != r�cker till)
        OverlapFunction();
    }

    void OverlapFunction()
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

    void CastFunction()
    {
        RaycastHit hitInfo;
        bool hit = Physics.CapsuleCast(
            point1: GetPoint1(),
            point2: GetPoint2(),
            radius: col.radius,
            direction: velocity.normalized,
            hitInfo: out hitInfo,
            maxDistance: velocity.magnitude * Time.deltaTime + skinWidth,
            layerMask: collisionMask
            );

        if (velocity.magnitude < collisionEpsilon)
        {
            transform.position += Vector3.zero;
        }

        if (hit)
        {
            CalculateVelocity(hitInfo.normal);
            UpdateVelocity();
        }
        else
        {
            //velocity += input * acceleration * Time.deltaTime;
            //velocity *= Mathf.Pow(airResistance, Time.deltaTime);
        }
    }

    void CalculateVelocity(Vector3 normal)
    {
        Vector3 normalForce = NormalForce(velocity, normal/*.normalized*/);
        velocity += normalForce;
        Friction(normalForce);
    }

    Vector3 NormalForce(Vector3 velocity, Vector3 normal)
    {
        Vector3 projection;
        float dot = Vector3.Dot(velocity, normal);
        if (dot > 0)
        {
            projection = Vector3.zero;
            //velocity = (velocity - normal * dot).normalized * velocity.magnitude;
        }
        else
            projection = Vector3.Dot(velocity, normal) * normal;
        return -projection;
    }

    void Friction(Vector3 normalForce)
    {
        if (velocity.magnitude < normalForce.magnitude * staticFrictionCoefficient)
            velocity = Vector3.zero;
        else
            velocity -= velocity.normalized * normalForce.magnitude * kineticFrictionCoefficient;
    }

    float GetDistanceToPoints()
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

    Vector3 GetMovementInput(RaycastHit hit)
    {
        input = Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.forward * Input.GetAxisRaw("Vertical");
        
        if (input.magnitude > 1.0f)
            input.Normalize();

        float inputMagnitude = input.magnitude;

        Vector3 normal = grounded ? hit.normal : Vector3.up;
        input = Vector3.ProjectOnPlane(cam.transform.rotation * input, 
            Vector3.Lerp(Vector3.up, normal, slopeAngleFactor)).normalized * inputMagnitude;

        //input = cam.transform.rotation * input;
        return input;
    }

    public void Jump()
    {
        velocity += new Vector3(0.0f, jumpHeight, 0.0f);
        //velocity += Vector3.up * jumpHeight;
        //grounded = false;
    }
}