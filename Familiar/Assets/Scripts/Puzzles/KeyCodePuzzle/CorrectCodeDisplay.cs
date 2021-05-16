using UnityEngine;

public class CorrectCodeDisplay : MonoBehaviour
{
    [SerializeField, Tooltip("A reference to the child display game object. Should be inputed manually")]
    private GameObject display;
    [SerializeField, Tooltip("A reference to the Text mesh attached to this game object. Should be inputed manually")]
    private TextMesh text;
    [SerializeField, Tooltip("The number this key code represents. Must be inputed manually")]
    private int number;

    private void Awake()
    {
        InitializeSequence();
    }

    public void Activate(int inputOrder)
    {
        inputOrder += 1;
        display.SetActive(true);
        text.text = inputOrder.ToString();
        //Debug.Log(number + ": should be inputted " + inputOrder);
    }
    private void InitializeSequence()
    {
        InitializeTextMesh();
        InitializeDisplay();
    }
    private void InitializeTextMesh()
    {
        if (text == null)
        {
            Debug.LogWarning("The reference to this game objects text mesh should be inputed manually");
            text = GetComponent<TextMesh>();
            if (text == null)
                Debug.LogError("Cannot find text mesh");
        }
    }
    private void InitializeDisplay()
    {
        if (display == null)
        {
            Debug.LogWarning("The reference to this game objects child display should be inpited manually");
            display = GetComponentInChildren<Transform>().gameObject;
            if (display == null)
                Debug.LogError("Cannot find child display");
        }
    }

    public int Number
    {
        get => number;
    }
}
