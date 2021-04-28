using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code from Brackeys
// Videolink: https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
