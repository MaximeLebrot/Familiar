using UnityEngine;

public class PressurePlatePuzzle : MonoBehaviour
{
    [SerializeField, Tooltip("The game object that should open in case of puzzle completion")] 
    private GameObject door;
    [Tooltip("Checks whether the door should open or not")]
    private bool shouldOpen;
    [SerializeField, Tooltip("Array of references to pressure plates that are in play in this puzzle")]
    private MultiplePressurePlates[] childPressurePlates;

    private void Start()
    {
        InitializeSequence();
    }

    public void UpdatePuzzle()
    {
        foreach (MultiplePressurePlates pressurePlate in childPressurePlates)
        {
            if (pressurePlate.Active)
                shouldOpen = true;
            else
            {
                shouldOpen = false;
                return;
            }
        }
        CheckIfShouldOpen();
    }
    private void CheckIfShouldOpen()
    {
        if (shouldOpen == true)
        {
            door.SetActive(false);
            foreach (MultiplePressurePlates pressurePlate in childPressurePlates)
            {
                pressurePlate.ChangeMaterial();
            }
            return;
        }
        door.SetActive(true);
    }
    private void InitializeSequence()
    {
        InitializeChildren();
    }
    private void InitializeChildren()
    {
        if (childPressurePlates == null)
        {
            Debug.LogWarning("Children pressure plate values should be set in the inspector");
            childPressurePlates = GetComponentsInChildren<MultiplePressurePlates>();
            if (childPressurePlates == null)
                Debug.LogError("Cannot find pressure plates"); //skulle kunna ge mer info -> typ att jag använder GetComponentInChildren
        }
    }
}
