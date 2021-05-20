using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimations : MonoBehaviour
{
    public Animator anim;
    public GameObject title;

    public void PressToStart()
    {
        anim.SetTrigger("start");
        title.SetActive(true);
    }
}
