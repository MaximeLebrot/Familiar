using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Tooltip("Checks whether this pressure plate is active or not")]
    private bool isActive;

    [SerializeField, Tooltip("A reference to the parent script of this pressure plate. Should be inputed manually")]
    private PressurePlateOneOrBoth parent;
    [SerializeField, Tooltip("A reference to the animator attached to this object. Should be inputed manually")]
    private Animator anim;
    [SerializeField]
    private AudioSource audioS;
    [SerializeField]
    private AudioClip audioC;
    private static readonly float volumeMultiplier = 0.2f;

    void Start()
    {
        InitializeSequence();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Moveable"))
        {
            anim.SetBool("isPressed", true);
            isActive = true;
            parent.UpdatePuzzle();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Moveable"))
        {
            anim.SetBool("isPressed", false);
            isActive = false;
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

    public void PlayPressurePlateSound()
    {
        audioS.PlayOneShot(audioC, Sound.Instance.EffectsVolume * volumeMultiplier);
    }

    public bool IsActive
    {
        get => isActive;
    }
}
