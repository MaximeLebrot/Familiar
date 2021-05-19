using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextBlank : MonoBehaviour
{
    // Used to set all the UI texts to blank before the game starts.
    // Maybe not necessary?

    public Text tutorialText;
    public Text missionText;
    public Text dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        tutorialText.text = "";
        missionText.text = "";
        dialogueText.text = "";
    }
}
