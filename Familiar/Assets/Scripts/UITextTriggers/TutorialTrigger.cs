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

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // get animator here
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Sound effect here?
            tutorialText.text = tutorial;
            if (!permanent)
            {
                StartCoroutine(WaitAndDeactivate());
            }
            //gameObject.SetActive(false);
        }
    }

    private IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(activeTime);
        if (tutorialText.text == tutorial)
        {
            // Animate so the text fades out?
            tutorialText.text = "";
            
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && tutorialText.text == tutorial && !permanent)
        {
            // Animate out?
            tutorialText.text = "";
            //gameObject.SetActive(false);
        }
        else if (other.tag == "Player")
        {
            tutorialText.text = tutorial;
        }
    }
}
