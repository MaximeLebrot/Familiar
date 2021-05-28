using AbilitySystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Script for updating the Moonstone UI in the final room(s) in level 2.
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

    public GameObject moonstoneParent; // Used to find the animator component.
    [SerializeField]
    private Animator anim;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (anim == null)
            anim = moonstoneParent.GetComponent<Animator>();
    }

    void Update()
    {
        // Activates the text once the player picked up the first moonstone.
        if (player.StoneCounter == 1 && !activated) // Bool makes sure this is only done once
        {
            activated = true;
            moonCounter = player.StoneCounter;
            moonstoneText.text = " Collected a Moonstone"; // Special text for the first moonstone.
            anim.SetTrigger("Activated"); // Triggers animation so UI is visable.
        }

        // Updates the text.
        if (moonCounter != player.StoneCounter)
        {
            if (moonCounter == 1) // Special animation for the first update since more text needs to be changed.
                anim.SetTrigger("FirstUpdate");
            else // Same animation for the rest
                anim.SetTrigger("Update");
            moonCounter = player.StoneCounter;

            //StopAllCoroutines();
            StartCoroutine(Delay()); // So the update animation can play.
            moonstoneText.text = " " + moonCounter + " / 6 Moonstones collected"; // Be aware of the 6 moonstones.
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.75f);
    }
}
