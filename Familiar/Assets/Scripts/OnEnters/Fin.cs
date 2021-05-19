using AbilitySystem;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fin : MonoBehaviour
{
    [Tooltip("A reference to the \"Player\" script on the player game object. Should be inputed manually")]
    [SerializeField] private Player player;
    [Tooltip("A reference to the F2B game component. Should be inputed manually")]
    [SerializeField] private Image black;
    [Tooltip("The animator componen attached to this game object. Should be inputed manually")]
    [SerializeField] private Animator anim;

    protected void Awake()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (anim == null)
            anim = GetComponent<Animator>();
        //if (black == null)
            //find image
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player.StoneCounter >= 6)
        {
            StartCoroutine(Fading());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && player.StoneCounter >= 6)
        {
            StartCoroutine(Fading());
        }
    }

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene("Level 3");
    }

}
