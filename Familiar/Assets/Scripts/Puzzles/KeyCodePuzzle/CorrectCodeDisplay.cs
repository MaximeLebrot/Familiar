using UnityEngine;
using UnityEngine.UI;

public class CorrectCodeDisplay : MonoBehaviour
{
    [SerializeField, Tooltip("A reference to the child display game object. Should be inputed manually")]
    private GameObject display;
    [SerializeField]
    private GameObject textObject;
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
        textObject.GetComponent<Text>().text = inputOrder.ToString();
        //text.text = inputOrder.ToString();
        //Debug.Log(number + ": should be inputted " + inputOrder);
    }
    private void InitializeSequence()
    {
        //InitializeTextMesh();
        InitializeDisplay();
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
