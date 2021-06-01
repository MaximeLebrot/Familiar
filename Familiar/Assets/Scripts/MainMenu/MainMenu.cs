using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField, Tooltip("")]
    private GameObject mainMenuUI;
    [SerializeField, Tooltip("")]
    private GameObject optionsMenuUI;
    [SerializeField, Tooltip("")]
    private GameObject controlsMenuUI;
    [SerializeField, Tooltip("")]
    private Slider globalVolumeSlider;
    [SerializeField, Tooltip("")]
    private Slider effectsVolumeSlider;
    [SerializeField, Tooltip("")]
    private Slider musicVolumeSlider;
    [SerializeField, Tooltip("")]
    private Slider mouseSensitivitySlider;
    [SerializeField, Tooltip("")]
    private Dropdown difficultyDropdown;

    private Vector3 level1SpawnPosition = new Vector3(-72.5f, 2.5f, -33f);
    private readonly float level1Health = 10f;

    public void StartGame()
    {
        InitializeStartStats();
        SceneManager.LoadScene("Level 1");
    }

    public void Options()
    {
        //Debug.Log("Loading options...");
        optionsMenuUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void BackToMenu()
    {
        optionsMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void SetDifficultyLevel()
    {
        Stats.Instance.Difficulty = difficultyDropdown.value + 1;
    }

    public void SetGlobalVolume()
    {
        Sound.Instance.GlobalVolume = globalVolumeSlider.value;
        Sound.Instance.UpdateMusicVolume();
    }

    public void SetEffectsVolume()
    {
        Sound.Instance.EffectsVolume = effectsVolumeSlider.value;
    }

    public void SetMusicVolume()
    {
        Sound.Instance.MusicVolume = musicVolumeSlider.value;
        Sound.Instance.UpdateMusicVolume();
    }

    public void SetMouseSensitivity()
    {
        Stats.Instance.MouseSensitivity = mouseSensitivitySlider.value;
    }

    public void LoadGame()
    {
        LoadVariables();
        Debug.Log("Loading game...");
    }

    private void InitializeStartStats()
    {
        Stats.Instance.Health = level1Health;
        Stats.Instance.Position = level1SpawnPosition;
    }

    private void LoadVariables()
    {
        SaveData data = SaveGame.LoadPlayer();

        Stats.Instance.Health = data.health;

        Vector3 position = new Vector3();
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        Stats.Instance.Position = position;

        Vector3 rotation = new Vector3();
        rotation.x = data.rotation[0];
        rotation.y = data.rotation[1];
        rotation.z = data.rotation[2];
        Stats.Instance.Rotation = rotation;

        SceneManager.LoadScene(data.sceneName);
    }

    public void ShowControls()
    {
        controlsMenuUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }
}
