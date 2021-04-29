using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    private GameObject playerHandler;
    private GameObject camHandler;

    void Start()
    {
        playerHandler = GameObject.Find("Player");
        camHandler = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
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
        Time.timeScale = 1;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        AudioListener.volume = 1;

        playerHandler.GetComponent<Controller>().enabled = true;
        playerHandler.GetComponent<ShootingScript>().enabled = true;
        camHandler.GetComponent<CameraHandler>().enabled = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AudioListener.volume = 0;

        playerHandler.GetComponent<Controller>().enabled = false;
        playerHandler.GetComponent<ShootingScript>().enabled = false;
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
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Options()
    {
        Debug.Log("Loading options...");
        optionsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void GoBack()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
