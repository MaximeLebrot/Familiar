using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioHandler : MonoBehaviour
{
    public float volume = .025f;

    public AudioClip[] jumpSounds;
    public AudioClip[] runSounds;
    public AudioClip[] damageSounds;
    public AudioClip[] deathSounds;
    public AudioClip[] movingSounds; //flyg / wingflap
    public AudioClip[] zappingSounds;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)], volume);
    }
    public void PlayRunSound()
    {
        audioSource.PlayOneShot(runSounds[Random.Range(0, runSounds.Length)], volume);
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
        audioSource.PlayOneShot(zappingSounds[Random.Range(0, movingSounds.Length)], volume);
    }
}
