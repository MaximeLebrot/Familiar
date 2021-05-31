using UnityEngine;

public class FireZone : MonoBehaviour
{
    [SerializeField, Tooltip("The particle system component tied to this game object. Should be inputed manually")]
    private new ParticleSystem particleSystem;
    [SerializeField, Tooltip("A reference to the \"Player\" script tid to the player game object. Should be inputed manually")]
    private AbilitySystem.Player player;
    [SerializeField, Tooltip("A reference to the audio source component tied to this game object. Should be inputed manually")]
    private AudioSource audioS;

    private static readonly float volumeMultiplier = 0.2f;

    private bool isWorking = true;
    private bool inZone;
    public float damage;

    private static readonly float time = 0.1f;
    private float timer;
    private void Start()
    {
        if (particleSystem == null)
            particleSystem = GetComponent<ParticleSystem>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilitySystem.Player>();
        if (audioS == null)
            audioS = GetComponent<AudioSource>();
        audioS.volume = Sound.Instance.EffectsVolume * volumeMultiplier;
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
        if (audioS.volume != Sound.Instance.EffectsVolume * volumeMultiplier)
            audioS.volume = Sound.Instance.EffectsVolume * volumeMultiplier;
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
        particleSystem.Stop();
        audioS.Stop();
        isWorking = false;
    }
}
