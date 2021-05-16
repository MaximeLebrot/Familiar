using UnityEngine;
using UnityEngine.UI;

public class CodePanelArea : MonoBehaviour
{
    [SerializeField, Tooltip("A reference to the Player game object. Should be inputed manually")]
    private GameObject player;
    [SerializeField, Tooltip("A reference to the \"Player\" script. Should be inputed manually")]
    private AbilitySystem.Player playerStats;

    [SerializeField, Tooltip("A reference Text component attached to the UI. Must be inputed manually")]
    private Text helpText;
    [SerializeField, Tooltip("Instruction given to the player. Must be inputed manually")]
    private string instructions;

    void Start()
    {
        InitializeSequence();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerStats.IsInCodePanelArea = true;

            if (playerStats.CanSeeCodePanel == true)
            {
                helpText.text = instructions;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerStats.IsInCodePanelArea = false;
            helpText.text = "";
        }
    }
    private void InitializeSequence()
    {
        InitializePlayerGameObject();
        InitializePlayerScript();
        InitializeHelpText();
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
    private void InitializeHelpText()
    {
        if (helpText == null)
        {
            Debug.LogError("Help text not set to any value");
        }

        helpText.text = "";
    }
}
