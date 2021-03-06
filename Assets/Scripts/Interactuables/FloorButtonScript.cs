﻿using System.ComponentModel.Design;
using UnityEngine;

public class FloorButtonScript : Interactuable
{
    bool activado = false; // Flag de si esta activado el boton

    public override void Start()
    {
        base.Start(); // Base start
        tipoInteractuable = Tipo.BotonSuelo;
    }

    private void OnTriggerExit(Collider other)
    {
        // En caso de salir del boton, desactivar los objetos asociados y revertir animacion
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Guardia") || other.gameObject.CompareTag("Caja"))
        {
            activado = false;
            interactAnimator.SetBool("Active", activado);
            base.Activate(activado);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (active)
        {
            // En caso de estar en el boton, activar los objetos asociados y revertir animacion
            if (!activado && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Guardia") || other.gameObject.CompareTag("Caja")))
            {
                activado = true;
                interactAnimator.SetBool("Active", activado);
                base.Activate(activado);
            }
        }

    }

}
