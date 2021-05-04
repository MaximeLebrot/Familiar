using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorConsole : MonoBehaviour
{

    public GameObject[] doors;

    public GameObject[] allDoors;

    public bool canUseConsole;

    private void Start()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            //first element put in is going to be active
            if (i % 2 == 0)
            {
                doors[i].SetActive(true);
            }
            else
            {
                doors[i].SetActive(false);
            }
        }
        //if (doors.Length > 0)
        //doors[0].SetActive(true);
        //if (doors.Length > 1)
        //doors[1].SetActive(false);
    }

    private void Update()
    {
        if (canUseConsole)
        {
            //if (Input.GetButton("Fire1"))
            //{
            //    DoorSwap();
            //}
            if (Input.GetKeyDown(KeyCode.E))
            {
                DoorSwap();
            }
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //ui "press e to activate"
            canUseConsole = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //remove ui "press e to activate"
            canUseConsole = false;
        }
    }

    private void DoorSwap()
    {
        for (int i = 0; i<doors.Length; i++)
        {
            doors[i].SetActive(!doors[i].activeInHierarchy);
        }
        //if (doors.Length > 0)
        //{
            //doors[0].SetActive(!doors[0].activeInHierarchy);
            //if (doors.Length > 1)
                //doors[1].SetActive(!doors[1].activeInHierarchy);
        //}
        if (allDoors.Length > 0)
        {
            foreach (GameObject door in allDoors)
                door.SetActive(false);
        }
    }
}
