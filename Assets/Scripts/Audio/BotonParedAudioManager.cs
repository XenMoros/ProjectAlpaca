using UnityEngine;


[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ButtonScript))]
public class BotonParedAudioManager : AudioController
{
    public AudioClip[] botonParedAudios = new AudioClip[1];

    internal override void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = botonParedAudios[0];
        base.Awake();
    }

    public void StartTicTacAudio()
    {
        audioSource.Play();
    }

    public void StopTicTacAudio()
    {
        audioSource.Stop();
    }
}