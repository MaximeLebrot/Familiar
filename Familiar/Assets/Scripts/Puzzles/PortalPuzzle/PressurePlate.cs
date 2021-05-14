using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool active;

    private PressurePlateOneOrBoth parent;
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
