using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        if (collision.collider.tag == "Moveable")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Moveable")
        {
            Destroy(gameObject);
        }
    }
}
