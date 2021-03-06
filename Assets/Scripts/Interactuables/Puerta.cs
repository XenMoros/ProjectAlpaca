﻿using UnityEngine;

public class Puerta : MonoBehaviour, IActivable
{

    public BoxCollider puerta1; // Colliders de la puerta
    public BoxCollider puerta2;
    public BoxCollider puerta3;

    public Animator puertaAnimator; // Animador de la puerta
    public DoorAudioManager puertaAudioManager;

    // En caso de activacion, cambiar el estado de enable del renderizado y el collider de la puerta
    public void SetActivationState(bool activateState)
    {
        if (activateState && !puertaAnimator.GetBool("puertaAbierta"))
        { // Al activar la puerta se activa la animacion y desactiva los colliders
            puertaAnimator.SetBool("puertaAbierta", true);
            puertaAudioManager.AbrirPuertaAudio();

            puerta1.enabled = false;
            puerta2.enabled = false;
            puerta3.enabled = false;
        }
        else if(!activateState && puertaAnimator.GetBool("puertaAbierta"))
        { // Al desactivar la puerta se activa la animacion y activa los colliders
            puertaAnimator.SetBool("puertaAbierta", false);
            puertaAudioManager.CerrarPuertaAudio();

            puerta1.enabled = true;
            puerta2.enabled = true;
            puerta3.enabled = true;
        }
    }

    public void SetActivationState()
    { // Activacion de la puerta
        SetActivationState(true);
    }

    public void SetActivationState(int activateState)
    { // Activacion de la puerta
        if (activateState > 0)
        {
            SetActivationState(true);
        }
        SetActivationState(false);
    }

    public void AbrirPuerta()
    {
        SetActivationState(true);
    }

    public void CerrarPuerta()
    {
        SetActivationState(false);
    }
}
