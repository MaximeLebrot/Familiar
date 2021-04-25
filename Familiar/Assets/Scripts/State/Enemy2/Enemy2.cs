using UnityEngine;
using UnityEngine.AI; //Navmesh https://docs.unity3d.com/Manual/nav-HowTos.html

public class Enemy2 : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public int health = 4;
    public bool zapped;
    public GameObject drop;
    public ManaPickup mana;

    public NavMeshAgent navAgent;
    public LayerMask collisionMask;
    public GameObject player;
    public Transform idlePosition;
    public State[] states;

    public Vector3 vecToPlayer;

    private StateMachine stateMachine;

    protected void Awake()
    {
        idlePosition = transform;
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        stateMachine = new StateMachine(this, states);
    }

    private void Update()
    {
        vecToPlayer = player.transform.position;
        stateMachine.HandleUpdate();
        Debug.DrawLine(transform.position, vecToPlayer, Color.red);
    }
}