using System;
using UnityEngine;

//Author: Simon Canbäck
public class GrabObjectScript : MonoBehaviour
{
    [SerializeField]
    private static readonly float grabRange = 3.0f;

    private Controller controller;

    [SerializeField]
    private GameObject carriedObject;
    private Rigidbody carriedObjectRB;
    [SerializeField]
    private Transform heldObjectPoint;
    [SerializeField]
    private float throwForce = 20.0f;

    public static float GrabRange
    {
        get
        {
            return grabRange;
        }
    }

    public GameObject CarriedObject
    {
        get
        {
            return carriedObject;
        }

        set
        {
            carriedObject = value;
        }
    }

    private void Awake()
    {
        if (heldObjectPoint == null)
            heldObjectPoint = GameObject.FindGameObjectWithTag("HOLP").transform;
        controller = GetComponent<Controller>();
    }

    void Update()
    {

    }

    public void ToggleGrab()
    {
        if (carriedObject == null)
            GrabObject();
        else
            DropObject();
    }

    private void FixedUpdate()
    {
        if (carriedObject != null)
            carriedObjectRB.MovePosition(heldObjectPoint.position);
    }

    public void GrabObject(RaycastHit[] hitArray)
    {
        int hitIndex = -1;

        if (hitArray.Length == 0)
            return;

        for (int i = 0; i < hitArray.Length; i++)
        {
            if (hitArray[i].collider.CompareTag("Moveable") || hitArray[i].collider.CompareTag("Key")) //Emils dumma ändringar
            {
                hitIndex = i;
                break;
            }
        }

        if (hitIndex < 0)
            return;

        carriedObject = hitArray[hitIndex].collider.gameObject;
        carriedObject.transform.parent = transform;
        carriedObject.transform.rotation = carriedObject.transform.parent.rotation;

        carriedObjectRB = carriedObject.GetComponent<Rigidbody>();
        carriedObjectRB.useGravity = false;
        carriedObjectRB.isKinematic = true;
        //carriedObjectRB.transform.position = heldObjectPoint.position;
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
        Debug.Log("throwing");
        try
        {
            carriedObjectRB.useGravity = true;
            carriedObjectRB.velocity += controller.Camera.transform.forward * throwForce + controller.Camera.transform.up * throwForce;
            carriedObjectRB.isKinematic = false;

            carriedObject.transform.parent = null;
            carriedObject = null;
            //carriedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //carriedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
        catch (NullReferenceException) { Debug.Log("null ref"); }
        catch (UnassignedReferenceException) { Debug.Log("unass ref"); };

        return;
    }

    public void OnPlayerDeath()
    {
        //Debug.Log("In GrabObjectScript.OnPlayerDeath()");

        DropObject();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")
            || collision.gameObject.CompareTag("Enemy1")
            || collision.gameObject.CompareTag("Enemy2"))
            DropObject();
    }
}
