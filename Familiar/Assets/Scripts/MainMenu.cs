using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;

    public Slider volumeSlider;

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Options()
    {
        Debug.Log("Loading options...");
        optionsMenuUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void BackToMenu()
    {
        optionsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
