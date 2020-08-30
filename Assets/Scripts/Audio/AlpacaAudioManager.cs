using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AlpacaAudioManager : AudioController
{
    public AudioClip[] alpacaAudios = new AudioClip[8];

    public AlpacaMovement alpacaMovement; // Movimiento de la alpaca
    public CustomInputManager inputManager; // Input manager (prod o Debug)
    public Animator alpacaAnimator; // Animator de la alpaca
    LevelManager levelManager; // El level manager

    internal override void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        base.Awake();
    }

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
        if (audioSource.isPlaying) audioSource.Stop();
    }

    public void WalkAudio()
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = alpacaAudios[0];
        audioSource.Play();
    }

    public void RunAudio()
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = alpacaAudios[1];
        audioSource.Play();
    }


    public void CozAudio()
    {
        audioSource.PlayOneShot(alpacaAudios[2]);
    }

    public void CozHitAudio()
    {
        audioSource.PlayOneShot(alpacaAudios[3]);
    }

    public void YellAudio()
    {
        audioSource.PlayOneShot(alpacaAudios[4]);
    }

    public void JumpAudio()
    {
        audioSource.PlayOneShot(alpacaAudios[5]);
    }

    public void FallHitAudio()
    {
        audioSource.PlayOneShot(alpacaAudios[6]);
    }

    public void EscupitajoAudio()
    {
        audioSource.PlayOneShot(alpacaAudios[7]);
    }

    public void ArrastreAudio()
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = alpacaAudios[0];
        audioSource.Play();
    }

}