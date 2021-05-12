using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    public Text tutorialText;
    public string tutorial;
    public float activeTime;
    public bool permanent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tutorialText.text = "";
            // Sound effect here?
            tutorialText.text = tutorial;
            if (!permanent)
            {
                StartCoroutine(WaitAndDeactivate());
            }
        }
    }

    private IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(activeTime);
        if (tutorialText.text == tutorial)
        {
            tutorialText.text = "";
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && tutorialText.text == tutorial)
        {
            tutorialText.text = "";
        }
    }
}
