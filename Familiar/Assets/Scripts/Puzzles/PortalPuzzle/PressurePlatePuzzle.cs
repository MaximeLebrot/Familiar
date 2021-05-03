using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlatePuzzle : MonoBehaviour
{
    public GameObject door;
    public bool shouldOpen;
    public MultiplePressurePlates[] childPressurePlates;

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
                //CheckIfShouldOpen();
                return;
            }
        }
        CheckIfShouldOpen();
    }
    private void CheckIfShouldOpen()
    {
        if (shouldOpen)
            door.SetActive(false);
        foreach (MultiplePressurePlates pressurePlate in childPressurePlates)
        {
            pressurePlate.ChangeMaterial();
        }
        if (!shouldOpen)
            door.SetActive(true);
    }
}
