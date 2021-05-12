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

    // Start is called before the first frame update
    void Start()
    {
        missionText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !needKey || other.tag == "Key" && needKey)
        {
            missionText.text = mission;
            gameObject.SetActive(false);
        }
    }
}
