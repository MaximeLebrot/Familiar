using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICodePanelButton : MonoBehaviour
{
    private int number;

    private Image image;
    private Code code;
    private Text text;

    public CodePanelActivate codePanelActivate;
    public UIGetAllImages allImages;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        number = int.Parse(text.text);
        image = GetComponent<Image>();
        code = GameObject.FindGameObjectWithTag("CodePanel").GetComponent<Code>();
        allImages = GetComponentInParent<UIGetAllImages>();
        codePanelActivate = GetComponentInParent<CodePanelActivate>();
    }

    public void ButtonClicked()
    {
        if (code.correctCode[code.correctCode.Count - 1] == number && number == code.currentNumber)
        {
            allImages.SetAllToColor(Color.green);
            //Deactivate ui? deactivate buttons iaf .. allButtons?
            StartCoroutine(codePanelActivate.PuzzleComplete());
            return;
        }
        if (code.currentNumber == number)
        {
            image.color = Color.green;
        }
        else
        {
            allImages.SetAllToColor(Color.red);
            //set "incorrect code panel" to active?
            code.RestartCurrentCodeCounter();
            StartCoroutine(allImages.ResetAll());
        }

    }
}
