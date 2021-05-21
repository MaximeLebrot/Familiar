using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code : MonoBehaviour
{
    [SerializeField, Tooltip("A reference to the door game object that should open upon puzzle completion. Should be inputed manually")] 
    private GameObject door;
    [Tooltip("A list of integers that represent the correct code in the correct order")]
    private List<int> correctCode;
    [Tooltip("The current number that needs to be inputed as part of the correct code")]
    private int currentNumber;

    [SerializeField, Tooltip("A list of possible key codes. Should be inputed manually")]
    private List<KeyCodeCombination> keyCodeGenerated = new List<KeyCodeCombination>();
    [Tooltip("An iterator of the correct code")]
    private int correctCodeIterator;
    Animator animator;

    private void Awake()
    {
        InitializeSequence();

        GenerateCorrectCode();

        animator = door.GetComponent<Animator>(); //vild kod
    }

    public void GenerateCorrectCode()
    {
        foreach (KeyCodeCombination key in keyCodeGenerated)
        {
            key.IsCorrect = RandomBool();
            if (key.IsCorrect)
                correctCode.Add(key.Number);
        }

        if (correctCode.Count > 0)
            RandomizeOrder();
        else
            correctCode[0] = 1;

        currentNumber = correctCode[0];
    }

    private bool RandomBool()
    {
        float random = Random.Range(0.0f, 1.0f);
        if (random >= 0.5f)
            return true;
        else
            return false;
    }
    
    private void RandomizeOrder()
    {
        correctCode.Reverse();
        if (correctCode.Count >= 3)
        {
            int temp = correctCode[0];
            correctCode[0] = correctCode[correctCode.Count - 2];
            correctCode[correctCode.Count - 2] = temp;
        }
        if (correctCode.Count >= 4)
        {
            int temp = correctCode[3];
            correctCode[3] = correctCode[correctCode.Count - 3];
            correctCode[correctCode.Count - 3] = temp;
        }
        //huller om buller, finns säker ett riktigt sätt att göra på
    }

    //Called on by the buttons connected to the UI
    public void TryInput(int input)
    {
        TryingInput(input);
    }

    private void TryingInput(int input)
    {
        int temp = input - 1;
        if (correctCode[correctCode.Count - 1] == input && correctCode[correctCode.Count - 1] == currentNumber)
        {
            Success();
            return;
        }
        if (keyCodeGenerated[temp].IsCorrect && keyCodeGenerated[temp].Number == currentNumber)
        {
            keyCodeGenerated[temp].setGreen();
        }
        if (!keyCodeGenerated[temp].IsCorrect || keyCodeGenerated[temp].Number != currentNumber)
        {
            ResetInput();
            return;
        }
        StepNext();
    }

    private void StepNext()
    {
        correctCodeIterator++;
        currentNumber = correctCode[correctCodeIterator];
    }

    private void ResetInput()
    {
        foreach (KeyCodeCombination keycode in keyCodeGenerated)
        {
            keycode.setRed();
        }
        StartCoroutine(ResetTimer());
        
    }

    private void Success()
    {
        animator.SetBool("open", true); //vild kod
        //door.SetActive(false);
        foreach (KeyCodeCombination keycode in keyCodeGenerated)
        {
            keycode.setGreen();
        }
    }

    public IEnumerator ResetTimer()
    {
        RestartCurrentCodeCounter();
        yield return new WaitForSeconds(1.0f);
        foreach (KeyCodeCombination keycode in keyCodeGenerated)
        {
            keycode.ResetAll();
        }
    }

    public void RestartCurrentCodeCounter()
    {
        currentNumber = correctCode[0];
        correctCodeIterator = 0;
    }

    public IEnumerator RestartCodeCounterAfterDelay()
    {
        yield return new WaitForSeconds(0.2f); //plåster för någon update ordning som skakar slottet
        RestartCurrentCodeCounter();
    }

    private void InitializeSequence()
    {
        InitializeKeyCodeList();
        InitializeCorrectCode();
    }
    private void InitializeKeyCodeList()
    {
        if (keyCodeGenerated.Count == 0)
        {
            Debug.LogWarning("Key Code values should be inputed manually");
            keyCodeGenerated.AddRange(GetComponentsInChildren<KeyCodeCombination>());
            if (keyCodeGenerated == null)
                Debug.LogError("Cannot find key codes. Are there children objects with key codes attached to them?");
        }
    }
    private void InitializeCorrectCode()
    {
        correctCode = new List<int>();
    }

    public List<int> CorrectCode
    {
        get => correctCode;
    }
    public int CurrentNumber
    {
        get => currentNumber;
    }
}
