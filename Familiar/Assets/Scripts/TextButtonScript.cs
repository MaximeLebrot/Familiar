using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextButtonScript : MonoBehaviour, IZappable 
{
    //private bool isPowered = false;
    private TextMesh text;
    private KeyCodeCombination keypad;

    public bool IsZapped
    {
        get;
        set;
    }

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMesh>();
        keypad = GetComponent<KeyCodeCombination>();
    }

    public void OnZap()
    {
        //isPowered = !isPowered;
        //text.color = isPowered ? Color.white : Color.black;
        //keypad.Activate();
    }
}
