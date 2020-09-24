using UnityEngine;

[RequireComponent(typeof(ButtonScript))]
public class BotonParedAudioManager : InteractuableAudioManager
{

    public AudioSource tickTackSource;

    public void StartTicTacAudio()
    {
        tickTackSource.Play();
    }

    public void StopTicTacAudio()
    {
        tickTackSource.Stop();
    }
}