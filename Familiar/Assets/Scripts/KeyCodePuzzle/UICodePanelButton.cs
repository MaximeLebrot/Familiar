using UnityEngine;
using UnityEngine.UI;

public class UICodePanelButton : MonoBehaviour
{
   
    private Image image;
    private Code code;

    private void Start()
    {
        image = GetComponent<Image>();
        code = GameObject.FindGameObjectWithTag("CodePanel").GetComponent<Code>();
    }

    public void ButtonClicked()
    {
        //if (code.correctCode[code.correctCode.Count-1] == number)
        //{
            //Puzzle complete, set all to green
            //image.color = Color.black;
            //return;
        //}
        //if (code.currentNumber == number)
        //{
            //image.color = Color.green;
        //}
        //else
        //{
            //image.color = Color.red;
        //}

    }
}
