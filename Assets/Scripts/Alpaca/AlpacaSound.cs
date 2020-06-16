using UnityEngine;

public class AlpacaSound : MonoBehaviour
{
    // Elementos precacheados en Inspector
    public AlpacaMovement alpacaMovement; // Movimiento de la alpaca
    public AudioManager audioControll; // Controlador de Audio
    public AudioSource audioSource; // AudioSource de la Alpaca
    public CustomInputManager inputManager; // Input manager (prod o Debug)
    public Animator alpacaAnimator; // Animator de la alpaca
    [Range(0f,20f)] public float hearDistance = 10; // Distancia a la que te oyen los enemigos

    void Update()
    {
        if (!alpacaMovement.pause)
        {
            if (inputManager.GetButtonDown("Yell") && !(alpacaMovement.arrastrando
                || alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Cozeo
                || alpacaAnimator.GetCurrentAnimatorStateInfo(2).IsName("Escupitajo")
                || alpacaAnimator.GetCurrentAnimatorStateInfo(1).IsName("Berreo")))
            { // Si pulsas el boton de berreo Y no estas ni arrastrando, ni coceando ni haciendo las animaciones de escupitajo o berreo

                alpacaAnimator.SetTrigger("Berreo"); // Activa la animacion de berreo
                audioControll.PlaySound(0, audioSource); // Reproduce el sonido 0 desde la alpaca

                //Ademas alertamos a todos los guardias de donde se ha berreado segun la distancia de berreo
                Collider[] possibleEnemiesWhoHeardMe = Physics.OverlapSphere(transform.position, hearDistance, LayerMask.GetMask("Guardia"));
                foreach (Collider enemy in possibleEnemiesWhoHeardMe)
                {
                    enemy.GetComponent<GuardiaMovement>().GuardiaEscucha(transform.position); // Alerta a cada guardia
                }
            }
            
            if (inputManager.GetButtonDown("Coz") && alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Cozeo)
            {// Al Cocear, reproducimos el audio 1 desde nuestra source
                audioControll.PlaySound(1, audioSource);
            }
        }
    }

    public void SetInputManager(CustomInputManager manager)
    { // Enlaza el input manager segun entrada (Para actores externos)
        inputManager = manager;
    }

    public void SetAudioManager(AudioManager manager)
    { // Enlaza el AudioManager (para actores externos)
        audioControll = manager;
    }
}
