using UnityEngine;
using UnityEngine.AI; //Navmesh https://docs.unity3d.com/Manual/nav-HowTos.html

public class Enemy1 : MonoBehaviour, IZappable
{
    [Tooltip("The states that this enemy will use throughout their life")]
    [SerializeField] private State[] states;

    [Header("Self references")]
    [Tooltip("The navigation mesh agent attached to this game object, should be inputed manually")]
    public NavMeshAgent navAgent;
    [Tooltip("The animator attached to this game object")]
    public Animator anim;
    [Tooltip("The transform component attached to this game object")]
    new public Transform transform;

    [Header("Player")]
    [Tooltip("Reference to the player game object, should be inputed manually")]
    [SerializeField] private GameObject player;
    [Tooltip("A reference to the instance of the \"Player\" script attached to the player game object")]
    public AbilitySystem.Player playerStats;
    [Tooltip("The transform component attached to the player game object")]
    public Transform playerTransform;

    [Header("Patrol")]
    [Tooltip("Array of Transforms that are used by the patrol state, should be empty if patrol state not in use")]
    public Transform[] points;
    [HideInInspector] public int destPoint = 0;
    [HideInInspector] public Vector3 vecToPlayer;

    private StateMachine stateMachine;

    protected void Awake()
    {
        InitializeVariables(); //this is done in case the variables are not set in the inspector
        stateMachine = new StateMachine(this, states);
    }

    private void Update()
    {
        vecToPlayer = playerTransform.position - transform.position;
        //Debug.DrawRay(transform.position, vecToPlayer);
        stateMachine.HandleUpdate();
    }

    public bool IsZapped
    {
        get;
        set;
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
    private void InitializeVariables()
    {
        InitializePlayerGameObject();
        InitializePlayerScript();
        InitializeAnimator();
        InitializeNavAgent();
        InitializeTransforms();
    }
    private void InitializePlayerGameObject()
    {
        if (player == null) //if the value has been inputed manually, use it. Else find the game object
            player = GameObject.FindGameObjectWithTag("Player");
    }
    private void InitializePlayerScript()
    {
        if (player == null)
            InitializePlayerGameObject();
        if (playerStats == null) //if the value has been inputed manually, use it. Else find the component
            playerStats = player.GetComponent<AbilitySystem.Player>();
    }
    private void InitializeAnimator()
    {
        if (anim == null) //if the value has been inputed manually, use it. Else find the component
            anim = GetComponent<Animator>();
    }
    private void InitializeNavAgent()
    {
        if (navAgent == null) //if the value has been inputed manually, use it. Else find the component
            navAgent = GetComponent<NavMeshAgent>();
    }
    private void InitializeTransforms() //the transforms are set as variables instead because gameObject.transform performs a GetComponent() which is costly
    {
        if (this.transform == null) //if the value has been inputed manually, use it. Else find the component
            this.transform = gameObject.transform;
        if (player == null)
            InitializePlayerGameObject();
        if (playerTransform == null) //if the value has been inputed manually, use it. Else find the component
            playerTransform = player.transform;
    }
}