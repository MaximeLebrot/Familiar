using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioHandler : MonoBehaviour
{
    [SerializeField, Tooltip("The volume of this instance of the audio handler")]
    private float volume;
    private static readonly float volumeMultiplier = 0.1f;

    [SerializeField, Tooltip("Array of jumping sounds")]
    public AudioClip[] jumpSounds;
    [SerializeField, Tooltip("Array of damage sounds")]
    public AudioClip[] damageSounds;
    [SerializeField, Tooltip("Array of death sounds")]
    public AudioClip[] deathSounds;
    [SerializeField, Tooltip("Array of moving sounds")]
    public AudioClip[] movingSounds;
    [SerializeField, Tooltip("Array of zapping sounds")]
    public AudioClip[] zappingSounds;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volume = Sound.Instance.EffectsVolume * volumeMultiplier;
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)], volume);
    }
    public void PlayDamageSound()
    {
        audioSource.PlayOneShot(damageSounds[Random.Range(0, damageSounds.Length)], volume);
    }
    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSounds[Random.Range(0, deathSounds.Length)], volume);
    }
    public void PlayMovingSound()
    {
        audioSource.PlayOneShot(movingSounds[Random.Range(0, movingSounds.Length)], volume);
    }
    public void PlayZappingSound()
    {
        audioSource.PlayOneShot(zappingSounds[Random.Range(0, zappingSounds.Length)], volume);
    }

    public float Volume
    {
        get => volume;
        set => volume = value;
    }
}
