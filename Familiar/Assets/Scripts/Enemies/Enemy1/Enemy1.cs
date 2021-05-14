using UnityEngine;
using UnityEngine.AI; //Navmesh https://docs.unity3d.com/Manual/nav-HowTos.html

public class Enemy1 : MonoBehaviour, IZappable
{
    [SerializeField, Tooltip("The states that this enemy will use throughout their life")]
    private State[] states;

    [Header("Self references")]
    [SerializeField, Tooltip("The navigation mesh agent attached to this game object. Should be inputed manually")]
    private NavMeshAgent navAgent;
    [SerializeField, Tooltip("The animator attached to this game object. Should be inputed manually")]
    private Animator anim;
    [SerializeField, Tooltip("The transform component attached to this game object. Should be inputed manually")]
    new private Transform transform;
    [SerializeField, Tooltip("The transform component attached to the \"eyes\" game object. Should be inputed manually")]
    private Transform visionOrigin;

    [Header("Player")]
    [SerializeField, Tooltip("Reference to the player game object. Should be inputed manually")]
    private GameObject player;
    [SerializeField, Tooltip("A reference to the instance of the \"Player\" script attached to the player game object")]
    private AbilitySystem.Player playerStats;
    [SerializeField, Tooltip("The transform component attached to the player game object. Should be inputed manually")]
    private Transform playerTransform;
    [Tooltip("The vector that goes from the enemy to the player")]
    private Vector3 vecToPlayer;

    [Header("Patrol")]
    [SerializeField, Tooltip("Array of Transforms that are used by the patrol state. Should be empty if patrol state not in use. Should be inputed manually")]
    private Transform[] points;
    [Tooltip("The next destination in the points array of Transforms")] 
    private int destPoint = 0;

    [Header("Idle"), SerializeField, Tooltip("Should be true if the enemy does not patrol between points and is of idle only type")]
    private bool isIdleEnemy;

    [Tooltip("The statemachine attached to the enemy")]
    private StateMachine stateMachine;
    private static string patrolPoint = "Patrol point";

    protected void Awake()
    {
        InitializeVariables(); //this is done in case the variables are not set in the inspector
        InitializeStateMachine();
        InitializePatrolPoints(); //this is done in case the variables are not set in the inspector
    }

    private void Update()
    {
        vecToPlayer = playerTransform.position - transform.position;
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

    private void InitializeStateMachine()
    {
        stateMachine = new StateMachine(this, states);
    }

    //this should never run and should be used only make sure there are patrol points tied to the enemy
    private void InitializePatrolPoints()
    {
        if (points.Length == 0)
        {
            Transform[] transforms;
            transforms = GetComponentsInChildren<Transform>();
            if (transforms != null)
            {
                GameObject[] gameObjects;
                gameObjects = GameObject.FindGameObjectsWithTag(patrolPoint);
                if (gameObjects != null)
                {
                    foreach (Transform trans in transforms)
                    {
                        foreach (GameObject gameObject in gameObjects)
                        {
                            if (trans.gameObject == gameObject)
                            {
                                points.SetValue(trans, points.Length + 1);
                            }
                        }
                    }
                }
            }
        }
    }

    public NavMeshAgent NavAgent
    {
        get => navAgent;
    }

    public Animator Anim
    {
        get => anim;
    }

    public AbilitySystem.Player PlayerStats
    {
        get => playerStats;
    }

    public Transform[] Points
    {
        get => points;
    }

    public int DestPoint
    {
        get => destPoint;
        set => destPoint = value;
    }

    public Vector3 VecToPlayer
    {
        get => vecToPlayer;
    }

    public Transform PlayerTransform
    {
        get => playerTransform;
    }

    public bool IsIdleEnemy
    {
        get => isIdleEnemy;
    }
}