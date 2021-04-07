using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1AttackState : Enemy1BaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy Entered Attack State");

    }

    public override void HandleUpdate()
    {
        Debug.Log("enemy attacking");
    }

    private void Attack()
    {

    }
}
