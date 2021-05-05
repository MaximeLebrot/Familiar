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
            StartCoroutine(codePanelActivate.PuzzleComplete());
            return;
        }
        if (code.currentNumber == number)
        {
            image.color = Color.green;
        }
        else
        {
            Debug.Log("Wrong!");
            allImages.SetAllToColor(Color.red);
            StartCoroutine(code.RestartCodeCounterAfterDelay());
            StartCoroutine(allImages.ResetAll());
        }

    }
}
