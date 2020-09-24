using UnityEngine;

public class MonkeyAudioManager : MonoBehaviour
{
    public AudioClip[] monkeyAudios = new AudioClip[2];

    public AudioSource boca;

    public void YellAudio()
    {
        boca.PlayOneShot(monkeyAudios[0]);
    }

    public void FrustationAudio()
    {
        boca.PlayOneShot(monkeyAudios[1]);
    }
}