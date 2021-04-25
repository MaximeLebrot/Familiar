using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextButtonScript : MonoBehaviour, IZappable 
{
    private bool isPowered = false;
    private TextMesh text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnZap()
    {
        isPowered = !isPowered;

        text.color = isPowered ? Color.white : Color.black;
    }
}
