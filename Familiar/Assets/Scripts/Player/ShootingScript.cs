using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

//Primary author: Simon Canbäck
//Secondary author: Maxime Lebrot
public class ShootingScript : MonoBehaviour
{
    [SerializeField] private float attackRadius = 1.0f;
    [SerializeField] private float attackRange = 3.0f;

    [SerializeField] private Transform attackOrigin;
    [SerializeField] private ParticleSystem zapVFX;

    public bool CanFire
    {
        get; 
        set;
    }

    void Start()
    {
        CanFire = true;
    }

    public void Shoot()
    {
        RaycastHit[] hitArray;

        //Ray camRay = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

        //Physics.Raycast(camRay, out RaycastHit hitPoint, 20.0f/*LayerMask*/);
        //Ray playerRay = new Ray(transform.position, (hitPoint.point - transform.position).normalized);
        //Debug.DrawRay(playerRay.origin, playerRay.direction * 20.0f, Color.yellow, 1.0f);

        zapVFX.Play();

        hitArray = Physics.SphereCastAll(attackOrigin.position, attackRadius, transform.forward, attackRange);

        if (hitArray.Length > 0)
        {
            foreach (RaycastHit hit in hitArray)
            {
                if (hit.collider.gameObject == gameObject)
                    continue;

                if (hit.collider.GetComponent<IZappable>() != null)
                {
                    

                    foreach (IZappable iz in hit.collider.GetComponents<IZappable>())
                    {
                        iz.OnZap();
                    }

                    return;
                }

                if (hit.collider.CompareTag("Enemy2"))
                {
                    Enemy2 enemy2;
                    enemy2 = hit.collider.gameObject.GetComponent<Enemy2>();
                    //enemy2.TakeDamage(1);
                }
                else if (hit.collider.CompareTag("Enemy1"))
                {
                    Enemy1 enemy1;
                    enemy1 = hit.collider.gameObject.GetComponent<Enemy1>();
                    enemy1.IsZapped = true;
                }
            }
        }
        //if (Physics.Raycast(playerRay, out hitPoint, 20.0f/*LayerMask*/))
        //{
        //    if (hitPoint.collider.GetComponent<IZappable>() != null)
        //    {
        //        foreach (IZappable iz in hitPoint.collider.GetComponents<IZappable>())
        //        {
        //            iz.OnZap();
        //        }

        //        return;
        //    }

        //    if (hitPoint.collider.CompareTag("Enemy2"))
        //    {
        //        Enemy2 enemy2;
        //        enemy2 = hitPoint.collider.gameObject.GetComponent<Enemy2>();
        //        //enemy2.TakeDamage(1);
        //    }
        //    else if (hitPoint.collider.CompareTag("Enemy1"))
        //    {
        //        Enemy1 enemy1;
        //        enemy1 = hitPoint.collider.gameObject.GetComponent<Enemy1>();
        //        enemy1.IsZapped = true;
        //    }
        //}
    }
    public IEnumerator ResetCanFire()
    {
        CanFire = false;
        yield return new WaitForSeconds(1.0f);
        CanFire = true;
    }
}