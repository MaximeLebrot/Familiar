using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code : MonoBehaviour
{
    public GameObject door; // to open

    private List<KeyCodeCombination> KeyCodeGenerated = new List<KeyCodeCombination>();

    public List<int> correctCode;
    public int currentNumber;
    private int correctCodeIterator;

    private void Awake()
    {
        //KeyCodes.ForEach(Entry => correctCode.Add(Entry, RandomBool()));
        KeyCodeGenerated.AddRange(GetComponentsInChildren<KeyCodeCombination>());
        GenerateCode();
        foreach (KeyCodeCombination key in KeyCodeGenerated)
        {
            if (key.isCorrect)
                correctCode.Add(key.number);
        }
        if (correctCode.Count > 0)
        {
            //RandomizeOrder();
        }
        else if (correctCode.Count == 0)
        {
            correctCode[0] = 1;
        }
        currentNumber = correctCode[0];
    }
    private void Update()
    {
        //SuperSmidigtJagLovar();
    }

    public void GenerateCode()
    {
        foreach (KeyCodeCombination key in KeyCodeGenerated)
        {
            key.isCorrect = RandomBool();
        }
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
        int temp = correctCode[0];
        //correctCode[correctCode.Count] = temp;
        //huller om buller
    }


    private void SuperSmidigtJagLovar()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            TryingInput(1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            TryingInput(2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            TryingInput(3);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            TryingInput(4);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            TryingInput(5);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            TryingInput(6);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            TryingInput(7);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            TryingInput(8);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            TryingInput(9);
    }
    public void TryInput(int input)
    {
        TryingInput(input);
        //Debug.Log("Input " + input);
    }

    private void TryingInput(int input)
    {
        int temp = input - 1;
        if (correctCode[correctCode.Count - 1] == input && correctCode[correctCode.Count - 1] == currentNumber)
        {
            Success();
            return;
        }
        if (KeyCodeGenerated[temp].isCorrect && KeyCodeGenerated[temp].number == currentNumber)
        {
            KeyCodeGenerated[temp].setGreen();
        }
        if (!KeyCodeGenerated[temp].isCorrect || KeyCodeGenerated[temp].number != currentNumber)
        {
            ResetInput();
            return;
        }
        StepNext();
    }

    private void StepNext()
    {
        correctCodeIterator++;
        currentNumber = correctCode[correctCodeIterator]; //correctCode.stepNext;
    }
    private void ResetInput()
    {
        foreach (KeyCodeCombination keycode in KeyCodeGenerated)
        {
            keycode.setRed();
        }
        StartCoroutine(ResetTimer());
        
    }

    private void Success()
    {
        door.SetActive(false);
        foreach (KeyCodeCombination keycode in KeyCodeGenerated)
        {
            keycode.setGreen();
        }
    }

    public IEnumerator ResetTimer()
    {
        RestartCurrentCodeCounter();
        yield return new WaitForSeconds(1.0f);
        foreach (KeyCodeCombination keycode in KeyCodeGenerated)
        {
            keycode.ResetAll();
        }
    }

    public void RestartCurrentCodeCounter()
    {
        Debug.Log("Restartnig current code counter");
        currentNumber = correctCode[0];
        correctCodeIterator = 0;
    }

    public IEnumerator RestartCodeCounterAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        RestartCurrentCodeCounter();
    }
}
