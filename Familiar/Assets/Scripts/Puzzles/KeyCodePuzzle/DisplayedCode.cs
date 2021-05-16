using System.Collections.Generic;
using UnityEngine;

public class DisplayedCode : MonoBehaviour
{
    [SerializeField, Tooltip("A reference to the code panel game object. Should be inputed manually")]
    private GameObject codePanel;
    [SerializeField, Tooltip("A reference to the \"Code\"script attached code panel game object. Should be inputed manually")]
    private Code code;
    [SerializeField, Tooltip("A list of all possible key codes to display. Should be inputed manually")]
    private List<CorrectCodeDisplay> codeDisplay = new List<CorrectCodeDisplay>();

    private void Awake()
    {
        InitializeSequence();
    }
    private void Start()
    {
        foreach (CorrectCodeDisplay display in codeDisplay)
        {
            for (int i = 0; i < code.CorrectCode.Count; i++)
            {
                if (code.CorrectCode[i] == display.Number)
                {
                    display.Activate(i);
                }
            }
        }
    }
    private void InitializeSequence()
    {
        InitializeCodePanelReference();
        InitializeCodeReference();
        InitializeCodeDisplay();
    }
    private void InitializeCodePanelReference()
    {
        if (codePanel == null)
        {
            codePanel = GameObject.FindGameObjectWithTag("CodePanel");
            Debug.LogWarning("Code panel reference should be inputed manually");
            if (codePanel == null)
                Debug.LogError("Cannot find reference to code panel. Are you sure the code panel has tag: \"CodePanel\"");
        }
    }
    private void InitializeCodeReference()
    {
        if (code == null)
        {
            code = codePanel.GetComponent<Code>();
            Debug.LogWarning("Code panel reference should be inputed manually");
            if (codePanel == null)
                Debug.LogError("Cannot find reference to code script");
        }
    }
    private void InitializeCodeDisplay()
    {
        if (codeDisplay.Count == 0)
        {
            codeDisplay.AddRange(GetComponentsInChildren<CorrectCodeDisplay>());
            Debug.LogWarning("Key codes should be inputed manually");
        }
    }
}
