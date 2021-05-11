using UnityEngine;

public class PressurePlatePuzzle : MonoBehaviour
{
    [SerializeField] private GameObject door;
    private bool shouldOpen;
    private MultiplePressurePlates[] childPressurePlates;

    private void Start()
    {
        childPressurePlates = GetComponentsInChildren<MultiplePressurePlates>();
    }

    public void UpdatePuzzle()
    {
        foreach (MultiplePressurePlates pressurePlate in childPressurePlates)
        {
            if (pressurePlate.active)
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
}
