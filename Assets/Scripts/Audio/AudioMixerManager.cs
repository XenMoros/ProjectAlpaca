using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    internal void Awake()
    {
        StaticManager.OnMasterVolumeChange += ChangeMaster;
        StaticManager.OnMenuVolumeChange += ChangeMenu;
        StaticManager.OnEffectsVolumeChange += ChangeEffects;
        StaticManager.OnMusicVolumeChange += CangeMusic;
        StaticManager.OnPauseChange += MuteOnPause;

    }

    private void OnDisable()
    {
        StaticManager.OnMasterVolumeChange -= ChangeMaster;
        StaticManager.OnMenuVolumeChange -= ChangeMenu;
        StaticManager.OnEffectsVolumeChange -= ChangeEffects;
        StaticManager.OnMusicVolumeChange -= CangeMusic;
        StaticManager.OnPauseChange -= MuteOnPause;
    }

    void ChangeMaster()
    {
        audioMixer.SetFloat("MasterVolume", (StaticManager.masterVolume * 50) - 50);
    }

    void CangeMusic()
    {
        audioMixer.SetFloat("MusicVolume", (StaticManager.musicVolume * 50) - 50);
    }

    void ChangeMenu()
    {
        audioMixer.SetFloat("MenuVolume", (StaticManager.menuVolume * 50) - 50);
    }

    void ChangeEffects()
    {
        audioMixer.SetFloat("EffectsVolume", (StaticManager.effectsVolume * 50) - 50);
    }

    private void MuteOnPause()
    {

        if (StaticManager.pause) {
            audioMixer.SetFloat("MusicVolume", (StaticManager.musicVolume * 25) - 50);
            audioMixer.SetFloat("EffectsVolume", -50f);
            audioMixer.SetFloat("MenuVolume", (StaticManager.menuVolume * 50) - 50);
        }
        else {
            audioMixer.SetFloat("MusicVolume", (StaticManager.musicVolume * 50) - 50);
            audioMixer.SetFloat("EffectsVolume", (StaticManager.effectsVolume * 50) - 50);
            audioMixer.SetFloat("MenuVolume", -50f);
        }
    }
}
