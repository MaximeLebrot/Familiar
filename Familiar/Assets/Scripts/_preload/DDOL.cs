using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene(LoadingSceneIntegration.otherScene);
#else
        SceneManager.LoadScene("Main Menu");
#endif
    }
}
