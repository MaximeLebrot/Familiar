using UnityEngine;

public class EliasToProg : MonoBehaviour
{
    private static EliasPlayer eliasPlayer;

    private static readonly string actionPresetName = "From Exp to Prog";
    [SerializeField]
    private bool allowRequiredThemeMissmatch;

    void Awake()
    {
        if (Sound.Instance != null)
            eliasPlayer = Sound.Instance.EliasPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            PlayFromExpToProg();
    }

    private void PlayFromExpToProg()
    {
        eliasPlayer.RunActionPreset(actionPresetName, allowRequiredThemeMissmatch);
    }
}
