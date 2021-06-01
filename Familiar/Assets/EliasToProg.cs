using UnityEngine;

public class EliasToProg : MonoBehaviour
{
    private static EliasPlayer eliasPlayer;

    [SerializeField]
    private AbilitySystem.Player player;

    private static readonly string actionPresetName = "From Exp to Prog";
    [SerializeField]
    private bool allowRequiredThemeMissmatch;

    void Awake()
    {
        if (Sound.Instance != null)
            eliasPlayer = Sound.Instance.EliasPlayer;
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilitySystem.Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayFromExpToProg();
            player.SpawnPosition = 1;
        }
    }

    private void PlayFromExpToProg()
    {
        eliasPlayer.RunActionPreset(actionPresetName, allowRequiredThemeMissmatch);
    }
}
