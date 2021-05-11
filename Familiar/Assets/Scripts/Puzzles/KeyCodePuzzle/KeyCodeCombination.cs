using UnityEngine;

public class KeyCodeCombination : MonoBehaviour
{
    public int number;
    public bool isCorrect;

    [SerializeField] private GameObject correct;
    [SerializeField] private GameObject incorrect;


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
}
