using System.Collections;
using UnityEngine;

public class EliasDemoEventTrigger : MonoBehaviour
{
    [SerializeField]
    private static EliasPlayer eliasPlayer;
	//public EliasPlayer eliasPlayer;
    public bool useSetLevel;
    public EliasSetLevel setLevel;

    public bool useSetLevelOnTrack;
    public EliasSetLevelOnTrack setLevelOnTrack;

    public bool usePlayStinger;
	public EliasPlayStinger playStinger;

	public bool useActionPreset;
	public string actionPresetName;
	public bool allowRequiredThemeMissmatch;

	public bool useDoubleEffectParam;
	public EliasSetEffectParameterDouble doubleEffectParam;

	public bool useSetSendVolume;
	public EliasSetSendVolume setSendVolume;

    void Awake()
    {
        if (Sound.Instance != null)
            eliasPlayer = Sound.Instance.EliasPlayer;
    }

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (useSetLevel)
            {
                eliasPlayer.QueueEvent(setLevel.CreateSetLevelEvent(eliasPlayer.Elias));
            }
            if (useSetLevelOnTrack)
            {
                eliasPlayer.QueueEvent(setLevelOnTrack.CreateSetLevelOnTrackEvent(eliasPlayer.Elias));
            }
            if (usePlayStinger)
            {
                eliasPlayer.QueueEvent(playStinger.CreatePlayerStingerEvent(eliasPlayer.Elias));
            }
            if (useActionPreset)
            {
                eliasPlayer.RunActionPreset(actionPresetName, allowRequiredThemeMissmatch);
            }
            if (useDoubleEffectParam)
            {
                eliasPlayer.QueueEvent(doubleEffectParam.CreateSetEffectParameterEvent(eliasPlayer.Elias));
            }
            if (useSetSendVolume)
            {
                eliasPlayer.QueueEvent(setSendVolume.CreateSetSendVolumeEvent(eliasPlayer.Elias));
            }
        }
    }
}