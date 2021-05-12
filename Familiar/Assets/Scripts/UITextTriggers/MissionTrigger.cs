using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTrigger : MonoBehaviour
{
    public Text missionText;
    public string mission;
    public bool needKey;
    // The mission texts animator?
    //public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !needKey || other.tag == "Key" && needKey)
        {
            missionText.text = mission; // maybe add typing effect instead?
            gameObject.SetActive(false);
        }
    }
}
