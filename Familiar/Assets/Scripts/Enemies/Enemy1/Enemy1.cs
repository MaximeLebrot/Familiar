using UnityEngine;
using UnityEngine.AI; //Navmesh https://docs.unity3d.com/Manual/nav-HowTos.html

public class Enemy1 : MonoBehaviour, IZappable
{
    public Animator anim;

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

    public bool IsZapped
    {
        get;
        set;
    }

    protected void Awake()
    {
        anim = GetComponent<Animator>();
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
        //Debug.DrawLine(transform.position + new Vector3(0, 5, 0), navAgent.velocity, Color.cyan);
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