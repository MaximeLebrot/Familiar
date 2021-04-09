using UnityEngine;
using UnityEngine.AI; //Navmesh /*https://docs.unity3d.com/Manual/nav-HowTos.html*/

public class Enemy1 : MonoBehaviour
{
    //Enemy1Controller
    public float moveSpeed = 10.0f;
    public bool zapped;

    public NavMeshAgent navAgent;
    public LayerMask collisionMask;
    public Controller playerController;
    public State[] states;
    public Transform patrolPoint1;
    public Transform patrolPoint2;

    public Vector3 vecToPoint1 = new Vector3(0, 3.83f, -16.5f);
    public Vector3 vecToPoint2 = new Vector3(0, 3.83f, 16.5f);
    public Vector3 vecToPlayer;

    private StateMachine stateMachine;

    protected void Awake()
    {
        Debug.Log("Enemy1 Awake");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>(); //get player controller
        navAgent = GetComponent<NavMeshAgent>(); //get nav mesh
        stateMachine = new StateMachine(this, states);

        patrolPoint1.position = vecToPoint1;
        patrolPoint2.position = vecToPoint2;
    }

    private void Update()
    {
        vecToPlayer = playerController.transform.position*2 - transform.position;
        stateMachine.HandleUpdate();
        Debug.DrawLine(transform.position, vecToPlayer, Color.red);
        Debug.DrawLine(transform.position, vecToPoint1, Color.blue);
        Debug.DrawLine(transform.position, vecToPoint2, Color.green);
    }
}