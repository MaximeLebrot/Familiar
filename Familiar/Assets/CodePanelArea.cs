using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePanelArea : MonoBehaviour
{
    private AbilitySystem.Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilitySystem.Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.isInCodePanelArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.isInCodePanelArea = false;
        }
    }
}
