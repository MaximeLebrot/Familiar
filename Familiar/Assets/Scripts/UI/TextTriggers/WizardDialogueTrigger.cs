using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardDialogueTrigger : MonoBehaviour
{
    // The dialogue isn't permanent
    // Gets nulled after a specified time, not affected by the player leaving the trigger.
    public GameObject dialogueParent;
    public Text dialogueText;
    public string dialogue;
    public float activeTime;
    public bool needKey;
    public string expressionTrigger; // The expressions trigger

    public Animator anim; // Dialogue panels animator
    public Animator expressionAnim; // Expression images animator

    private void OnTriggerEnter(Collider other)
    {
        // Checks if its the player or key entering the trigger.
        if (other.CompareTag("Player") && !needKey || other.CompareTag("Key") && needKey)
        {
            expressionAnim.SetTrigger(expressionTrigger);
            // Sound effect here ?
            dialogueText.text = dialogue;
            anim.SetBool("inUse", true); // Animates in the panel

            // Start a coroutine to count down how long the text should be up.
            StartCoroutine(ActiveTime());
           
        }
    }

    // The text resets to blank if the player leaves the trigger without OnTriggerExit.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !needKey || other.CompareTag("Key") && needKey)
        {
            dialogueText.text = dialogue;
        }
    }

    // Waits for the specified time then closes the dialogue panel with animation and deactivates trigger.
    private IEnumerator ActiveTime()
    {
        yield return new WaitForSeconds(activeTime);
        anim.SetBool("inUse", false); // Animates out the panel
        // Maybe add something here so the text gets changed to ""?
        gameObject.SetActive(false);
    }
}
