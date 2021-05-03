using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePanelActivate : MonoBehaviour
{
    // this is a little test script to check the animations for the code panel
    public Animator anim;

    private AbilitySystem.Player player;
    public CameraHandler cam;

    public GameObject UICoverPanel;
    // Bool to toggle if code panel is active or not.
    private bool active = false;
    private bool puzzleDone;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilitySystem.Player>();
        cam = player.GetComponentInChildren<CameraHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!puzzleDone)
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
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    if (!active)
        //    {
        //        ShowCodePanel();
        //    }
        //    else
        //    {
        //        HideCodePanel();
        //    }
        //}
    }

        private void HideCodePanel()
    {
        anim.SetBool("Active", false);
        active = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.lockState = CursorLockMode.Locked;
        cam.freezeCamera = false;
    }
    private void ShowCodePanel()
    {
        anim.SetBool("Active", true);
        active = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        cam.freezeCamera = true;
    }
    public IEnumerator PuzzleComplete()
    {
        puzzleDone = true;
        UICoverPanel.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        HideCodePanel();
    }
}
