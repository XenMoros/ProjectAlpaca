using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudioManager : MonoBehaviour
{
    public AudioClip[] musicAudios = new AudioClip[2];

    public AudioSource musicAudioSource;

    internal void Awake()
    {
        AudioSource[] camaraAudioSources = Camera.main.GetComponentsInChildren<AudioSource>();

        foreach(AudioSource source in camaraAudioSources)
        {
            if (source.gameObject.name == "MusicSource") musicAudioSource = source;
        }
    }

    public void GameAudio(int audio)
    {
        if (musicAudioSource.isPlaying) musicAudioSource.Stop();
        if (audio >= 0 && audio < musicAudios.Length) musicAudioSource.clip = musicAudios[audio];
        else musicAudioSource.clip = musicAudios[0];
        musicAudioSource.Play();
    }

    public void StopAudio()
    {
        if(musicAudioSource.isPlaying) musicAudioSource.Stop();
    }

}
