using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateOneOrBoth : MonoBehaviour
{
    public bool shouldOpen;

    public PressurePlate[] pressurePlates;
    public GameObject door;

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
                shouldOpen = true;
                CheckIfShouldOpen();
                return;
            }
            else
                shouldOpen = false;
        }
        CheckIfShouldOpen();
    }

    private void CheckIfShouldOpen()
    {
        if (shouldOpen)
            door.SetActive(false);
        if (!shouldOpen)
            door.SetActive(true);
    }
}
