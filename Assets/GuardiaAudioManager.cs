using UnityEngine;

public class GuardiaAudioManager : MonoBehaviour
{
    public AudioClip[] guardiaAudios = new AudioClip[5];

    public AudioSource boca, suelo;



    public void IdleAudio()
    {
        if (suelo.isPlaying) suelo.Stop();
    }

    public void WalkAudio()
    {
        if (suelo.isPlaying) suelo.Stop();
        suelo.clip = guardiaAudios[0];
        suelo.Play();
    }

    public void RunAudio()
    {
        if (suelo.isPlaying) suelo.Stop();
        suelo.clip = guardiaAudios[1];
        suelo.Play();
    }

    public void GruntAudio()
    {
        boca.PlayOneShot(guardiaAudios[2]);
    }

    public void ScareAudio()
    {
        boca.PlayOneShot(guardiaAudios[3]);
    }

    public void HitAudio()
    {
        boca.PlayOneShot(guardiaAudios[4]);
    }
   
}
