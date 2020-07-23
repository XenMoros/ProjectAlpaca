using System.Collections.Generic;
using UnityEngine;

public class EscupitajoAction : ShootSystem
{
    // Elementos precacheados en Inspector
    public ParticleSystem escupitajoParticleSystem; // El sistema de particulas del chorro de escupitajo
    public Animator alpacaAnimator; // Animator de la alpaca
    public CustomInputManager inputManager; // Input manager 
    public AlpacaMovement alpacaMovement; // Movimiento de la Alpaca

    // Variables publicas de control
    [Range(0.1f,10)]public float tiempoEntreDisparos = 2f; // Tiempo de espera entre disparos

    // Timers 
    private float timerEntreDisparos = 0; // Timer desde el ultimo disparo

    internal override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (!alpacaMovement.pause && !(alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Stopped || alpacaMovement.arrastrando 
            || alpacaAnimator.GetCurrentAnimatorStateInfo(1).IsName("Berreo") || alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Subida || 
            alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Caida))
        {  // Si no esta en pausa, no estas ni coceando, ni arrastrando, ni enla animacion de berreo ni en el aire mira si puedes escupir

            if (tiempoEntreDisparos > 0)
            { // Avanza (hacia 0) el temporizador entre disparos
                timerEntreDisparos -= Time.deltaTime;
            }
            
            if (inputManager.GetAxis("Escupir") == 1 && timerEntreDisparos <= 0)
            {// Si esta el gatillo a fondo y no estas en tiempo de espera
                alpacaAnimator.SetTrigger("Escupitajo"); // Ejecuta la animacion de escupir
                timerEntreDisparos = tiempoEntreDisparos; // Empieza a contar el tiempo de enfriamiento
            }
        }
    }

    public override void Disparar()
    { // Animation event, escupe un proyectil
        ParticulasEscupitajo(); // Activa las particulas de salida del escupitajo

        base.Disparar(); // Dispara el proyectil como en la clase base
    }

    void ParticulasEscupitajo()
    { // Activa una iteracion del particle system de las particulas de salida del escupitajo
        escupitajoParticleSystem.Play(true);
    }

    public void SetInputManager(CustomInputManager manager)
    { // Setea el input manager segun entrada, para actores externos
        inputManager = manager;
    }
}
