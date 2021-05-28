using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardDialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Animator anim; // Dialogue panels animator
    public Animator expressionAnim; // Expression images animator

    private bool isActive;

    public void NewDialogue(string dialogue, string expression, float activeTime)
    {
        if (isActive)
        {
            anim.SetTrigger("update");
            expressionAnim.SetTrigger("update");
        }
        expressionAnim.SetTrigger(expression);
        // Sound effect here ?
        dialogueText.text = dialogue;
        if (!isActive)
        {
            anim.SetBool("inUse", true); // Animates in the panel
            isActive = true;
        }

        StopAllCoroutines();
        // Start a coroutine to count down how long the text should be up.
        StartCoroutine(ActiveTime(activeTime));
        
    }

    private IEnumerator ActiveTime(float activeTime)
    {
        yield return new WaitForSeconds(activeTime);
        anim.SetBool("inUse", false); // Animates out the panel
        isActive = false;
    }
}
