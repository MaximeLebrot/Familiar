using System;
using UnityEngine;

//Author: Simon Canbäck
public class GrabObjectScript : MonoBehaviour
{
    [SerializeField]
    private static readonly float grabRange = 3.0f;

    private Controller controller;

    [SerializeField] private GameObject carriedObject;
    [SerializeField] private Transform heldObjectPoint;

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
        carriedObject.transform.localPosition = heldObjectPoint.localPosition;
        carriedObject.transform.rotation = carriedObject.transform.parent.rotation;
        carriedObject.GetComponent<Rigidbody>().useGravity = false;
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
            carriedObject.GetComponent<Rigidbody>().useGravity = true;
            carriedObject.transform.parent = null;
            carriedObject = null;
            //carriedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //carriedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
        catch (NullReferenceException) { }
        catch (UnassignedReferenceException) { };

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
