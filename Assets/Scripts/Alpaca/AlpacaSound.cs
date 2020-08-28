﻿using Assets.Scripts.Managers;
using UnityEngine;

public class AlpacaSound : MonoBehaviour
{
    // Elementos precacheados en Inspector
    public AlpacaMovement alpacaMovement; // Movimiento de la alpaca
    public AlpacaAudioManager audioControll; // Controlador de Audio
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
                audioControll.YellAudio(); // Reproduce el sonido 0 desde la alpaca

                //Ademas alertamos a todos los guardias de donde se ha berreado segun la distancia de berreo
                /*Collider[] possibleEnemiesWhoHeardMe = Physics.OverlapSphere(transform.position, hearDistance, LayerMask.GetMask("Guardia"));
                foreach (Collider enemy in possibleEnemiesWhoHeardMe)
                {
                    enemy.GetComponent<GuardiaMovement>().GuardiaEscucha(transform.position); // Alerta a cada guardia
                }*/
                levelManager.AlertarGuardias(transform.position);
            }
            
            if (inputManager.GetButtonDown("Coz") && alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Stopped
                && alpacaMovement.tipoStopped == AlpacaMovement.TipoStopped.Cozeo)
            {// Al Cocear, reproducimos el audio 1 desde nuestra source
                audioControll.CozAudio();
            }
        }
    }

    public void SetManagers(CustomInputManager manager, LevelManager levelManageer)
    { // Enlaza el AudioManager (para actores externos)
        inputManager = manager;
        levelManager = levelManageer;
    }
}
