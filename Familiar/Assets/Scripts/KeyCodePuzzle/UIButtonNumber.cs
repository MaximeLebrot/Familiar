using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonNumber : MonoBehaviour
{
    private int number;
    private Text text;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        number = int.Parse(text.text);
    }
}
