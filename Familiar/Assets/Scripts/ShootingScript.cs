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
        if (Input.GetButtonDown("Fire2"))
        {
            Grab();
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }


    }

    void Shoot()
    {

        Ray camRay = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

        GetComponentInChildren<ParticleSystem>().Play();

        if (Physics.Raycast(camRay, out RaycastHit hitPoint, 100.0f/*LayerMask*/))
            if (hitPoint.collider.CompareTag("Switch"))
            {
                Collider[] colArray = Physics.OverlapBox(connectiveSpace.transform.position, connectiveSpace.size);

                foreach (Collider c in colArray)
                {
                    if (c.CompareTag("Connector"))
                    {
                        hitPoint.collider.gameObject.GetComponent<ElectricalSwitchScript>().OnZap();
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
        carriedObject = GameObject.FindGameObjectWithTag("Conductor");
            carriedObject.transform.parent = transform;
        //}

    }
}