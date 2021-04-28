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

    // Start is called before the first frame update
    void Start()
    {
        count = 0f;
    }

    // Update is called once per frame
    void Update()
    {

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