using System;
using UnityEngine;

//Author: Simon Canb�ck
public class GrabObjectScript : MonoBehaviour
{
    [SerializeField]
    private static readonly float grabRange = 3.0f;

    private Controller controller;

    [SerializeField]
    private GameObject carriedObject;
    [SerializeField]
    private Transform heldObjectPoint;
    private Vector3 HopPos;

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
        HopPos = new Vector3(0, 0, 3);
        if (heldObjectPoint == null)
            heldObjectPoint = GameObject.FindGameObjectWithTag("HOLP").transform;
        heldObjectPoint.transform.localPosition = HopPos;
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

    public void GrabObject(RaycastHit[] hitArray)
    {
        int hitIndex = -1;

        if (hitArray.Length == 0)
            return;

        for (int i = 0; i < hitArray.Length; i++)
        {
            if (hitArray[i].collider.CompareTag("Moveable") || hitArray[i].collider.CompareTag("Key")) //Emils dumma �ndringar
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
