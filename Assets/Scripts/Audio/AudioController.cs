using UnityEngine;

public class AudioController : MonoBehaviour
{
    public enum AudioType { Music, Effects, Menu};
    public AudioType audioType;

    [Range(0f, 1f)] public float specificFactor = 1f;
    [Range(0f, 1f)] float typeFactor = 1f;

    protected AudioSource audioSource;

    internal virtual void Awake()
    {
        StaticManager.OnMasterVolumeChange += ChangeVolume;

        switch (audioType)
        {
            case AudioType.Menu:
                StaticManager.OnMenuVolumeChange += ChangeVolume;
                StaticManager.OnPauseChange += MuteOnPause;
                break;
            case AudioType.Effects:
                StaticManager.OnEffectsVolumeChange += ChangeVolume;
                StaticManager.OnPauseChange += MuteOnPause;
                break;
            case AudioType.Music:
                StaticManager.OnMusicVolumeChange += ChangeVolume;
                break;
            default:
                break;
        }
        
    }

    internal virtual void OnDisable()
    {
        StaticManager.OnMasterVolumeChange -= ChangeVolume;

        switch (audioType)
        {
            case AudioType.Menu:
                StaticManager.OnMenuVolumeChange -= ChangeVolume;
                StaticManager.OnPauseChange -= MuteOnPause;
                break;
            case AudioType.Effects:
                StaticManager.OnEffectsVolumeChange -= ChangeVolume;
                StaticManager.OnPauseChange -= MuteOnPause;
                break;
            case AudioType.Music:
                StaticManager.OnMusicVolumeChange -= ChangeVolume;
                break;
            default:
                break;
        }
    }

    private void ChangeVolume()
    {
        switch (audioType)
        {
            case AudioType.Menu:
                typeFactor = StaticManager.menuVolume;
                break;
            case AudioType.Effects:
                typeFactor = StaticManager.effectsVolume;
                break;
            case AudioType.Music:
                typeFactor = StaticManager.musicVolume;
                break;
            default:
                break;
        }

        audioSource.volume = StaticManager.masterVolume * typeFactor * specificFactor;
    }

    private void MuteOnPause()
    {
        switch (audioType)
        {
            case AudioType.Menu:
                audioSource.volume = StaticManager.pause ? StaticManager.masterVolume * typeFactor * specificFactor : 0;
                break;
            case AudioType.Effects:
                audioSource.volume = !StaticManager.pause ? StaticManager.masterVolume * typeFactor * specificFactor : 0;
                break;
            case AudioType.Music:
                audioSource.volume = StaticManager.pause ? 0.5f * StaticManager.masterVolume * typeFactor * specificFactor : 0;
                break;
            default:
                break;
        }
    }
}
