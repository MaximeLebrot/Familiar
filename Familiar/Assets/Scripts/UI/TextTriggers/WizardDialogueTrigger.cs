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
    // need different dialogue triggers on the same place, use key bool.
    public bool needKey;
    public Image expressionPosition;
    public Sprite expression;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = dialogueParent.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if its the player or key entering the trigger.
        if (other.tag == "Player" && !needKey || other.tag == "Key" && needKey)
        {
            expressionPosition.sprite = expression;
            // Sound effect here
            dialogueText.text = dialogue;
            anim.SetBool("inUse", true); // Animates in the panel
            // Start a coroutine to count down how long the text should be up.
            StartCoroutine(ActiveTime());
           
        }
    }

    // The text resets to blank if the player leaves the trigger without OnTriggerExit.
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !needKey || other.tag == "Key" && needKey)
        {
            dialogueText.text = dialogue;
        }
    }

    // Waits for the specified time then closes the dialogue panel with animation and deactivates trigger.
    private IEnumerator ActiveTime()
    {
        yield return new WaitForSeconds(activeTime);
        anim.SetBool("inUse", false); // Animates out the panel
        // Maybe add something here so the text gets changed to "".
        gameObject.SetActive(false);
    }
}
