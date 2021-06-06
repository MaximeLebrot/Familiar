using UnityEngine;

public class Stats : MonoBehaviour
{
    public static Stats Instance { get; set; }

    private float mouseSensitivity;

    private Vector3 position;

    private Vector3 rotation;

    private float health;

    private int difficulty;

    private bool arachnophobiaMode;

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
        Instance.MouseSensitivity = 3.0f;
        Instance.Health = 10f;
        Instance.Difficulty = 2;
        Instance.ArachnophobiaMode = false;
    }

    public float MouseSensitivity
    {
        get => mouseSensitivity;
        set => mouseSensitivity = value;
    }

    public int Difficulty
    {
        get => difficulty;
        set => difficulty = value;
    }

    public float Health
    {
        get => health;
        set => health = value;
    }

    public Vector3 Position
    {
        get => position;
        set => position = value;
    }

    public Vector3 Rotation
    {
        get => rotation;
        set => rotation = value;
    }

    public bool ArachnophobiaMode
    {
        get => arachnophobiaMode;
        set => arachnophobiaMode = value;
    }
}
