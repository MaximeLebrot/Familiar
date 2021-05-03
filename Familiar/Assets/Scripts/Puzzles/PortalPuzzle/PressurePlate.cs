using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private PressurePlateOneOrBoth parent;
    public bool active;

    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        parent = GetComponentInParent<PressurePlateOneOrBoth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Moveable"))
        {
            anim.SetTrigger("Click"); 
            active = true;
            parent.UpdatePuzzle();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Moveable"))
        {
            anim.SetTrigger("Unclick");
            active = false;
            parent.UpdatePuzzle();
        }
    }
}
