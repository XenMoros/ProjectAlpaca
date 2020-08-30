using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Puerta))]
public class DoorAudioManager : AudioController
{
    public AudioClip[] doorAudios = new AudioClip[1];

    internal override void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        base.Awake();
    }

    public void AbrirPuertaAudio()
    {
        audioSource.PlayOneShot(doorAudios[0]);
    }

    public void CerrarPuertaAudio()
    {
        audioSource.PlayOneShot(doorAudios[0]);
    }
}