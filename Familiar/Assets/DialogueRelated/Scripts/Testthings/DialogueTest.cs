using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    // A script to test starting a dialogue.
    public DialogueTrigger dTrigger;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("the triggers been seen");
            dTrigger.TriggerDialogue();
        }
    }


}
