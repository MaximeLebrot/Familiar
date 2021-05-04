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
            StartCoroutine(WaitAndDestroy());
        }
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(waitTime);
        helpText.text = "";
        gameObject.SetActive(false);
    }
}
