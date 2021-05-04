using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpTextTrigger : MonoBehaviour
{
    // A class that sets an UI text. Can be destroyed, if needed.
    public Text helpText;
    public string instructions;
    public float waitTime;
    public bool deactiveTrigger;
    public bool permanentText;

    // Start is called before the first frame update
    void Start()
    {
        helpText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            helpText.text = instructions;
            if (deactiveTrigger)
            {
                StartCoroutine(WaitAndDestroy());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
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
