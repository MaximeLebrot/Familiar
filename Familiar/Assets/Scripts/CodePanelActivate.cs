using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePanelActivate : MonoBehaviour
{
    // this is a little test script to check the animations for the code panel
    public Animator anim;

    private AbilitySystem.Player player;

    // Bool to toggle if code panel is active or not.
    private bool active = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilitySystem.Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isInCodePanelArea)
        {
            HideCodePanel();
        }
        else if (player.canSeeCodePanel)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!active)
                {
                    ShowCodePanel();
                }
                else
                {
                    HideCodePanel();
                }
            }
        }
    }

    private void HideCodePanel()
    {
        anim.SetBool("Active", false);
        active = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void ShowCodePanel()
    {
        anim.SetBool("Active", true);
        active = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
