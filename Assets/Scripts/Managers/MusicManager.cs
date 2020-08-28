using UnityEngine;

public class MusicManager : AudioController
{
    public AudioClip[] musicAudios = new AudioClip[2];

    internal override void Awake()
    {
        AudioSource[] sources = Camera.main.GetComponentsInChildren<AudioSource>();
        foreach(AudioSource source in sources)
        {
            audioSource = source.gameObject.GetInstanceID() != GetInstanceID() ? source : null;
        }
        
        base.Awake();
    }

    public void MenuAudio()
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = musicAudios[0];
        audioSource.Play();
    }
    public void AudioStop()
    {
        if (audioSource.isPlaying) audioSource.Stop();
    }

    public void GameAudio()
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = musicAudios[1];
        audioSource.Play();
    }
}
