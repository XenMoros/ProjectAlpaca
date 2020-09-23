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
        if(StaticManager.masterVolume == 0) audioMixer.SetFloat("MasterVolume", -50);
        else audioMixer.SetFloat("MasterVolume", (StaticManager.masterVolume * 30) - 30);
    }

    void CangeMusic()
    {
        if (StaticManager.musicVolume == 0) audioMixer.SetFloat("MusicVolume", -50);
        else audioMixer.SetFloat("MusicVolume", (StaticManager.musicVolume * 30) - 30);
    }

    void ChangeMenu()
    {
        if (StaticManager.menuVolume == 0) audioMixer.SetFloat("MenuVolume", -50);
        else audioMixer.SetFloat("MenuVolume", (StaticManager.menuVolume * 30) - 30);
    }

    void ChangeEffects()
    {
        //audioMixer.SetFloat("EffectsVolume", (StaticManager.effectsVolume * 50) - 50);
    }

    private void MuteOnPause()
    {

        if (StaticManager.pause) {
            audioMixer.SetFloat("EffectsVolume", -50);
            audioMixer.SetFloat("MenuVolume", (StaticManager.menuVolume * 30) - 30);
        }
        else {
            audioMixer.SetFloat("EffectsVolume", (StaticManager.effectsVolume * 30) - 30);
            audioMixer.SetFloat("MenuVolume", -50f);
        }
    }

    public void MuteOnLoad()
    {
        audioMixer.SetFloat("MenuVolume", -50f);
    }
}
