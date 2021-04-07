using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    //Enemy1Controller också?
    public float moveSpeed = 10.0f;

    public Player player;
    public State[] states;

    private StateMachine stateMachine;

    protected void Awake()
    {
        Debug.Log("Enemy Awake");
        //find player
        stateMachine = new StateMachine(this, states);
    }

    private void Update()
    {
        stateMachine.HandleUpdate();
    }
}