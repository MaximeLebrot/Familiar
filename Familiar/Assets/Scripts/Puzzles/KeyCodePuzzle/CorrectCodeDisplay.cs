using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectCodeDisplay : MonoBehaviour
{
    public GameObject display;
    public TextMesh text;
    public int number;

    private void Awake()
    {
        text = GetComponent<TextMesh>();
    }
    //public bool correct;
    public void Activate(int inputOrder)
    {
        inputOrder += 1;
        display.SetActive(true);
        text.text = inputOrder.ToString();
        Debug.Log(number + ": should be inputted " + inputOrder);
    }
}
