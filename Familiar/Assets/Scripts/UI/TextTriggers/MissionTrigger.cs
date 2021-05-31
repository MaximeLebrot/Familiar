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
    
    // The mission panels animator
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !needKey || other.CompareTag("Key") && needKey)
        {
            anim.SetTrigger("Update"); // Needs better animation ?
                                       //if (delay)
                                       //StartCoroutine(DelayChange());

            missionText.text = mission; // maybe add typing effect instead?
            gameObject.SetActive(false);
            // Add SFX here ?
        }
    }
}
