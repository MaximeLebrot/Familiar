using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CodePanelActivate : MonoBehaviour
{
    [SerializeField, Tooltip("A reference to the Animator component attached to this game object. Should be inputed manually")]
    private Animator anim;

    [SerializeField, Tooltip("A reference to the Player game object. Should be inputed manually")]
    private GameObject player;
    [SerializeField, Tooltip("A reference to the \"Player\" script. Should be inputed manually")]
    private AbilitySystem.Player playerStats;
    [SerializeField, Tooltip("A reference to the \"Player\" script. Should be inputed manually")]
    private CameraHandler cam;

    [SerializeField]
    private Button[] buttons;

    [SerializeField, Tooltip("A reference to the UICoverPanel game object. Must be inputed manually")]
    private GameObject UICoverPanel;
    [SerializeField, Tooltip("A reference to the UICoverPanel game object. Must be inputed manually")]
    private GameObject ClearedUICoverPanel;
    [Tooltip("Checks whether the code panel is active or not")]
    private bool active;
    [Tooltip("Checks whether the puzzel is done or not")]
    private bool puzzleDone;

    void Start()
    {
        InitializeSequence();
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleDone != true)
        {
            if (playerStats.IsInCodePanelArea != true)
            {
                HideCodePanel();
            }
            else if (playerStats.CanSeeCodePanel == true)
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
            foreach (Button button in buttons)
                button.interactable = false;
            anim.SetBool("Active", false);
            active = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            //Cursor.lockState = CursorLockMode.Locked;
            playerStats.CanZap = true;
            cam.FreezeCamera = false;
        }        
    }
    private void ShowCodePanel()
    {
        foreach (Button button in buttons)
            button.interactable = true;
        anim.SetBool("Active", true);
        active = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerStats.CanZap = false;
        cam.FreezeCamera = true;
    }
    public IEnumerator PuzzleComplete()
    {
        puzzleDone = true;
        ClearedUICoverPanel.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        HideCodePanel();
    }
    private void InitializeSequence()
    {
        InitializePlayerGameObject();
        InitializePlayerScript();
        InitializeAnim();
        InitializeCam();
    }
    private void InitializePlayerGameObject()
    {
        if (player == null)
        {
            Debug.LogWarning("The reference to the Player game object should be inputed manually");
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
                Debug.LogError("Cannot find player game object");
        }
    }
    private void InitializePlayerScript()
    {
        if (playerStats == null)
        {
            Debug.LogWarning("The reference to the \"Player\" script should be inputed manually");
            playerStats = player.GetComponent<AbilitySystem.Player>();
            if (playerStats == null)
                Debug.LogError("Cannot find the \"Player\" script");
        }
    }
    private void InitializeAnim()
    {
        if (anim == null)
        {
            Debug.LogWarning("The reference to the Animator component attached to this game object should be inputed manually");
            anim = GetComponent<Animator>();
            if (anim == null)
                Debug.LogError("Cannot find the Animator component");
        }
    }
    private void InitializeCam()
    {
        if (cam == null)
        {
            Debug.LogWarning("The reference to the Camera Handler component attached to the player game object game object should be inputed manually");
            cam = player.GetComponentInChildren<CameraHandler>();
            if (cam == null)
                Debug.LogError("Cannot find the CameraHandler component");
        }
    }
}
