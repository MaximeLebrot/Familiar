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

    void Start()
    {
        controller = GetComponent<Controller>();
    }

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

        Physics.Raycast(camRay, out RaycastHit hitPoint, 20.0f/*LayerMask*/);
        Ray playerRay = new Ray(transform.position, (hitPoint.point - transform.position).normalized);
        Debug.DrawRay(playerRay.origin, playerRay.direction * 20.0f, Color.yellow, 1.0f);

        gameObject.GetComponentInChildren<ParticleSystem>().Play();

        if (Physics.Raycast(playerRay, out hitPoint, 20.0f/*LayerMask*/))
        {
            if (hitPoint.collider.GetComponent<IZappable>() != null)
            {
                foreach (IZappable iz in hitPoint.collider.GetComponents<IZappable>())
                {
                    iz.OnZap();
                }

                return;
            }

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
        if (carriedObject != null
            && carriedObject.transform.parent == gameObject.transform)
        {
            carriedObject.transform.parent = null;
            carriedObject = null;
            return;
        }

        int hitIndex = -1;

        RaycastHit[] hitArray = Physics.CapsuleCastAll(
            point1: controller.GetPoint1(),
            point2: controller.GetPoint2(),
            radius: GetComponent<CapsuleCollider>().radius,
            direction: transform.forward,
            maxDistance: GetComponent<CapsuleCollider>().radius * 3.0f
            );

        if (hitArray.Length == 0)
            return;

        for (int i = 0; i < hitArray.Length; i++)
        {
            if (hitArray[i].collider.CompareTag("Moveable"))
            {
                hitIndex = i;
                break;
            }
        }

        if (hitIndex < 0)
            return;

        carriedObject = hitArray[hitIndex].collider.gameObject;
        carriedObject.transform.parent = transform;
    }
}