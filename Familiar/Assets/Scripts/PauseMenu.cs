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
    private Slider globalVolumeSlider;
    [SerializeField, Tooltip("")]
    private Slider effectsVolumeSlider;
    [SerializeField, Tooltip("")]
    private Slider musicVolumeSlider;
    [SerializeField, Tooltip("")]
    private Slider mouseSensitivitySlider;

    [SerializeField, Tooltip("A reference to the Dialogue Audio Script")]
    private DialogueAudio dialogueAudio;
    [SerializeField, Tooltip("All Instances of AudioHandler in this scene")]
    private AudioHandler[] audioSources;

    [SerializeField]
    private AudioSource narrationSource;
    //[SerializeField]
    //private AudioSource musicSource;

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
        //musicSource = Sound.Instance.Audio;
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

        narrationSource.UnPause();
        //musicSource.UnPause();

        Time.timeScale = 1;
        GameIsPaused = false;

        playerHandler.GetComponent<Controller>().enabled = true;
        playerHandler.GetComponent<ShootingScript>().enabled = true;
        playerHandler.GetComponent<AbilitySystem.Player>().enabled = true;
        camHandler.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        ApplyValueChange();
    }

    void Pause()
    {
        UpdateSettingsToCorrectValue();
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        narrationSource.Pause();
        //musicSource.Pause();

        Time.timeScale = 0;
        GameIsPaused = true;

        playerHandler.GetComponent<Controller>().enabled = false;
        playerHandler.GetComponent<ShootingScript>().enabled = false;
        playerHandler.GetComponent<AbilitySystem.Player>().enabled = false;
        camHandler.enabled = false;
    }

    void UpdateSettingsToCorrectValue()
    {
        globalVolumeSlider.value = Sound.Instance.GlobalVolume;
        effectsVolumeSlider.value = Sound.Instance.EffectsVolumeRaw;
        musicVolumeSlider.value = Sound.Instance.MusicVolumeRaw;
        mouseSensitivitySlider.value = Stats.Instance.MouseSensitivity;
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

    public void SetGlobalVolume()
    {
        Sound.Instance.GlobalVolume = globalVolumeSlider.value;
        Sound.Instance.UpdateMusicVolume();
        dialogueAudio.UpdateVolume();
    }

    public void SetEffectsVolume()
    {
        Sound.Instance.EffectsVolume = effectsVolumeSlider.value;
        //UpdateAllAudioHandlersVolume(); //could be called here but would not be optimal
        dialogueAudio.UpdateVolume();
    }

    public void SetMusicVolume()
    {
        Sound.Instance.MusicVolume = musicVolumeSlider.value;
        Sound.Instance.UpdateMusicVolume();
    }

    public void SetMouseSensitivity()
    {
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
        camHandler.MouseSensitivity = Stats.Instance.MouseSensitivity;
        UpdateAllAudioHandlersVolume();
    }

    void UpdateAllAudioHandlersVolume()
    {
        foreach (AudioHandler audio in audioSources)
        {
            audio.UpdateVolume();
        }
    }
}
