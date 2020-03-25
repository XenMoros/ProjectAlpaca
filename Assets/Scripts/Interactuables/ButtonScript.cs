﻿using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Elementos precacheados en Inspector
    public Animator buttonAnimator; // Animator del boton
    public GameObject activableObj; // Objeto enlazado a activar por el boton

    // Valores de control del proceso
    public float tiempoActivacion = 5f;
    public float timerActivacion = 6f; // Tiempo de activacion del objeto enlazado

    // Interfaz IActivable del objeto asociado
    private IActivable activateObj;


    private void Start()
    {
        // Capturar la interfaz IActivable del objeto enlazado
        activateObj = activableObj.GetComponent<IActivable>();
    }

    private void Update()
    {
       // Gestion de espera del boton
       ManageTimer();        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si activas el boton con una coz o un escupitajo
        if (other.gameObject.CompareTag("Coz") || other.gameObject.CompareTag("Escupitajo"))
        {
            // Iniciar la cuenta atras, activar la animacion del boton i activar el objeto asociado
            timerActivacion = 0;
            buttonAnimator.SetTrigger("Pushed");
            activateObj.SetActivationState(true);
        }
    }

    // Funcion de control del tiempo de activacion
    void ManageTimer()
    {
        // Suma tiempo mientras este activo
        if (timerActivacion <= tiempoActivacion)
        {
            timerActivacion += Time.deltaTime;

            // Si al sumar ha acabado el tiempo, desactiva el objeto asociado
            if (timerActivacion > tiempoActivacion)
            {
                activateObj.SetActivationState(false);
            }
        }
        
    }
}
