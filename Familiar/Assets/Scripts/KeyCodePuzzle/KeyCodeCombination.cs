using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCodeCombination : MonoBehaviour
{
    public int number;
    public bool isCorrect;

    public GameObject correct;
    public GameObject incorrect;


    public void Activate()
    {
        Debug.Log("Activated");
        if (isCorrect)
        {
            correct.SetActive(true);
        }
        else
        {
            incorrect.SetActive(true);
        }
        //if true sätt grön färg 
        //else röd
    }

    public void setGreen()
    {
        correct.SetActive(true);
        incorrect.SetActive(false);
    }

    public void ResetAll()
    {
        correct.SetActive(false);
        incorrect.SetActive(false);
    }
}
