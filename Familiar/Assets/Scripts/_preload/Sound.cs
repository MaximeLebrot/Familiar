using UnityEngine;

public class Sound : MonoBehaviour 
{
    public static Sound Instance { get; set; }

    private float volume;
    [SerializeField]
    private new AudioSource audio;
    [SerializeField]
    private EliasPlayer eliasPlayer;

    void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this; 
        } 
        else 
        { 
            Debug.Log("Warning: multiple " + this + " in scene!"); 
        }
        audio = GetComponent<AudioSource>();
        eliasPlayer = GetComponent<EliasPlayer>();
        Instance.Volume = 1.0f;
    }

    public float Volume
    {
        get => volume;
        set => volume = value;
    }
    public EliasPlayer EliasPlayer
    {
        get => eliasPlayer;
    }

    public void SetGlobalVolume(float value)
    {
        SetMusicVolume(value);
        SetEffectsVolume(value);
    }
    public void SetMusicVolume(float value)
    {
        audio.volume = value;
    }

    public void SetEffectsVolume(float value)
    {

    }
}
