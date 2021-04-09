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

        transform.position += (Vector3)(velocity * Time.deltaTime);
        //transform.forward = cam.transform.forward;
        //transform.rotation = Quaternion.Euler(0, camera.transform.rotation.y, 0); // rotera spelaren enligt cameran
    }
    void GroundCheck()
    {
        RaycastHit hit;
        if (Physics.CapsuleCast(GetPoint1(), GetPoint2(), col.radius, Vector3.down, out hit, velocity.magnitude * Time.deltaTime + skinWidth, collisionMask))
        {
            grounded = true;
            //velocity += velocity.magnitude * Vector3.ProjectOnPlane(velocity, hit.normal).normalized;
            velocity += Vector3.ProjectOnPlane(velocity, hit.normal);
            //CalculateVelocity(hitInfo.normal);
        }
        else
        {
            //grounded = false;
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
        //CastFunction(GetPoint1(), GetPoint2());
        //if (CastFunction != räcker till)
        OverlapFunction(GetPoint1(), GetPoint2());
    }
    void OverlapFunction(Vector3 point1, Vector3 point2)
    {
        Collider[] colliders = Physics.OverlapCapsule(point1, point2, col.radius, collisionMask);
        Vector3 direction;
        float distance;

        for (int i = 0; i < colliders.Length; i++)
        {
            Physics.ComputePenetration(
                col, transform.position, transform.rotation,
                colliders[i], colliders[i].gameObject.transform.position, colliders[i].gameObject.transform.rotation,
                out direction, out distance);
            //velocity += SurfaceProjection(velocity);
            CalculateVelocity(direction);
        }
    }
    void CastFunction(Vector3 point1, Vector3 point2)
    {
        RaycastHit hitInfo;
        bool hit = Physics.CapsuleCast(point1, point2, col.radius, velocity.normalized, out hitInfo, velocity.magnitude * Time.deltaTime + skinWidth, collisionMask);

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
    void Friction(Vector3 normalForce)
    {
        if (velocity.magnitude < normalForce.magnitude * staticFrictionCoefficient)
            velocity = Vector3.zero;
        else
            velocity -= velocity.normalized * normalForce.magnitude * kineticFrictionCoefficient;
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
    void CalculateVelocity(Vector3 normal)
    {
        Vector3 normalForce = NormalForce(velocity, normal/*.normalized*/);
        velocity += normalForce;
        Friction(normalForce);
        velocity *= Mathf.Pow(airResistance, Time.deltaTime);
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