using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            dialogueManager.DisplayNextSentence();
        }
    }
}
