using UnityEngine;

public class EliasStartingLevel : MonoBehaviour
{
    private static EliasPlayer eliasPlayer;

    [SerializeField]
    private EliasSetLevel setLevel;

    void Awake()
    {
        if (Sound.Instance != null)
            eliasPlayer = Sound.Instance.EliasPlayer;
    }

    public void PlayLevel1Theme()
    {
        eliasPlayer.QueueEvent(setLevel.CreateSetLevelEvent(eliasPlayer.Elias));
    }
}
