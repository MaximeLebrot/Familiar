using UnityEngine;

public class Sound : MonoBehaviour 
{
    public static Sound Instance { get; set; }

    private float volume;
    
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
        Instance.Volume = 1.0f;
    }

    public float Volume
    {
        get => volume;
        set => volume = value;
    }

}
