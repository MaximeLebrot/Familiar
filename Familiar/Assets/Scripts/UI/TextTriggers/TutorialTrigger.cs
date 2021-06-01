using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    // Tutorial text is only active within the trigger.
    // Is sometimes permanent, hence the bool.
    // Maybe add animation for fade in/ out?
    public Text tutorialText;
    public static readonly string tutorial = "Use 'Left Click' to interact with the Mechanism";
    public float activeTime;
    public bool permanent;

    public Animator anim; // Tutorialtexts animator.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //tutorialText.text = "";
            // SFX here?
            tutorialText.text = tutorial;
            anim.SetBool("isActive", true); // Fade in text
            
            if (!permanent)
            {
                StartCoroutine(WaitAndDeactivate());
            }
        }
    }

    // If the tutorial text isn't permanent it gets changed to "" after specified time.
    // The trigger also gets deactivated.
    private IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(activeTime);
        if (tutorialText.text == tutorial)
        {
            anim.SetBool("isActive", false); // Fade out text
            //tutorialText.text = "";
        }
        gameObject.SetActive(false);
    }

    // Used to null out the text when player leaves trigger.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && tutorialText.text == tutorial)
        {
            anim.SetBool("isActive", false); // Fade out text
            //tutorialText.text = "";
        }
    }
}
