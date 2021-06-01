using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureV2 : MonoBehaviour
{
    // Opens doors when a box with tag movable is on it's trigger. Could be prettier.
    public BoxCollider boxTrigger;
    public BoxCollider boxTrigger2;

    public GameObject door;
    private float count;

    void Start()
    {
        count = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Moveable"))
        {
            count++;
            Debug.Log("count is = " + count);
        }

        if (count == 2)
        {
            door.SetActive(false);
        }
    }

}