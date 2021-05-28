using UnityEngine;

public class DialogueAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioS;
    private void Start()
    {
        if (audioS == null)
            audioS = GetComponent<AudioSource>();
        if (Sound.Instance != null)
            audioS.volume = Sound.Instance.EffectsVolume;
    }

    public void PlayAudioClip(AudioClip audioClip)
    {
        if (audioS.isPlaying == true)
            audioS.Stop();
        audioS.PlayOneShot(audioClip);
    }
}
