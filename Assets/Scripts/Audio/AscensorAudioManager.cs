using UnityEngine;


[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Elevador))]
public class AscensorAudioManager : AudioController
{
    public AudioClip[] ascensorAudios = new AudioClip[2];

    public Elevador elevador;

    internal override void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = ascensorAudios[0];

        elevador.OnLeave += StartElevatorAudio;
        elevador.OnReach += StopElevatorAudio;
    }

    internal override void OnDisable()
    {
        base.OnDisable();

        elevador.OnLeave -= StartElevatorAudio;
        elevador.OnReach -= StopElevatorAudio;

    }

    public void StartElevatorAudio()
    {
        audioSource.Play();
    }

    public void StopElevatorAudio()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(ascensorAudios[1]);
    }


}