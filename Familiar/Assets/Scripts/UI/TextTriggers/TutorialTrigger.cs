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
    public string tutorial;
    public float activeTime;
    public bool permanent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialText.text = "";
            // SFX here?
            tutorialText.text = tutorial;
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
            tutorialText.text = "";
        }
        gameObject.SetActive(false);
    }

    // Used to null out the text when player leaves trigger.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && tutorialText.text == tutorial)
        {
            tutorialText.text = "";
        }
    }
}
