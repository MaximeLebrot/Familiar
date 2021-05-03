using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGetAllImages : MonoBehaviour
{
    public Image[] images;

    public GameObject UICoverPanel;

    void Start()
    {
        images = GetComponentsInChildren<Image>();
    }

    public void SetAllToColor(Color color)
    {
        foreach (Image image in images)
        {
            image.color = color;
        }
    }

    public IEnumerator ResetAll()
    {
        UICoverPanel.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        foreach (Image image in images)
        {
            image.color = Color.white;
        }

        UICoverPanel.SetActive(false);
    }
}
