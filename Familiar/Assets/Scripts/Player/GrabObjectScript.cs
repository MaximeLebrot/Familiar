using System;
using UnityEngine;

//Author: Simon Canbäck
public class GrabObjectScript : MonoBehaviour
{
    [SerializeField] private static readonly float grabRange = 3.0f;
    [SerializeField] private static readonly float heldObjectDistanceTolerance = 2.0f;

    private Controller controller;
    [SerializeField] private SpringJoint spring;

    [SerializeField] private GameObject carriedObject;
    private Rigidbody carriedRigidbody;
    [SerializeField] private Transform heldObjectPoint;

    public static float GrabRange => grabRange;

    public static float HeldObjectDistanceTolerance => heldObjectDistanceTolerance;

    public GameObject CarriedObject
    {
        get => carriedObject;
        set => carriedObject = value;
    }

    private void Awake()
    {
        if (heldObjectPoint == null)
            heldObjectPoint = GameObject.FindGameObjectWithTag("HOLP").transform;

        controller = GetComponent<Controller>();
    }

    private void FixedUpdate()
    {
        if (carriedObject != null 
            && (carriedObject.transform.position - heldObjectPoint.position).magnitude > heldObjectDistanceTolerance)
            DropObject();
    }

    public void ToggleGrab()
    {
        if (carriedObject == null)
            GrabObject();
        else
            DropObject();
    }

    public void GrabObject(RaycastHit[] hitArray)
    {
        int hitIndex = -1;

        if (hitArray.Length == 0)
            return;

        for (int i = 0; i < hitArray.Length; i++)
        {
            if (hitArray[i].collider.CompareTag("Moveable") || hitArray[i].collider.CompareTag("Key") || hitArray[i].collider.GetComponent<IMoveable>() != null) //Emils dumma ändringar
            {
                hitIndex = i;
                break;
            }
        }

        if (hitIndex < 0)
            return;

        carriedObject = hitArray[hitIndex].collider.gameObject;
        carriedRigidbody = carriedObject.GetComponent<Rigidbody>();
        carriedObject.transform.parent = transform;
        carriedObject.transform.localPosition = heldObjectPoint.localPosition;
        carriedObject.transform.rotation = carriedObject.transform.parent.rotation;
        spring.connectedBody = carriedRigidbody;
        spring.spring = carriedRigidbody.mass * 1000.0f;
        carriedRigidbody.drag = 10.0f;
        carriedRigidbody.useGravity = false;
        carriedRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        try
        {
            carriedObject.GetComponent<IMoveable>().Carrier = gameObject;
        }
        catch (Exception) { }

        //carriedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    }

    public void GrabObject()
    {
        RaycastHit[] hitArray = Physics.CapsuleCastAll(
            point1: controller.GetPoint1(),
            point2: controller.GetPoint2(),
            radius: GetComponent<CapsuleCollider>().radius,
            direction: transform.forward,
            maxDistance: GetComponent<CapsuleCollider>().radius * grabRange
            );

        GrabObject(hitArray);
    }

    public void DropObject()
    {
        try
        {
            carriedRigidbody.useGravity = true;
            carriedRigidbody.drag = 1.0f;
            carriedRigidbody.constraints = RigidbodyConstraints.None;
            carriedRigidbody = null;
            carriedObject.transform.parent = null;
            carriedObject = null;
            spring.connectedBody = null;
            //carriedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //carriedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
        catch (NullReferenceException) { }
        catch (UnassignedReferenceException) { };

        return;
    }

    public void OnPlayerDeath() => DropObject();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")
            || collision.gameObject.CompareTag("Enemy1")
            || collision.gameObject.CompareTag("Enemy2"))
            DropObject();
    }
}
