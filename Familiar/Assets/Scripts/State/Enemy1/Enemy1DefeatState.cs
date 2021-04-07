using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1DefeatState : Enemy1BaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy Entered Defeat State");

    }

    public override void HandleUpdate()
    {
        Debug.Log("enemy Defeated");
    }

    private void Defeated()
    {

    }
}
