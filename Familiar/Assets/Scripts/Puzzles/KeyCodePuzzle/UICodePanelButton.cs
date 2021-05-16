using System;
using UnityEngine;
using UnityEngine.UI;

public class UICodePanelButton : MonoBehaviour
{
    [Tooltip("The number this button represents")]
    private int number;

    [SerializeField, Tooltip("A reference to the Image component attached to this game object. Should be inputed manually")]
    private Image image;
    [SerializeField, Tooltip("A reference to the code panel game object. Should be inputed manually")]
    private GameObject codePanel;
    [SerializeField, Tooltip("A reference to the \"Code\" script attached to the code panel game object. Should be inputed manually")]
    private Code code;
    [SerializeField, Tooltip("A reference to the Text component attached to this game object. Should be inputed manually")]
    private Text text;

    [SerializeField, Tooltip("A reference to the parent script \"CodePanelActivate\". Should be inputed manually")]
    private CodePanelActivate codePanelActivate;
    [SerializeField, Tooltip("A reference to the parent script \"UIGetAllImages\". Should be inputed manually")]
    private UIGetAllImages allImages;

    private void Start()
    {
        InitializeSequence();
    }

    public void ButtonClicked()
    {
        if (code.CorrectCode[code.CorrectCode.Count - 1] == number && number == code.CurrentNumber)
        {
            allImages.SetAllToColor(Color.green);
            StartCoroutine(codePanelActivate.PuzzleComplete());
            return;
        }
        if (code.CurrentNumber == number)
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
    private void InitializeSequence()
    {
        InitializeText();
        InitializeNumber();
        InitializeImage();
        InitializeCodePanel();
        InitializeCode();
        InitializeAllImages();
        InitializeCodePanelActivate();
    }
    private void InitializeText()
    {
        if (text == null)
        {
            Debug.LogWarning("The reference to the child Text component should be inputed manually");
            text = GetComponentInChildren<Text>();
            if (text == null)
                Debug.LogError("Cannot find text component");
        }
    }
    private void InitializeNumber()
    {
        number = int.Parse(text.text);
    }
    private void InitializeImage()
    {
        if (image == null)
        {
            Debug.LogWarning("The reference to this game objects image component should be inputed manually");
            image = GetComponent<Image>();
            if (image == null)
                Debug.LogError("Cannot find image component");
        }
    }
    private void InitializeCodePanel()
    {
        if (codePanel == null)
        {
            Debug.LogWarning("The reference to code panel game object should be inputed manually");
            codePanel = GameObject.FindGameObjectWithTag("CodePanel");
            if (codePanel == null)
                Debug.LogError("Cannot find code panel");
        }
    }
    private void InitializeCode()
    {
        if (code == null)
        {
            Debug.LogWarning("The reference to the \"Code\"script attached to the code panel game object should be inputed manually");
            code = codePanel.GetComponent<Code>();
            if (code == null)
                Debug.LogError("Cannot find the \"Code\"script attached to the code panel game object");
        }
    }
    private void InitializeAllImages()
    {
        if (allImages == null)
        {
            Debug.LogWarning("The reference to the \"UIGetAllImages\"script attached to a parent game object should be inputed manually");
            allImages = GetComponentInParent<UIGetAllImages>();
            if (allImages == null)
                Debug.LogError("Cannot find the \"UIGetAllImages\"script attached to a parent game object");
        }
    }
    private void InitializeCodePanelActivate()
    {
        if (codePanelActivate == null)
        {
            Debug.LogWarning("The reference to the \"CodePanelActivate\"script attached to a parent game object should be inputed manually");
            codePanelActivate = GetComponentInParent<CodePanelActivate>();
            if (codePanelActivate == null)
                Debug.LogError("Cannot find the \"CodePanelActivate\"script attached to a parent game object");
        }
    }
}
