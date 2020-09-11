using UnityEngine;

public class AlpacaAudioManager : MonoBehaviour
{
    public AudioClip[] alpacaAudios = new AudioClip[8];

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
        suelo.clip = alpacaAudios[0];
        suelo.Play();
    }

    public void RunAudio()
    {
        if (suelo.isPlaying) suelo.Stop();
        suelo.clip = alpacaAudios[1];
        suelo.Play();
    }


    public void CozAudio()
    {
        detras.PlayOneShot(alpacaAudios[2]);
    }

    public void CozHitAudio()
    {
        detras.PlayOneShot(alpacaAudios[3]);
    }

    public void YellAudio()
    {
        boca.PlayOneShot(alpacaAudios[4]);
    }

    public void JumpAudio()
    {
        if (suelo.isPlaying) suelo.Stop();
        suelo.PlayOneShot(alpacaAudios[5]);
    }

    public void FallHitAudio()
    {
        suelo.PlayOneShot(alpacaAudios[6]);
    }

    public void EscupitajoAudio()
    {
        boca.PlayOneShot(alpacaAudios[7]);
    }

    public void ArrastreAudio()
    {
        if (suelo.isPlaying) suelo.Stop();
        suelo.clip = alpacaAudios[0];
        suelo.Play();
    }

}