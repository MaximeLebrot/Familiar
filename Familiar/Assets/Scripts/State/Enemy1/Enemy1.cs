using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    //Enemy1Controller också?
    public float moveSpeed = 10.0f;
    public bool zapped;

    public Controller playerController;
    public State[] states;
    public Transform transform1;
    public Transform transform2;

    private StateMachine stateMachine;

    protected void Awake()
    {
        Debug.Log("Enemy1 Awake");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>(); //get player controller
        stateMachine = new StateMachine(this, states);
        transform1.position = new Vector3(0, 3.83f, -16.5f);
        transform2.position = new Vector3(0, 3.83f, 16.5f);
    }

    private void Update()
    {
        stateMachine.HandleUpdate();
    }
}