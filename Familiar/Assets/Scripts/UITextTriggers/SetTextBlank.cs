using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextBlank : MonoBehaviour
{
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
