using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public Controller playerController;
    public State[] states;

    private StateMachine stateMachine;

    protected void Awake()
    {
        Debug.Log("Player Awake");
        playerController = GetComponent<Controller>();
        stateMachine = new StateMachine(this, states);
    }

    private void Update()
    {
        stateMachine.HandleUpdate();
    }
}
