using System.Collections;
using UnityEngine;
using UnityEngine.AI; //Navmesh https://docs.unity3d.com/Manual/nav-HowTos.html
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour, IZappable
{
    [Tooltip("The states that this enemy will use throughout their life")]
    [SerializeField] private State[] states;

    [Header("Stats")]
    [SerializeField] private float maxHealth;
    private float health;
    private bool canAttack;
    [Header("Drop")]
    [SerializeField] private GameObject drop;
    [SerializeField] private ManaPickup mana;

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

    private Vector3 idlePosition;
    [HideInInspector] public Vector3 vecToPlayer;
    private StateMachine stateMachine;

    [SerializeField] private Slider slider;

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

    public float GetHealth()
    {
        return health;
    }
    private void SetHealth(float health)
    {
        this.health = health;
    }
    public bool GetCanAttack()
    {
        return canAttack;
    }

    public void SetCanAttack(bool b)
    {
        canAttack = b;
    }
    private void SetHealthSliderValue(float value)
    {
        slider.value = value;
    }
    public Vector3 GetIdlePosition()
    {
        return idlePosition;
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
}