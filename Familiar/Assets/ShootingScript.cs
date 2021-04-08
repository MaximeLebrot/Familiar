using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    Controller controller;
    private GameObject carriedObject;
    public GameObject connector;
    public GameObject spotlight;
    public BoxCollider connectiveSpace;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        //Collider[] colArray = Physics.OverlapBox(connectiveSpace.transform.position, connectiveSpace.size);

        //foreach (Collider c in colArray)
        //{
        //    if (c.CompareTag("Connector"))
        //    {
        //        spotlight.SetActive(true);
        //        break;
        //    }

        //    spotlight.SetActive(false);
        //}

        if (Input.GetButtonDown("Fire2"))
        {
            Grab();
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }


    }

    void Shoot()
    {

        Ray camRay = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        //Ray ray = new Ray(transform.position, getCrosshairFromCamera());
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan, 2f);
        //Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.height / 2, Screen.width / 2));
        RaycastHit hitPoint;

        if (Physics.Raycast(camRay, out hitPoint, 100.0f/*LayerMask*/))
            if (hitPoint.collider.CompareTag("Switch"))
            {
                Collider[] colArray = Physics.OverlapBox(connectiveSpace.transform.position, connectiveSpace.size);

                foreach (Collider c in colArray)
                {
                    if (c.CompareTag("Connector"))
                    {
                        spotlight.SetActive(!spotlight.activeSelf);
                    }
                }
            }
    }

    void Grab()
    {
        if (carriedObject != null && carriedObject.transform.parent == gameObject.transform)
        {
            carriedObject.transform.parent = null;
            carriedObject = null;
            return;
        }
        //RaycastHit? hit = null;
        //RaycastHit[] hitArray = Physics.CapsuleCastAll(
        //    point1: controller.GetPoint1(),
        //    point2: controller.GetPoint2(),
        //    radius: GetComponent<CapsuleCollider>().radius,
        //    direction: transform.forward,
        //    maxDistance: GetComponent<CapsuleCollider>().radius * 3.0f,
        //    layerMask: controller.collisionMask
        //    );
        //foreach (RaycastHit r in hitArray)
        //{
        //    if (r.collider.CompareTag("Connector"))
        //    {
        //        hit = r;
        //        break;
        //    }
        //}

        //if (hit == null)
        //    return;


        //if (hit.Value.collider.gameObject.CompareTag("Connector"))
        //{
        //Debug.Log("raycast" + hit.ToString());
        carriedObject = GameObject.FindGameObjectWithTag("Connector");
            carriedObject.transform.parent = transform;
        //}

    }
}