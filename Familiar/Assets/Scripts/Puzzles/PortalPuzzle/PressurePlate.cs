using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Tooltip("Checks whether this pressure plate is active or not")]
    private bool active;

    [SerializeField, Tooltip("A reference to the parent script of this pressure plate. Should be inputed manually")]
    private PressurePlateOneOrBoth parent;
    [SerializeField, Tooltip("A reference to the animator attached to this object. Should be inputed manually")]
    private Animator anim;

    void Start()
    {
        InitializeSequence();
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

    private void InitializeSequence()
    {
        InitializeAnim();
        InitializeParent();
    }

    private void InitializeAnim()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
            Debug.LogWarning("Anim value should be set in the inspector");
            if (anim == null)
                Debug.LogError("Could not find animator");
        }
    }

    private void InitializeParent()
    {
        if (parent == null)
        {
            parent = GetComponentInParent<PressurePlateOneOrBoth>();
            Debug.LogWarning("Pressure plate parent value should be set in the inspector");
            if (parent == null)
                Debug.LogError("Could not find pressure plate parent");
        }
    }

    public bool Active
    {
        get => active;
    }
}
