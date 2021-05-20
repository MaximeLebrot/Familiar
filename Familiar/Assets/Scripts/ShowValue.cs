using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowValue : MonoBehaviour
{
    Text valueText;

    // Start is called before the first frame update
    void Start()
    {
        valueText = GetComponent<Text>();
    }

    public void textUpdate(float value)
    {
        valueText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void SensitivityTextUpdate(float value)
    {
        valueText.text = Mathf.Round(value) + "";
    }

    public void DifficultyTextUpdate(float value)
    {
        valueText.text = Mathf.Round(value).ToString();
    }
}
