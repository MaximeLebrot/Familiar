using UnityEngine;

public class WizardDialogueTrigger : MonoBehaviour
{
    // The dialogue isn't permanent
    // Gets nulled after a specified time, not affected by the player leaving the trigger.
    public string dialogue;
    public float activeTime;
    public bool needKey;
    public string expressionTrigger; // The expressions trigger

    [SerializeField] private DialogueAudio dialogueAudio;
    [SerializeField] private AudioClip voiceLine;

    public WizardDialogueManager dialogueManager;

    private void OnTriggerEnter(Collider other)
    {
        // Checks if its the player or key entering the trigger.
        if (other.CompareTag("Player") && !needKey || other.CompareTag("Key") && needKey)
        {
            dialogueManager.NewDialogue(dialogue, expressionTrigger, activeTime);
            if (voiceLine != null)
                dialogueAudio.PlayAudioClip(voiceLine);
            gameObject.SetActive(false);
        }
    }
}
