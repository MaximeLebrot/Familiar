using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

//Primary author: Simon Canbäck
//Secondary author: Maxime Lebrot
public class ShootingScript : MonoBehaviour
{
    //private bool canFire = true;
    //fireCooldown

    Controller controller;
    public GameObject connector;
    public GameObject spotlight;
    public BoxCollider connectiveSpace;

    public bool CanFire
    {
        get; 
        set;
    }

    void Start()
    {
        controller = GetComponent<Controller>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && CanFire)
        {
            //Shoot();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            CanFire = true;
        }


    }

    public void Shoot()
    {
        CanFire = false;
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
    public IEnumerator ResetCanFire()
    {
        //canFire = false;
        yield return new WaitForSeconds(1.0f);
        CanFire = true;
    }
}