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
    private GameObject destination1;
    [SerializeField]
    private GameObject destination2;
    [SerializeField]
    private GameObject eliasProgTrigger;
    [SerializeField]
    private bool keyShouldAnimate;

    private bool pathing;

    [SerializeField]
    private bool shouldPlaySuccessSound;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>(); //vild kod
    }
    private void Update()
    {
        if (pathing)
            CheckForPath();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            if (keyShouldAnimate)
            {
                other.gameObject.GetComponent<Animator>().SetTrigger("isUsed");
                player.GetComponent<GrabObjectScript>().DropObject();
                Destroy(other, 0.5f);
            }
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
        if (navAgent != null && destination1 != null)
            navAgent.SetDestination(destination1.transform.position);
    }

    private void SetWizardDestination()
    {
        if (navAgent != null)
        {
            if (destination2 != null)
                pathing = true;
            navAgent.SetDestination(destination1.transform.position);
        }
    }

    void CheckForPath()
    {
        if (destination2 != null)
            navAgent.SetDestination(destination2.transform.position);
        
        if (Vector3.Distance(transform.position, destination2.transform.position) < 0.2f)
            pathing = false;
    }
}
