using UnityEngine;

[RequireComponent(typeof(Puerta))]
public class DoorAudioManager : MonoBehaviour
{
    public AudioClip[] doorAudios = new AudioClip[2];

    public AudioSource doorAudioSource;


    public void AbrirPuertaAudio()
    {
        doorAudioSource.PlayOneShot(doorAudios[0]);
    }

    public void CerrarPuertaAudio()
    {
        doorAudioSource.PlayOneShot(doorAudios[1]);
    }
}