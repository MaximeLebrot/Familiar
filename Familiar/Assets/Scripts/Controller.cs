using UnityEngine;

public class Controller : MonoBehaviour
{
    public int health = 10;
    [SerializeField, Range(0f, 100f)]
    public float acceleration = 20.0f;
    public float deceleration = 40.0f;
    public float turnSpeedModifier = 10.0f;
    public float maxSpeed = 7.0f;
    public float jumpHeight = 10.0f;

    public float staticFrictionCoefficient = 0.65f;
    public float kineticFrictionCoefficient = 0.4f;
    public float airResistance = 0.2f;

    public float skinWidth = 0.01f;
    public float gravity = 20.0f;
    public float groundCheckDistance = 0.1f;
    public bool grounded;

    public LayerMask collisionMask;
    public CameraHandler cam;
    public Vector3 velocity;
    public Vector3 input;

    public CapsuleCollider col;

    void Awake()
    {
        col = GetComponent<CapsuleCollider>();
        cam = GetComponentInChildren<CameraHandler>();
        //cam = Camera.main;
    }

    void Update()
    {
        input = GetMovementInput();
        if (input.magnitude > 1)
            velocity += input.normalized * acceleration * Time.deltaTime;
        else
            velocity += input * acceleration * Time.deltaTime;

        velocity += Vector3.down * gravity * Time.deltaTime;

        GroundCheck();

        UpdateVelocity(); 
        //Debug.DrawLine(transform.position, transform.position + input, Color.black);
        //Debug.DrawLine(transform.position, transform.position + velocity, Color.green); 

        //if (Input.GetKeyDown(KeyCode.Space) && grounded)
        //{
        //    Jump();
        //}

        transform.position += velocity * Time.deltaTime;
        transform.forward = new Vector3(cam.transform.forward.x, 0.0f, cam.transform.forward.z);
        //transform.rotation = Quaternion.Euler(0, camera.transform.rotation.y, 0); // rotera spelaren enligt cameran
    }
    void GroundCheck()
    {
        RaycastHit hit;
        if (Physics.CapsuleCast(GetPoint1(), GetPoint2(), col.radius, Vector3.down, out hit, /*velocity.magnitude * Time.deltaTime + */groundCheckDistance - skinWidth, collisionMask))
        {
            grounded = true;
            //Debug.Log(hit.distance);
            //velocity += Vector3.ProjectOnPlane(transform.position, hit.normal) + Vector3.Dot(transform.position, hit.normal) * hit.normal;
            //velocity += velocity.magnitude * Vector3.ProjectOnPlane(velocity, hit.normal).normalized;
            //Vector3 planeProjection = Vector3.ProjectOnPlane(velocity, hit.normal);
            //velocity += Vector3.ProjectOnPlane(velocity, hit.normal);
            //velocity = Vector3.ProjectOnPlane(velocity, hit.normal);
            //Debug.DrawLine(transform.position, planeProjection, Color.yellow);
            //CalculateVelocity(hitInfo.normal);
            //float Angle = 90 - Mathf.Abs(Vector3.Angle(transform.position, hit.normal));
            //velocity *= 0.5f + (Angle / 90f) / 2;
            //float planeAngle = Vector3.Angle(transform.forward, hit.normal);
            //if (planeAngle > 110)
            //    velocity = Vector3.ProjectOnPlane(velocity, hit.normal);
        }
        else
            grounded = false;
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
        CastFunction();
        //if (CastFunction != räcker till)
        //OverlapFunction();
    }
    void OverlapFunction()
    {
        Collider[] colliders = Physics.OverlapCapsule(GetPoint1(), GetPoint2(), col.radius, collisionMask);
        Vector3 direction;
        float distance;

        for (int i = 0; i < colliders.Length; i++)
        {
            Physics.ComputePenetration(
                col, transform.position, transform.rotation,
                colliders[i], colliders[i].gameObject.transform.position,
                colliders[i].gameObject.transform.rotation,
                out direction, out distance);
            //velocity += SurfaceProjection(velocity);
            CalculateVelocity(direction);
        }
    }
    void CastFunction()
    {
        RaycastHit hitInfo;
        bool hit = Physics.CapsuleCast(GetPoint1(), GetPoint2(), col.radius, velocity.normalized, out hitInfo, velocity.magnitude * Time.deltaTime + skinWidth, collisionMask);

        if (velocity.magnitude < 0.001f)
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
        velocity *= Mathf.Pow(airResistance, Time.deltaTime);
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
        return col.height / 2 - col.radius;
    }
    public Vector3 GetPoint1()
    {
        return col.center + Vector3.up * GetDistanceToPoints() + transform.position;
    }
    public Vector3 GetPoint2()
    {
        return col.center + Vector3.down * GetDistanceToPoints() + transform.position;
    }
    Vector3 GetMovementInput()
    {
        input = Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.forward * Input.GetAxisRaw("Vertical");
        input = cam.transform.rotation * input;
        return input;
    }
    public void Jump()
    {
        velocity += new Vector3(0, jumpHeight, 0);
        grounded = false;
    }
}