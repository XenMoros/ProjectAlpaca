using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class MonkeyAudioManager : AudioController
{
    public AudioClip[] monkeyAudios = new AudioClip[1];

    internal override void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        base.Awake();
    }

    public void Yell()
    {
        audioSource.PlayOneShot(monkeyAudios[0]);
    }
}