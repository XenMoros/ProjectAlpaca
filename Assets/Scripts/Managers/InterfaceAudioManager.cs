using UnityEngine;

public class InterfaceAudioManager : AudioController
{

    public AudioClip[] interfaceAudios = new AudioClip[5];

    internal override void Awake()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        base.Awake();
    }

    public void ButtonClickAudio()
    {
        audioSource.PlayOneShot(interfaceAudios[0]);
    }
    public void SliderChangeAudio()
    {
        audioSource.PlayOneShot(interfaceAudios[1]);
    }

    public void ToggleChangeAudio()
    {
        audioSource.PlayOneShot(interfaceAudios[2]);
    }
    public void ElementSelectAudio()
    {
        audioSource.PlayOneShot(interfaceAudios[3]);
    }

    public void StartGameAudio()
    {
        audioSource.PlayOneShot(interfaceAudios[4]);
    }
    

}
