using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowValue : MonoBehaviour
{
    [SerializeField, Tooltip("The text component attached to this game object")]
    Text valueText;
    [SerializeField, Tooltip("The slider component attached to parent of this game object")]
    Slider slider;

    void Start()
    {
        if (valueText == null)
            valueText = GetComponent<Text>();
        if (slider == null)
            slider = GetComponentInParent<Slider>();
    }

    public void VolumeTextUpdate()
    {
        valueText.text = Mathf.RoundToInt(slider.value * 100).ToString() + "%";
    }

    public void MouseSenseTextUpdate()
    {
        valueText.text = Mathf.Round(slider.value).ToString();
    }

    public void DifficultyTextUpdate()
    {

    }
}
