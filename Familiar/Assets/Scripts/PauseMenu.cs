using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
        
    public Slider volumeSlider;
    public Slider sensitivitySlider;

    private GameObject playerHandler;
    private GameObject camHandler;

    void Start()
    {
        playerHandler = GameObject.Find("Player");
        camHandler = GameObject.Find("Main Camera");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);

        Time.timeScale = 1;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        playerHandler.GetComponent<Controller>().enabled = true;
        playerHandler.GetComponent<ShootingScript>().enabled = true;
        playerHandler.GetComponent<AbilitySystem.Player>().enabled = true;
        camHandler.GetComponent<CameraHandler>().enabled = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;
        GameIsPaused = true;
        
        playerHandler.GetComponent<Controller>().enabled = false;
        playerHandler.GetComponent<ShootingScript>().enabled = false;
        playerHandler.GetComponent<AbilitySystem.Player>().enabled = false;
        camHandler.GetComponent<CameraHandler>().enabled = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Options()
    {        
        optionsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void GoBack()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void SetSensitivity()
    {
        camHandler.GetComponent<CameraHandler>().mouseSensitivity = sensitivitySlider.value * 10;
    }

    public void SaveGame()
    {
        Debug.Log("Saving game...");
    }
}
