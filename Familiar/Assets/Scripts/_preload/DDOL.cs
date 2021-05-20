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
        SceneManager.LoadScene(LoadingSceneIntegration.otherScene);
    }
}
