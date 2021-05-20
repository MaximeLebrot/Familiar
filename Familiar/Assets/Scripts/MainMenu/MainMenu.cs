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
    private Slider volumeSlider;
    [SerializeField, Tooltip("")]
    private Slider mouseSensitivitySlider;

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
        mainMenuUI.SetActive(true);
    }

    public void SetVolume()
    {
        Sound.Instance.Volume = volumeSlider.value;
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
        Vector3 position = new Vector3(-72.5f, 2.5f, -33f);
        float health = 10;

        Stats.Instance.Health = health;
        Stats.Instance.Position = position;
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
        SceneManager.LoadScene(data.sceneName);
    }
}
