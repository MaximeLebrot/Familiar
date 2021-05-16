using UnityEngine;

public class KeyCodeCombination : MonoBehaviour
{
    [SerializeField, Tooltip("The number corresponding to this key code object. Must be inputed manually")]
    private int number;
    [Tooltip("Checks whether this key code object is part of the correct code")]
    private bool isCorrect;

    [SerializeField, Tooltip("A reference to a game object that signifies this key code is correct. Should be inputed manually")]
    private GameObject correct;
    [SerializeField, Tooltip("A reference to a game object that signifies this key code is incorrect. Should be inputed manually")]
    private GameObject incorrect;

    private void Awake()
    {
        InitializeSequence();
    }
    public void Activate()
    {
        Debug.Log("Activated");
        if (isCorrect == true)
            correct.SetActive(true);
        else
            incorrect.SetActive(true);
    }

    public void setGreen()
    {
        correct.SetActive(true);
        incorrect.SetActive(false);
    }

    public void setRed()
    {
        correct.SetActive(false);
        incorrect.SetActive(true);
    }

    public void ResetAll()
    {
        correct.SetActive(false);
        incorrect.SetActive(false);
    }
    private void InitializeSequence()
    {
        InitializeChildren();
    }
    private void InitializeChildren()
    {
        if (correct == null)
        {
            Debug.LogWarning("\"Correct\" game object should be inputed manually");
            correct = GetComponentInChildren<BoxCollider>().gameObject;
            if (correct == null)
                Debug.LogError("Cannot find \"Correct\" game object");
        }
        if (incorrect == null)
        {
            Debug.LogWarning("\"Incorrect\" game object should be inputed manually");
            correct = GetComponentInChildren<CapsuleCollider>().gameObject;
            if (incorrect == null)
                Debug.LogError("Cannot find \"Incorrect\" game object");
        }
    }

    public int Number
    {
        get => number;
    }
    public bool IsCorrect
    {
        get => isCorrect;
        set => isCorrect = value;
    }
}
