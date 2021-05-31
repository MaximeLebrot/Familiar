using UnityEngine;

public class DoorConsole : MonoBehaviour
{
    [SerializeField, Tooltip("Fill array with doors that this console should open. Handles opening in alternating order")]
    private GameObject[] doors;

    [SerializeField, Tooltip("Fill array with doors that on activation should open simultaneously, should be left empty unless on last console")]
    private GameObject[] allDoors; //fill array if activation should open them all, else leave empty

    [SerializeField, Tooltip("Fill array with doors that on activation should open simultaneously, should be left empty unless on last console")]
    private FireZone[] fireZone; //fill array if activation should stop the fire zone

    [Tooltip("Checks whether the player can use this console or not")]
    private bool canUseConsole;

    bool puzzleComplete;

    [SerializeField]
    private AbilitySystem.Player player;
   

    private void Start()
    {
        if (allDoors.Length > 0)
        {
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilitySystem.Player>();
        }
        
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
        if (canUseConsole == true && Input.GetKeyDown(KeyCode.E) && puzzleComplete != true)
            DoorSwap();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //ui "press e to activate"
            canUseConsole = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //remove ui "press e to activate"
            canUseConsole = false;
        }
    }

    private void DoorSwap()
    {
        for (int i = 0; i<doors.Length; i++)
        {
            doors[i].GetComponent<Animator>().SetBool("isOpen", !doors[i].GetComponent<Animator>().GetBool("isOpen"));
        }
        if (allDoors.Length > 0)
        {
            puzzleComplete = true;
            foreach (GameObject door in allDoors)
            {
                door.GetComponent<Animator>().SetBool("isOpen", true);
                if (door.GetComponent<Door>() != null)
                    door.GetComponent<Door>().DoorOpened();
                player.AudioHandler.PlayPuzzleCompletionSound();
            }
        }
        if (fireZone.Length > 0)
        {
            foreach (FireZone zone in fireZone)
                zone.Deactivate();
        }

    }
}
