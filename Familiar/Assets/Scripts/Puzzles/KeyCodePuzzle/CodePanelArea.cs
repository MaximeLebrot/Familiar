using UnityEngine;
using UnityEngine.UI;

public class CodePanelArea : MonoBehaviour
{
    private AbilitySystem.Player player;

    // A text telling the player which button to press to open the code panel UI
    public Text helpText;
    public string instructions;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilitySystem.Player>();
        helpText.text = "";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.isInCodePanelArea = true;

            if (player.canSeeCodePanel == true)
            {
                helpText.text = instructions;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.isInCodePanelArea = false;
            helpText.text = "";
        }
    }
}
