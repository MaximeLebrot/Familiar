using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchLevel : MonoBehaviour
{
    [SerializeField, Tooltip("The prison door game object, must be open to proceed to level 2")]
    private GameObject prisonDoor;
    [SerializeField, Tooltip("The prison door game objects \"Door\" component.")]
    private Door door;
    [SerializeField, Tooltip("A reference to the F2B game components image component. Must be inputted manually")]
    private Image black;
    [SerializeField, Tooltip("")]
    private Animator anim;
    [SerializeField, Tooltip("")]
    private Animator animDoor;

    private static readonly Vector3 level2StartPosition = new Vector3(51.5f, 1.5f, 33.0f);
    private static readonly Vector3 level2StartRotation = new Vector3(0.0f, 270.0f, 0.0f);
    private static readonly float level2StartHealth = 10.0f;

    [Header("Events")]
    [SerializeField, Tooltip("The event in which the player goes to level 2")]
    private UnityEvent SwitchToLevel2;

    private void Awake()
    {
        if (prisonDoor == null)
            Debug.LogError("Input \"Prison door\" manually");
        if (black == null)
            Debug.LogError("Input the F2B component in \"Fin\" manually");
        if (anim == null)
            GetComponent<Animator>();
        if (animDoor == null)
            GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (door.open == true)
        { 
            if (other.CompareTag("Key"))
            {
                animDoor.SetTrigger("exitOpen");
                StartCoroutine(Fading());
            }
        }
    }

    IEnumerator Fading()
    {
        SwitchToLevel2.Invoke();
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);

        InitializeLevel2Stats();
        SceneManager.LoadScene("Level 2");
    }

    private void InitializeLevel2Stats()
    {
        Stats.Instance.Health = level2StartHealth;
        Stats.Instance.Position = level2StartPosition;
        Stats.Instance.Rotation = level2StartRotation;
    }

}
