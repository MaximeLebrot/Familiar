using AbilitySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fin : MonoBehaviour
{
    private Player player;

    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && player.GetStones()==2)
        {
            SceneManager.LoadScene("Level 3");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" && player.GetStones() == 2)
        {
            SceneManager.LoadScene("Level 3");
        }
    }

}
