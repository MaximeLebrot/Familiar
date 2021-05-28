using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField, Tooltip("")]
    private GameObject pauseMenuUI;
    [SerializeField, Tooltip("")]
    private GameObject optionsMenuUI;

    [SerializeField, Tooltip("")]
    private Slider volumeSlider;
    [SerializeField, Tooltip("")]
    private Slider mouseSensitivitySlider;

    [SerializeField, Tooltip("Should be inputed manually")]
    private GameObject playerHandler;
    [SerializeField, Tooltip("Should be inputed manually")]
    private GameObject cam;
    [SerializeField, Tooltip("Should be inputed manually")]
    private CameraHandler camHandler;

    void Start()
    {
        if (playerHandler == null)
            playerHandler = GameObject.FindGameObjectWithTag("Player");
        if (cam == null)
            cam = Camera.main.gameObject;
        if (camHandler == null)
            camHandler = cam.GetComponent<CameraHandler>();
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
        camHandler.enabled = true;

        ApplyValueChange();
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
        camHandler.enabled = false;
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
        Sound.Instance.GlobalVolume = volumeSlider.value;
    }

    public void SetMouseSensitivity()
    {
        //Det här körs inte på on value change på slidern. vrf??
        Stats.Instance.MouseSensitivity = mouseSensitivitySlider.value;
        Debug.Log(Stats.Instance.MouseSensitivity);
    }

    public void SaveTheGame()
    {
        SaveGame.SavePlayer(playerHandler.GetComponent<AbilitySystem.Player>());
        Debug.Log("Saving game...");
    }

    void ApplyValueChange()
    {
        //Stats.Instance.MouseSensitivity = mouseSensitivitySlider.value;
        //camHandler.MouseSensitivity = Stats.Instance.MouseSensitivity;
        //sound.volume = Sound.Instance.Volume;
    }
}
