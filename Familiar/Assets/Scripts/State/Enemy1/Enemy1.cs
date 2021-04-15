using UnityEngine;
using UnityEngine.AI; //Navmesh https://docs.unity3d.com/Manual/nav-HowTos.html

public class Enemy1 : MonoBehaviour
{
    public bool zapped;

    public LayerMask collisionMask;
    public Controller playerController;
    public State[] states;

    public Transform[] points; 
    public int destPoint = 0;
    public Vector3 vecToPlayer;

    public NavMeshAgent navAgent;
    private StateMachine stateMachine;

    protected void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>(); //get player controller
        navAgent = GetComponent<NavMeshAgent>(); //get nav mesh

        stateMachine = new StateMachine(this, states);
    }

    private void Update()
    {
        //vecToPlayer = playerController.transform.position*2 - transform.position;
        vecToPlayer = playerController.transform.position;
        stateMachine.HandleUpdate();
        Debug.DrawLine(transform.position, vecToPlayer, Color.red);
    }
}