using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDialoguePanel : MonoBehaviour
{
    public GameObject dialogueParent;
    public Text dialogueText;
    private Animator anim;
    private bool active = false;
    //private string currentText;

    void Start()
    {
        anim = dialogueParent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueText.text == "" && active)
        {
            //dialogueParent.SetActive(false);
            //dialogueText.text = currentText;
            anim.SetBool("inUse", false);
            active = false;
        }
        else if (dialogueText.text != "" && !active)
        {
            //dialogueParent.SetActive(true);
            //currentText = dialogueText.text;
            anim.SetBool("inUse", true);
            active = true;
        }
    }
}
