using UnityEngine;

public class Stats : MonoBehaviour
{
    public static Stats Instance { get; set; }

    private float mouseSensitivity;

    private Vector3 position;

    private float health;

    private int difficulty;

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
        Instance.Difficulty = 1;
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
}
