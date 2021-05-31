using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioHandler : MonoBehaviour
{
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
    [SerializeField, Tooltip("Array of puzzle completion sounds")]
    public AudioClip[] puzzleCompletionSounds;
    [SerializeField]
    AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)], GetVolume());
    }
    public void PlayDamageSound()
    {
        audioSource.PlayOneShot(damageSounds[Random.Range(0, damageSounds.Length)], GetVolume());
    }
    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSounds[Random.Range(0, deathSounds.Length)], GetVolume());
    }
    public void PlayMovingSound()
    {
        audioSource.PlayOneShot(movingSounds[Random.Range(0, movingSounds.Length)], GetVolume());
    }
    public void PlayZappingSound()
    {
        audioSource.PlayOneShot(zappingSounds[Random.Range(0, zappingSounds.Length)], GetVolume());
    }
    public void PlayPuzzleCompletionSound()
    {
        audioSource.PlayOneShot(puzzleCompletionSounds[Random.Range(0, puzzleCompletionSounds.Length)], GetVolume());
    }
    public float GetVolume()
    {
        return Sound.Instance.EffectsVolume * volumeMultiplier;
    }
}
