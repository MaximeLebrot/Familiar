using System.Collections;
using UnityEngine;

public class CodePanelActivate : MonoBehaviour
{
    // this is a little test script to check the animations for the code panel
    [SerializeField] private Animator anim;

    private AbilitySystem.Player player;
    private CameraHandler cam;

    [SerializeField] private GameObject UICoverPanel;
    // Bool to toggle if code panel is active or not.
    private bool active = false;
    private bool puzzleDone;

    void Start()
    {
        //initialize()
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilitySystem.Player>();
        cam = player.GetComponentInChildren<CameraHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleDone != true)
        {
            if (player.IsInCodePanelArea != true)
            {
                HideCodePanel();
            }
            else if (player.CanSeeCodePanel == true)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (active != true)
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
    }

    private void HideCodePanel()
    {
        if(PauseMenu.GameIsPaused == true)
        {
            return;
        }
        else
        {
            anim.SetBool("Active", false);
            active = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            //Cursor.lockState = CursorLockMode.Locked;
            cam.freezeCamera = false;
        }        
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
