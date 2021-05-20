using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTrigger : MonoBehaviour
{
    // The mission text is permanently on the screen.
    // Can be delayed.
   
    public Text missionText;
    public string mission;
    public bool needKey;
    public bool delay; //If the mission should wait for dialog
    public float delayTime; // How long before mission should change.
    
    // The mission panels animator
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !needKey || other.CompareTag("Key") && needKey)
        {
            if (delay)
                //StartCoroutine(DelayChange());

            // Add SFX here ?
            anim.SetTrigger("Update"); // Needs better animation ?
            missionText.text = mission; // maybe add typing effect instead?
            gameObject.SetActive(false);
        }
    }

    // Delays the mission text being updated.
    private IEnumerator DelayChange()
    {
        yield return new WaitForSeconds(delayTime);
    }
}
