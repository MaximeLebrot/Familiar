using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The old class for setting all the texts in one script.
// Not in use anymore!!!

public class HelpTextTrigger : MonoBehaviour
{
    // A class that sets an UI text via triggers who use this script.
    // The trigger can be deactivated via bool.
    // Theres also a bool for keeping the text permanent and not resetting it.
    // Scripts been updated to also work for the key!
    public Text helpText;
    public string instructions;
    public float waitTime;
    public bool deactiveTrigger;
    public bool permanentText;

    // A little safety bool so the player doesn't trigger stuff it shouldn't without the key.
    public bool needKey;

    // Typing stuff
    public float typeSpeed;
    public bool useTyping;

    // Start is called before the first frame update
    void Start()
    {
        helpText.text = "";

        // Change once issues are fixed
        useTyping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !needKey)
        {
            if (useTyping)
            {
                StartCoroutine(TypeText());
            }
            else
            {
                helpText.text = instructions;
            }

            if (deactiveTrigger)
            {
                StartCoroutine(WaitAndDestroy());
            }
        }
        else if (other.tag == "Key" && needKey)
        {
            if (useTyping)
            {
               StartCoroutine(TypeText());
            }
            else
            {
                helpText.text = instructions;
            }
        }
    }

    // Added so the letters type out one after another. Has some issues so not used yet
    private IEnumerator TypeText()
    {
        helpText.text = "";
        foreach (char letter in instructions.ToCharArray())
        {
            helpText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !needKey)
        {
            if (helpText.text == instructions && !permanentText)
            {
                helpText.text = "";
            }
        }
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(waitTime);
        if (helpText.text == instructions && !permanentText)
        {
            helpText.text = "";
        }
        gameObject.SetActive(false);
    }
}
