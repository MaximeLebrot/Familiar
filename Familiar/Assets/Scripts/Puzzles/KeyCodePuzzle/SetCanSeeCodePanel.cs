using UnityEngine;

public class SetCanSeeCodePanel : MonoBehaviour
{
    [SerializeField, Tooltip("A reference to the Player game object. Should be inputed manually")]
    private GameObject player;
    [SerializeField, Tooltip("A reference to the \"Player\" script. Should be inputed manually")]
    private AbilitySystem.Player playerStats;

    void Start()
    {
        InitializeSequence();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStats.CanSeeCodePanel = true;
        }
    }
    private void InitializeSequence()
    {
        InitializePlayerGameObject();
        InitializePlayerScript();
    }
    private void InitializePlayerGameObject()
    {
        if (player == null)
        {
            Debug.LogWarning("The reference to the Player game object should be inputed manually");
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
                Debug.LogError("Cannot find player game object");
        }
    }
    private void InitializePlayerScript()
    {
        if (playerStats == null)
        {
            Debug.LogWarning("The reference to the \"Player\" script should be inputed manually");
            playerStats = player.GetComponent<AbilitySystem.Player>();
            if (playerStats == null)
                Debug.LogError("Cannot find the \"Player\" script");
        }
    }
}
