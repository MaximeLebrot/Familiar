using UnityEngine;

public class Door : MonoBehaviour
{
    [HideInInspector, Tooltip("Checks whether the door is open or not")]
    public bool open;
    [SerializeField, Tooltip("The Animator component tied to this object")]
    private Animator animator;
    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>(); //vild kod
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            open = true;
            animator.SetBool("open", true);
        }
    }
}
