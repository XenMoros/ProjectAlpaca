using UnityEngine;

public class AlpacaAudioManager : MonoBehaviour
{
    public AudioClip[] alpacaAudiosBoca = new AudioClip[2];
    public AudioClip[] alpacaAudiosSuelo = new AudioClip[5];
    public AudioClip[] alpacaAudiosDetras = new AudioClip[2];

    public AudioSource boca, suelo, detras;

    public AlpacaMovement alpacaMovement; // Movimiento de la alpaca
    public CustomInputManager inputManager; // Input manager (prod o Debug)
    public Animator alpacaAnimator; // Animator de la alpaca
    LevelManager levelManager; // El level manager

    void Update()
    {
        if (!alpacaMovement.Pause)
        {
            if (inputManager.GetButtonDown("Yell") && !(alpacaMovement.arrastrando
                || alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Stopped
                || alpacaAnimator.GetCurrentAnimatorStateInfo(2).IsName("Escupitajo")
                || alpacaAnimator.GetCurrentAnimatorStateInfo(1).IsName("Berreo")))
            { // Si pulsas el boton de berreo Y no estas ni arrastrando, ni coceando ni haciendo las animaciones de escupitajo o berreo

                alpacaAnimator.SetTrigger("Berreo"); // Activa la animacion de berreo
                YellAudio(); // Reproduce el sonido del yell

                levelManager.AlertarGuardias(transform.position);
            }

            if (inputManager.GetButtonDown("Coz") && alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Stopped
                && alpacaMovement.tipoStopped == AlpacaMovement.TipoStopped.Cozeo)
            {// Al Cocear, reproducimos el audio 1 desde nuestra source
                CozAudio();
            }
        }
    }

    public void SetManagers(CustomInputManager manager, LevelManager levelManageer)
    { // Enlaza el AudioManager (para actores externos)
        inputManager = manager;
        levelManager = levelManageer;
    }

    public void IdleAudio()
    {
        if (suelo.isPlaying) suelo.Stop();
    }

    public void WalkAudio()
    {
        if (suelo.isPlaying) suelo.Stop();
        suelo.clip = alpacaAudiosSuelo[0];
        suelo.Play();
    }

    public void RunAudio()
    {
        if (suelo.isPlaying) suelo.Stop();
        suelo.clip = alpacaAudiosSuelo[1];
        suelo.Play();
    }

    public void ArrastreAudio()
    {
        if (suelo.isPlaying) suelo.Stop();
        suelo.clip = alpacaAudiosSuelo[2];
        suelo.Play();
    }

    public void JumpAudio()
    {
        if (suelo.isPlaying) suelo.Stop();
        suelo.PlayOneShot(alpacaAudiosSuelo[3]);
    }

    public void FallHitAudio()
    {
        suelo.PlayOneShot(alpacaAudiosSuelo[4]);
    }

    public void CozAudio()
    {
        detras.PlayOneShot(alpacaAudiosDetras[0]);
    }

    public void CozHitAudio()
    {
        detras.PlayOneShot(alpacaAudiosDetras[1]);
    }

    public void YellAudio()
    {
        boca.PlayOneShot(alpacaAudiosBoca[0]);
    }

    public void EscupitajoAudio()
    {
        boca.PlayOneShot(alpacaAudiosBoca[1]);
    }

    

}