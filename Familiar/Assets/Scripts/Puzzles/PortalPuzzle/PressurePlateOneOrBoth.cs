using UnityEngine;

public class PressurePlateOneOrBoth : MonoBehaviour
{
    [SerializeField, Tooltip("The game object that should open in case of puzzle completion")]
    private GameObject door;
    [SerializeField, Tooltip("Array of references to pressure plates that are in play in this puzzle")]
    private PressurePlate[] pressurePlates;
    Animator animator; //vild kod

    void Start()
    {
        InitializeSequence();
        animator = door.GetComponent<Animator>(); //vild kod
    }

    public void UpdatePuzzle()
    {
        foreach (PressurePlate pressurePlate in pressurePlates)
        {
            if (pressurePlate.Active)
            {
                animator.SetBool("open", true); //vild kod
                //door.SetActive(false);
                return;
            }
            animator.SetBool("open", false); //vild kod
            //door.SetActive(true);
        }
    }

    private void InitializeSequence()
    {
        InitializeChildren();
    }
    private void InitializeChildren()
    {
        if (pressurePlates == null)
        {
            pressurePlates = GetComponentsInChildren<PressurePlate>();
            Debug.LogWarning("Children pressure plate values should be set in the inspector");
            if (pressurePlates == null)
                Debug.LogError("Cannot find pressure plates"); //skulle kunna ge mer info -> typ att jag använder GetComponentInChildren
        }
    }
}
