using UnityEngine;

public class GuardiaAudioManager : MonoBehaviour
{
    public AudioClip[] guardiaAudiosSuelo = new AudioClip[2];
    public AudioClip[] guardiaAudiosBoca = new AudioClip[5];

    public AudioSource boca, suelo;



    public void IdleAudio()
    {
        if (suelo.isPlaying) suelo.Stop();
    }

    public void WalkAudio()
    {
        if (suelo.isPlaying) suelo.Stop();
        suelo.clip = guardiaAudiosSuelo[0];
        suelo.Play();
    }

    public void RunAudio()
    {
        if (suelo.isPlaying) suelo.Stop();
        suelo.clip = guardiaAudiosSuelo[1];
        suelo.Play();
    }

    public void GruntAudio()
    {
        boca.PlayOneShot(guardiaAudiosBoca[0]);
    }

    public void ScareAudio()
    {
        boca.PlayOneShot(guardiaAudiosBoca[1]);
    }

    public void HitAudio()
    {
        boca.PlayOneShot(guardiaAudiosBoca[Random.Range(2,5)]);
    }
   
}
