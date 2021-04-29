using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePanelActivate : MonoBehaviour
{
    // this is a little test script to check the animations for the code panel
    public Animator anim;

    // Bool to toggle if code panel is active or not.
    private bool active = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!active)
            {
                anim.SetBool("Active", true);
                active = true;
            }
            else
            {
                anim.SetBool("Active", false);
                active = false;
            }
        }
    }
}
