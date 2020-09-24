using UnityEngine;

public class InteractuableAudioManager : MonoBehaviour
{
    public AudioClip[] interactuableAudios = new AudioClip[2];

    public AudioSource interactuable;

    public void ActivateAudio()
    {
        interactuable.PlayOneShot(interactuableAudios[0]);
    }

    public void DeactivateAudio()
    {
        interactuable.PlayOneShot(interactuableAudios[1]);
    }
}