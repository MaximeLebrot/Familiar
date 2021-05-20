using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchLevel : MonoBehaviour
{
    public GameObject prisonDoor;
    public Image black;
    public Animator anim;
    
    private Vector3 level2StartPosition = new Vector3(51.5f, 1.5f, 33f);
    private float level2StartHealth = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            if (!prisonDoor.activeInHierarchy)
                StartCoroutine(Fading());
        }
    }

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);

        InitializeLevel2Stats();
        SceneManager.LoadScene("Level 2");
    }
    private void InitializeLevel2Stats()
    {
        Stats.Instance.Health = level2StartHealth;
        Stats.Instance.Position = level2StartPosition;
    }

}
