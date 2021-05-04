using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Moveable")
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
