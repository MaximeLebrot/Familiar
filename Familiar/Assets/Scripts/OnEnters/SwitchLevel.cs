using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchLevel : MonoBehaviour
{
    public Image black;
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Moveable")
        {
            StartCoroutine(Fading());
        }
    }

   IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene("Level 2");
    }

}
