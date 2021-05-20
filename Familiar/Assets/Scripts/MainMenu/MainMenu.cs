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
        //AudioListener.volume = volumeSlider.value;
    }

    public void SetMouseSensitivity()
    {
        Stats.Instance.MouseSensitivity = mouseSensitivitySlider.value;
        Debug.Log(Stats.Instance.MouseSensitivity);
    }

    public void LoadGame()
    {
        Debug.Log("Loading game...");
    }
}
