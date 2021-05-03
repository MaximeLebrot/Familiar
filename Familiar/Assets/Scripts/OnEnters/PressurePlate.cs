using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    // Opens doors when a box with tag movable is on it's trigger. Could be prettier.
    public BoxCollider boxTrigger;

    public GameObject door;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Moveable"))
        {
            anim.SetTrigger("Click");
            door.SetActive(false);
        }
    }
}
