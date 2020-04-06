using UnityEngine;

public class AlpacaSound : MonoBehaviour
{
    // Elementos precacheados en Inspector
    public AlpacaMovement alpacaMovement;
    public AudioManager audioControll; // Controlador de Audio
    public AudioSource audioSource; // AudioSource de la Alpaca
    public CustomInputManager inputManager;
    public Animator alpacaAnimator;
    public float hearDistance = 4;

    void Update()
    {
        if (!alpacaMovement.pause)
        {
            //if (Input.GetButtonDown("Y"))
            if (inputManager.GetButtonDown("Yell"))
            {
                // Al Berrear, reproducimos el audio 0 desde nuestra source

                alpacaAnimator.SetTrigger("Berreo");
                audioControll.PlaySound(0, audioSource);

                //Ademas alertamos a todos los guardias de donde se ha berreado, en un radio de 100 unidades

                Collider[] possibleEnemiesWhoHeardMe = Physics.OverlapSphere(transform.position, hearDistance, LayerMask.GetMask("Guardia"));
                foreach (Collider enemy in possibleEnemiesWhoHeardMe)
                {
                    enemy.GetComponent<GuardiaMovement>().GuardiaEscucha(transform.position);
                }
            }
            // Al Cocear, reproducimos el audio 1 desde nuestra source
            //if (Input.GetButtonDown("B"))
            if (inputManager.GetButtonDown("Coz"))
            {
                audioControll.PlaySound(1, audioSource);
            }
        }
    }

    public void SetInputManager(CustomInputManager manager)
    {
        inputManager = manager;
    }
}
