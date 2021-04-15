using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    private bool canFire = true;
    //fireCooldown

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

        if (Input.GetButton("Fire1") && canFire)
        {
            //Shoot();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            canFire = true;
        }


    }

    public void Shoot()
    {
        canFire = false;
        Ray camRay = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

        Physics.Raycast(camRay, out RaycastHit hitPoint, 100.0f/*LayerMask*/);
        Ray playerRay = new Ray(transform.position, (hitPoint.point - transform.position).normalized);
        Debug.DrawRay(playerRay.origin, playerRay.direction * 100, Color.yellow, 1f);

        GetComponentInChildren<ParticleSystem>().Play();

        if (Physics.Raycast(playerRay, out hitPoint, 100.0f/*LayerMask*/))
        {
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
            //else if (hitPoint.collider.CompareTag("Enemy2"))
            //{
            //    Enemy2 enemy2;
            //    enemy2 = hitPoint.collider.gameObject.GetComponent<Enemy2>();
            //    enemy2.zapped = true;
            //}            
            else if (hitPoint.collider.CompareTag("Enemy2"))
            {
                Enemy2 enemy2;
                enemy2 = hitPoint.collider.gameObject.GetComponent<Enemy2>();
                enemy2.health--;
            }
            else if (hitPoint.collider.CompareTag("Enemy1"))
            {
                Enemy1 enemy1;
                enemy1 = hitPoint.collider.gameObject.GetComponent<Enemy1>();
                enemy1.zapped = true;
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