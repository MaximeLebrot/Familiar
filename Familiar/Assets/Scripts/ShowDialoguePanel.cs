using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDialoguePanel : MonoBehaviour
{
    public GameObject dialogueParent;
    public Text dialogueText;

    // Update is called once per frame
    void Update()
    {
        if (dialogueText.text == "")
        {
            dialogueParent.SetActive(false);
        }
        else
        {
            dialogueParent.SetActive(true);
        }
    }
}
