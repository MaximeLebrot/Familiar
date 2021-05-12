using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTrigger : MonoBehaviour
{
    public Text missionText;
    public string mission;
    public bool needKey;
    public float typeSpeed;
    // The mission texts animator?
    //public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !needKey || other.tag == "Key" && needKey)
        {
            missionText.text = mission; // maybe add typing effect instead?
            //StartCoroutine(TypeText());
        }
    }

    // Could maybe be a problem if player walks out of the trigger?
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
