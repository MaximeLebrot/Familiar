using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1PatrolState : Enemy1BaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy Entered Patrol State");

    }

    public override void HandleUpdate()
    {
        Debug.Log("enemy patrolling");
    }

    private void Patrol()
    {

    }
}
