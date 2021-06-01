using UnityEngine;

public class EliasGameOver : MonoBehaviour
{
    [SerializeField]
    private static EliasPlayer eliasPlayer;

    private static readonly string preActionPresetName = "From Exp to Prog Fastest";
    private static readonly string actionPresetName = "Game Over";
    [SerializeField]
    private bool allowRequiredThemeMissmatch;

    void Awake()
    {
        if (Sound.Instance != null)
            eliasPlayer = Sound.Instance.EliasPlayer;
    }
    
    public void PlayGameOverTheme()
    {
        eliasPlayer.RunActionPreset(preActionPresetName, allowRequiredThemeMissmatch);
        eliasPlayer.RunActionPreset(actionPresetName, allowRequiredThemeMissmatch);
    }
}
