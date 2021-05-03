using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayedCode : MonoBehaviour
{
    public GameObject codeGenerator;
    private Code code;

    public List<CorrectCodeDisplay> codeDisplay = new List<CorrectCodeDisplay>();
    private void Awake()
    {
        code = codeGenerator.GetComponent<Code>();
        codeDisplay.AddRange(GetComponentsInChildren<CorrectCodeDisplay>());
    }
    private void Start()
    {
        foreach (CorrectCodeDisplay display in codeDisplay)
        {
            //foreach (int number in code.correctCode)
            //{
            //    if (number == display.number)
            //    {
            //        display.Activate();
            //    }
            //}
            for (int i = 0; i < code.correctCode.Count; i++)
            {
                if (code.correctCode[i] == display.number)
                {
                    display.Activate(i);
                }
            }
        }
    }
}
