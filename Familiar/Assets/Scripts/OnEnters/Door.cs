using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    [HideInInspector, Tooltip("Checks whether the door is open or not")]
    public bool open;
    [SerializeField, Tooltip("The Animator component tied to this object")]
    private Animator animator;
    [SerializeField]
    private AbilitySystem.Player player;
    [SerializeField]
    private GameObject wizard;
    [SerializeField]
    private NavMeshAgent navAgent;
    [SerializeField]
    private GameObject destination;
    [SerializeField]
    private GameObject eliasProgTrigger;
    [SerializeField]
    private bool keyShouldAnimate;

    [SerializeField]
    private bool shouldPlaySuccessSound;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>(); //vild kod
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            if (keyShouldAnimate)
                other.gameObject.GetComponent<Animator>().SetTrigger("isUsed");
            if (shouldPlaySuccessSound)
                player.AudioHandler.PlayPuzzleCompletionSound();
            open = true;
            animator.SetBool("open", true);
            if (wizard != null)
                SetWizardDestination();
        }
    }

    public void DoorOpened()
    {
        if (eliasProgTrigger != null)
            eliasProgTrigger.SetActive(true);
        if (navAgent != null && destination != null)
            navAgent.SetDestination(destination.transform.position);
    }

    private void SetWizardDestination()
    {
        if (navAgent != null)
            navAgent.SetDestination(destination.transform.position);
    }
}
