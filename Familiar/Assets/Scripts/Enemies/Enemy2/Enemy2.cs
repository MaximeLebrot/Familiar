using System.Collections;
using UnityEngine;
using UnityEngine.AI; //Navmesh https://docs.unity3d.com/Manual/nav-HowTos.html
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour, IZappable
{
    [SerializeField, Tooltip("The states that this enemy will use throughout their life")]
    private State[] states;

    [Header("Stats")]
    [SerializeField, Tooltip("The maximum health of the enemy")] 
    private float maxHealth;
    [Tooltip("The current health of the enemy")] 
    private float health;
    [Tooltip("Checks whether the enemy can attack")]
    private bool canAttack;

    [Header("Drop")]
    [SerializeField, Tooltip("The gameobject dropped on death. Should be inputed manually")] 
    private GameObject drop;
    [SerializeField, Tooltip("A reference to the \"ManaPickup\" script on the drop gameobject. Should be inputed manually")]
    private ManaPickup mana;

    [Header("Self references")]
    [SerializeField, Tooltip("The navigation mesh agent attached to this game object. Should be inputed manually")]
    private NavMeshAgent navAgent;
    [SerializeField, Tooltip("The animator attached to this game object. Should be inputed manually")]
    private Animator anim;
    [SerializeField, Tooltip("The transform component attached to this game object. Should be inputed manually")]
    new private Transform transform;

    [Header("Player")]
    [SerializeField, Tooltip("Reference to the player game object. Should be inputed manually")]
    private GameObject player;
    [SerializeField, Tooltip("A reference to the instance of the \"Player\" script attached to the player game object. Should be inputed manually")]
    private AbilitySystem.Player playerStats;
    [SerializeField, Tooltip("The transform component attached to the player game object. . Should be inputed manually")]
    private Transform playerTransform;

    [SerializeField, Tooltip("The transform component of this game objects idle position")]
    private Vector3 idlePosition;
    [Tooltip("A vector to the player, updates at runtime")]
    private Vector3 vecToPlayer;

    [Tooltip("The active statemachine of this component")]
    private StateMachine stateMachine;

    [SerializeField, Tooltip("The health bar slider")]
    private Slider slider;

    protected void Awake()
    {
        InitializeVariables(); //this is done in case the variables are not set in the inspector
        stateMachine = new StateMachine(this, states);
    }

    private void Update()
    {
        vecToPlayer = playerTransform.position - transform.position;
        //Debug.DrawLine(transform.position, vecToPlayer, Color.red);
        stateMachine.HandleUpdate();
    }

    public bool IsZapped
    {
        get
        {
            return false;
        }

        set
        {
        }
    }

    public void OnZap()
    {
        TakeDamage(1.0f);
        //Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Spider took " + damage + " damage");

        slider.value -= (damage / maxHealth);

        health -= damage;
        anim.SetTrigger("spiderDmg");
    }

    public IEnumerator AttackCooldown(float cooldown)
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    public IEnumerator KillAfterAnim()
    {
        yield return new WaitForSeconds(1.0f);
        drop.SetActive(true);
        mana.SetPosition(transform.position);
        Destroy(gameObject);
    }

    private void InitializeVariables()
    {
        InitializePlayerGameObject();
        InitializePlayerScript();
        InitializeDrop();
        InitializeTransforms();
        InitializeHealth();
        InitializeAnimator();
        InitializeNavAgent();
        InitializeHealthSlider();
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
    private void InitializeDrop()
    {
        if (drop == null)
            Debug.LogError("Drop game object not set");
        if (mana == null)
            mana = GetComponentInParent<Transform>().gameObject.GetComponentInChildren<ManaPickup>();
    }
    private void InitializeTransforms() //the transforms are set as variables instead because gameObject.transform performs a GetComponent() which is costly
    {
        if (this.transform == null) //if the value has been inputed manually, use it. Else find the component
            this.transform = gameObject.transform;
        if (player == null)
            InitializePlayerGameObject();
        if (playerTransform == null) //if the value has been inputed manually, use it. Else find the component
            playerTransform = player.transform;
        idlePosition = transform.position;
    }
    private void InitializeHealth()
    {
        health = maxHealth;
    }
    private void InitializeNavAgent()
    {
        if (navAgent == null) //if the value has been inputed manually, use it. Else find the component
            navAgent = GetComponent<NavMeshAgent>();
    }
    private void InitializeAnimator()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }
    private void InitializeHealthSlider()
    {
        slider.value = 1.0f;
    }

    public Transform Transform
    {
        get => transform;
    }
    public NavMeshAgent NavAgent
    {
        get => navAgent;
    }
    public Animator Anim
    {
        get => anim;
    }
    public Transform PlayerTransform
    {
        get => playerTransform;
    }
    public AbilitySystem.Player PlayerStats
    {
        get => playerStats;
    }
    public Vector3 VecToPlayer
    {
        get => vecToPlayer;
    }
    public Vector3 IdlePosition
    {
        get => idlePosition;
    }
    public float Health
    {
        get => health;
    }
    public bool CanAttack
    {
        get => canAttack;
        set => canAttack = value;
    }
}