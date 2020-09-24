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
        if(StaticManager.masterVolume == 0) audioMixer.SetFloat("MasterVolume", -80f);
        else audioMixer.SetFloat("MasterVolume", (StaticManager.masterVolume * 30f) - 30f);
    }

    void CangeMusic()
    {
        if (StaticManager.musicVolume == 0) audioMixer.SetFloat("MusicVolume", -80f);
        else audioMixer.SetFloat("MusicVolume", (StaticManager.musicVolume * 30f) - 30f);
    }

    void ChangeMenu()
    {
        if (StaticManager.menuVolume == 0) audioMixer.SetFloat("MenuVolume", -80f);
        else audioMixer.SetFloat("MenuVolume", (StaticManager.menuVolume * 30f) - 30f);
    }

    void ChangeEffects()
    {
        if (StaticManager.effectsVolume == 0) audioMixer.SetFloat("EffectsVolume", -80f);
        else audioMixer.SetFloat("EffectsVolume", (StaticManager.effectsVolume * 30f) - 30f);
    }

    private void MuteOnPause()
    {

        if (StaticManager.pause) {
            audioMixer.SetFloat("EffectsVolume", -80f);
            audioMixer.SetFloat("MenuVolume", (StaticManager.menuVolume * 30f) - 30f);
        }
        else {
            audioMixer.SetFloat("EffectsVolume", (StaticManager.effectsVolume * 30f) - 30f);
            audioMixer.SetFloat("MenuVolume", -80f);
        }
    }

    public void MuteOnLoad(bool stop = true)
    {
        if (stop) audioMixer.SetFloat("MenuVolume", -80f);
        else audioMixer.SetFloat("MenuVolume", (StaticManager.menuVolume * 30f) - 30f);
    }
}
