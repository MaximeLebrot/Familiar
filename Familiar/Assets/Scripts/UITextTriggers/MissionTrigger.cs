using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTrigger : MonoBehaviour
{
    // The mission text is permanently on the screen.
    // Maybe add a delay to the missiontext?
   
    public Text missionText;
    public string mission;
    public bool needKey;
    public float typeSpeed; // needs to be around 0.025?
    
    // The mission texts animator
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !needKey || other.tag == "Key" && needKey)
        {
            // Needs better animation or something to give better feedback on when text gets updated.
            anim.SetTrigger("Update");
            missionText.text = mission; // maybe add typing effect instead?
            //StartCoroutine(TypeText());

            // Add SFX here ?
        }
    }

    // Not used as of now.
    // Becomes a problem when player walks out of the trigger?
    // Or when the typing speed is to low, the sentence gets cut of for some reason?

    private IEnumerator TypeText()
    {
        missionText.text = "";
        foreach (char letter in mission.ToCharArray())
        {
            missionText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        gameObject.SetActive(false);
    }
}
