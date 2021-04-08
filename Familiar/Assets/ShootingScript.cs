using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    Controller controller;
    public GameObject carriedObject;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
            Grab();


    }

    void Grab()
    {
        if (carriedObject != null && carriedObject.transform.parent == gameObject.transform)
        {
            carriedObject.transform.parent = null;
            carriedObject = null;
            return;
        }

        //if (Physics.CapsuleCast(
        //    point1: controller.GetPoint1(),
        //    point2: controller.GetPoint2(),
        //    radius: GetComponent<CapsuleCollider>().radius,
        //    direction: transform.forward, 
        //    maxDistance: GetComponent<CapsuleCollider>().radius* 3.0f,
        //    layerMask: controller.collisionMask,
        //    hitInfo: out RaycastHit hit
        //    ) 
        //    && hit.collider.gameObject.CompareTag("Connector"))
        //{
        //Debug.Log("raycast" + hit.ToString());
        carriedObject = GameObject.FindGameObjectWithTag("Connector");
        carriedObject.transform.parent = transform;
        //}

    }
}
