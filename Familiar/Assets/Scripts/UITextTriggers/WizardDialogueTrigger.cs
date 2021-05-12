using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardDialogueTrigger : MonoBehaviour
{
    public GameObject dialogueParent;
    public Text dialogueText;
    public string dialogue;
    public float activeTime;
    public bool needKey;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = dialogueParent.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !needKey || other.tag == "Key" && needKey)
        {
            // Sound effect here
            dialogueText.text = dialogue;
            anim.SetBool("inUse", true);
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

    private IEnumerator ActiveTime()
    {
        yield return new WaitForSeconds(activeTime);
        anim.SetBool("inUse", false);
        // Maybe add something here so the text gets changed to "".
        gameObject.SetActive(false);
    }
}
