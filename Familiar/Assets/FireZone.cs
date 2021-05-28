using UnityEngine;

public class FireZone : MonoBehaviour
{
    [SerializeField, Tooltip("The animator component tied to this game object. Should be inputed manually")]
    private Animator anim;
    [SerializeField, Tooltip("A reference to the \"Player\" script tid to the player game object. Should be inputed manually")]
    private AbilitySystem.Player player;

    private bool isWorking = true;
    private bool inZone;
    public float damage;

    private static readonly float time = 0.2f;
    private float timer;

    private void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilitySystem.Player>();
        timer = time;
    }

    private void Update()
    {
        if (isWorking == true)
        {
            if (player.Dead == true)
                inZone = false;
            if (inZone == true)
                DamagePlayer();
        }
    }

    private void DamagePlayer()
    {
        if (timer <= 0)
        {
            timer = time;
            player.TakeDamage(damage);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isWorking == true)
        {
            if (player.Dead != true)
            {
                if (other.CompareTag("Player"))
                    inZone = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isWorking == true)
        {
            if (player.Dead != true)
            {
                if (other.CompareTag("Player"))
                    inZone = false;
            }
        }
    }

    public void Deactivate()
    {
        anim.SetBool("isActive", false);
        isWorking = false;
        //andra saker för att medela att det är över
    }
}
