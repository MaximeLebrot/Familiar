using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1IdleState : Enemy1BaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy Entered Idle State");

    }

    public override void HandleUpdate()
    {
        Debug.Log("enemy idle");
    }

    private void Idle()
    {

    }
}
