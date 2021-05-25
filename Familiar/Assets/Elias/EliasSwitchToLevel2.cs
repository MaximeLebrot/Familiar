using UnityEngine;

public class EliasSwitchToLevel2 : MonoBehaviour
{
    private static EliasPlayer eliasPlayer;

    private static readonly string actionPresetName = "From Prog To Exp Fast";
    [SerializeField]
    private bool allowRequiredThemeMissmatch;

    void Awake()
    {
        if (Sound.Instance != null)
            eliasPlayer = Sound.Instance.EliasPlayer;
    }

    public void PlayFromProgToExp()
    {
        Debug.Log("Switching to Level 2");
        eliasPlayer.RunActionPreset(actionPresetName, allowRequiredThemeMissmatch);
    }
}
