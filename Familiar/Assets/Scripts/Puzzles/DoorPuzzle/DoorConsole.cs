using UnityEngine;

public class DoorConsole : MonoBehaviour
{
    [SerializeField, Tooltip("Fill array with doors that this console should open. Handles opening in alternating order")]
    private GameObject[] doors;

    [SerializeField, Tooltip("Fill array with doors that on activation should open simultaneously, should be left empty unless on last console")]
    private GameObject[] allDoors; //fill array if activation should open them all, else leave empty

    [Tooltip("Checks whether the player can use this console or not")]
    private bool canUseConsole;
   

    private void Start()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            //first element put in is going to be active
            if (i % 2 == 0)
            {
                doors[i].GetComponent<Animator>().SetBool("isOpen", false);
            }
            else
            {
                doors[i].GetComponent<Animator>().SetBool("isOpen", true);
            }
        }
    }

    private void Update()
    {
        if (canUseConsole == true && Input.GetKeyDown(KeyCode.E))
            DoorSwap();
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
            //doors[i].SetActive(!doors[i].activeInHierarchy);
            doors[i].GetComponent<Animator>().SetBool("isOpen", !doors[i].GetComponent<Animator>().GetBool("isOpen"));
        }
        if (allDoors.Length > 0)
        {
            foreach (GameObject door in allDoors)
                door.GetComponent<Animator>().SetBool("isOpen", true);
        }
    }
}
