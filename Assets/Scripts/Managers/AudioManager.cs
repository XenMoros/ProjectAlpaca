using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{ 
    // Elementos precacheados en Inspector
    public List<AudioClip> audios; // Lista de los audios del juego
    public AudioSource source; // Source de audio por defecto

    // La funcion PlaySound tiene varias implementaciones, a saber:
    // - Si solo le indicamos el audio a reproducir, lo hara desde la source por defecto
    // - Si ademas le decimos el AudioSource a reproducir, lo reproducira desde el que lo indiquemos
    // - Si ademas (para el primer caso o el primero mas el segundo) le ponemos el modo, podemos indicar si hacer Play o PlayOneShot (modos 0 o 1 respectivamente). Default es PlayOneShot

    // Lista de audios:
    // - 0: Berrido alpaca
    // - 1: Coz Alpaca
    // - 2: Sonido Ambiente

    public void Initialize()
    {
        source = Camera.main.GetComponent<AudioSource>();
    }

    public void PlaySound(int audio)
    {
            source.PlayOneShot(audios[audio]);
    }

    public void PlaySound(int audio,int mode)
    {
        switch (mode)
        {
            case 0:
                source.clip = audios[audio];
                source.Play();
                break;
            case 1:
                source.PlayOneShot(audios[audio]);
                break;
            default:
                source.PlayOneShot(audios[audio]);
                break;
        }
        
    }

    public void PlaySound(int audio, AudioSource source)
    {
        source.PlayOneShot(audios[audio]);
    }

    public void PlaySound(int audio, AudioSource source, int mode)
    {
        switch (mode)
        {
            case 0:
                source.clip = audios[audio];
                source.Play();
                break;
            case 1:
                source.PlayOneShot(audios[audio]);
                break;
            default:
                source.PlayOneShot(audios[audio]);
                break;
        }

    }
}
