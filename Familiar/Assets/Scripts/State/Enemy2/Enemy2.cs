using UnityEngine;
using UnityEngine.AI; //Navmesh https://docs.unity3d.com/Manual/nav-HowTos.html

public class Enemy2 : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public int health = 4;
    public bool zapped;

    public NavMeshAgent navAgent;
    public LayerMask collisionMask;
    public Controller playerController;
    public Transform idlePosition;
    public State[] states;

    public Vector3 vecToPlayer;

    private StateMachine stateMachine;

    protected void Awake()
    {
        idlePosition = transform;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>(); //get player controller
        navAgent = GetComponent<NavMeshAgent>(); //get nav mesh
        stateMachine = new StateMachine(this, states);
    }

    private void Update()
    {
        vecToPlayer = playerController.transform.position;
        stateMachine.HandleUpdate();
        Debug.DrawLine(transform.position, vecToPlayer, Color.red);
    }
}