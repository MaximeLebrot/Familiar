using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupItem : MonoBehaviour
{
    protected GameObject player;
    protected AbilitySystem.Player playerStats;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = (AbilitySystem.Player)player.GetComponent("Player");
    }

    public void SetPosition(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }
}
