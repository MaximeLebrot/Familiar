using System;
using UnityEngine;
using UnityEngine.AI; //Navmesh https://docs.unity3d.com/Manual/nav-HowTos.html

public class Enemy1 : MonoBehaviour, IZappable
{
    [SerializeField] private State[] states;

    public Animator anim;
    public LayerMask collisionMask;
    public GameObject player;
    public AbilitySystem.Player playerStats;

    public Transform[] points;
    public int destPoint = 0;
    public NavMeshAgent navAgent;
    new public Transform transform;
    public Transform playerTransform;
    public Vector3 vecToPlayer;

    private StateMachine stateMachine;

    public bool IsZapped
    {
        get;
        set;
    }

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        GetPlayerGameObject();
        GetPlayerScript();
        GetTransforms();
        navAgent = GetComponent<NavMeshAgent>();
        stateMachine = new StateMachine(this, states);
    }

    private void GetTransforms()
    {
        this.transform = gameObject.transform;
        playerTransform = player.transform;
    }

    private void GetPlayerGameObject()
    {
        if (player == null) //det är mer effektivt att mata in i inspektorn. Annars -> find object
            player = GameObject.FindGameObjectWithTag("Player");
    }
    private void GetPlayerScript()
    {
        if (playerStats == null)
            playerStats = player.GetComponent<AbilitySystem.Player>();
    }

    private void Update()
    {
        vecToPlayer = playerTransform.position - transform.position;
        stateMachine.HandleUpdate();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Moveable"))
        {
            if (collision.gameObject.transform.parent != null)
            {
                GameObject go = collision.gameObject.transform.parent.gameObject;

                if (go.TryGetComponent(out GrabObjectScript phs))
                    phs.DropObject();
            }
        }
    }

    public void OnZap()
    {
        Debug.Log("Zapping enemy 1");
        //Destroy(this, 1.0f);
        //"this" här refererar till enemy1 scriptet. Det innebär att en zapp på en enemy1 effektivt gör dem handikappade
        //kanske ville använda this.gameObject?
        //egentligen ska det vara anim.setTrigger("dead") och ha ett event i den istället.
    }

    public void RemoveNavMesh()
    {
        Destroy(navAgent);
        Rigidbody RB = GetComponent<Rigidbody>();
        Destroy(RB);
    }
}