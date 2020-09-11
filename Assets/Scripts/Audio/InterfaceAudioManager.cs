using UnityEngine;

public class InterfaceAudioManager : MonoBehaviour
{

    public AudioClip[] interfaceAudios = new AudioClip[4];

    public AudioSource menuAudioSource;

    internal void Awake()
    {
        menuAudioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void ButtonClickAudio()
    {
        menuAudioSource.PlayOneShot(interfaceAudios[0]);
    }

    public void ElementSelectAudio()
    {
        menuAudioSource.PlayOneShot(interfaceAudios[0]);
    }

    public void SliderChangeAudio()
    {
        menuAudioSource.PlayOneShot(interfaceAudios[1]);
    }

    public void ToggleChangeAudio()
    {
        menuAudioSource.PlayOneShot(interfaceAudios[2]);
    }
   
    public void StartGameAudio()
    {
        menuAudioSource.PlayOneShot(interfaceAudios[3]);
    }


}
