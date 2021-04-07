using UnityEngine;
using UnityEngine.AI; //Navmesh

public class Enemy1 : MonoBehaviour
{
    //Enemy1Controller
    public float moveSpeed = 10.0f;
    public bool zapped;

    public Controller playerController;
    public State[] states;
    public Transform transform1;
    public Transform transform2;

    public Vector3 vecToPoint1 = new Vector3(0, 3.83f, -16.5f);
    public Vector3 vecToPoint2 = new Vector3(0, 3.83f, 16.5f);

    public Vector3 vecToPlayer;

    private StateMachine stateMachine;

    protected void Awake()
    {
        Debug.Log("Enemy1 Awake");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>(); //get player controller
        stateMachine = new StateMachine(this, states);
        transform1.position = vecToPoint1;
        transform2.position = vecToPoint2;
    }

    private void Update()
    {
        vecToPlayer = playerController.transform.position; //+-*/?
        stateMachine.HandleUpdate();
        Debug.DrawLine(transform.position, vecToPlayer, Color.red);
        Debug.DrawLine(transform.position, vecToPoint1, Color.blue);
        Debug.DrawLine(transform.position, vecToPoint2, Color.green);
    }
}