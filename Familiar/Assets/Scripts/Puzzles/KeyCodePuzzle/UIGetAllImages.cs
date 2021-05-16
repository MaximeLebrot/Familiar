using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIGetAllImages : MonoBehaviour
{
    [SerializeField, Tooltip("A reference to all child images. Should be inputed manually")]
    private Image[] images;

    [SerializeField, Tooltip("A reference to the UICoverPanel game object. Must be inputed manually")]
    private GameObject UICoverPanel;

    void Start()
    {
        InitializeSequence();
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
    private void InitializeSequence()
    {
        InitializeImages();
        InitializeUICoverPanel();
    }
    private void InitializeUICoverPanel()
    {
        if (UICoverPanel == null)
        {
            Debug.LogError("UI cover panel must be inputed manually");
        }
    }
    private void InitializeImages()
    {
        if (images.Length == 0)
        {
            Debug.LogWarning("UI child images should be inputed manually");
            images = GetComponentsInChildren<Image>();
            if (images == null)
                Debug.LogError("Cannot find UI child images");
        }
    }
}
