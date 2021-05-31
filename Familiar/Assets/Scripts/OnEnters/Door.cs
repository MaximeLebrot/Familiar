using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    [HideInInspector, Tooltip("Checks whether the door is open or not")]
    public bool open;
    [SerializeField, Tooltip("The Animator component tied to this object")]
    private Animator animator;
    [SerializeField]
    private GameObject wizard;
    [SerializeField]
    private NavMeshAgent navAgent;
    [SerializeField]
    private GameObject destination;
    [SerializeField]
    private GameObject eliasProgTrigger;

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
            if (wizard != null)
                SetWizardDestination();
        }
    }

    public void DoorOpened()
    {
        eliasProgTrigger.SetActive(true);
        navAgent.SetDestination(destination.transform.position);
    }

    private void SetWizardDestination()
    {
        navAgent.SetDestination(destination.transform.position);
    }
}
