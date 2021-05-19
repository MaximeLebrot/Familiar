using AbilitySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoonstoneUI : MonoBehaviour
{
    // Borrowed from the script Fin
    // Need the player since they have information on how many pickups they've picked up.
    [SerializeField, Tooltip("A reference to the \"Player\" script on the player game object. Should be inputed manually")]
    private Player player;

    private int moonCounter = 0;
    private bool activated = false;

    // The moonstone texts UI.
    public Text moonstoneText;

    // Probably needs an animator...
    public GameObject moonstoneParent;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        moonstoneParent.SetActive(false); // Change for animation later
    }

    // Update is called once per frame
    void Update()
    {
        if (player.StoneCounter == 1 && !activated)
        {
            moonstoneParent.SetActive(true); // Change for animation later
            activated = true;
            moonCounter = player.StoneCounter;
            moonstoneText.text = " Collected a Moonstone";
            // Activate/ animate the UI text so its visable.
            // Update the text ?
        }

        // Updates the text.
        if (moonCounter != player.StoneCounter)
        {
            moonCounter = player.StoneCounter;
            moonstoneText.text = " " + moonCounter + " / 6 Moonstones collected";
            // Add animation?
        }

    }
}
