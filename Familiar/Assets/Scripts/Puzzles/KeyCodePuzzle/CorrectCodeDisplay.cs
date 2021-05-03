using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectCodeDisplay : MonoBehaviour
{
    public GameObject display;
    public int number;
    //public bool correct;
    public void Activate()
    {
        display.SetActive(true);
        //activate med en siffra av vilken som ska vara först i tur
        //nu är koden alltid i ordning
    }
}
