using UnityEngine;
using UnityEngine.AI; //Navmesh https://docs.unity3d.com/Manual/nav-HowTos.html

public class Enemy1 : MonoBehaviour
{
    public bool zapped;

    public LayerMask collisionMask;
    public GameObject player;
    public State[] states;

    public Transform[] points; 
    public int destPoint = 0;
    public Vector3 vecToPlayer;
    public GameObject playerRespawnPoint;
    public Vector3 playerRespawnLocation;

    public NavMeshAgent navAgent;
    private StateMachine stateMachine;

    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        playerRespawnLocation = playerRespawnPoint.transform.position;
        stateMachine = new StateMachine(this, states);
    }

    private void Update()
    {
        vecToPlayer = player.transform.position;
        stateMachine.HandleUpdate();
        Debug.DrawLine(transform.position + new Vector3(0, 5, 0), vecToPlayer, Color.red);
        Debug.DrawLine(transform.position + new Vector3(0, 5, 0), navAgent.velocity, Color.cyan);
    }

    public void OnZap()
    {
        Debug.Log("Zapping enemy 1");
        Destroy(this, 1.0f);
    }
}