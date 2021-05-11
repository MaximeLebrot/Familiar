using UnityEngine;

public class PressurePlateOneOrBoth : MonoBehaviour
{
    [SerializeField] private GameObject door;
    private PressurePlate[] pressurePlates;

    void Start()
    {
        pressurePlates = GetComponentsInChildren<PressurePlate>();
    }

    public void UpdatePuzzle()
    {
        foreach (PressurePlate pressurePlate in pressurePlates)
        {
            if (pressurePlate.active)
            {
                door.SetActive(false);
                return;
            }
            door.SetActive(true);
        }
    }
}
