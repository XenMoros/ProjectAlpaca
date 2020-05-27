using UnityEngine;

public class ButtonScript : Interactuable
{
    // Valores de control del proceso
    public float tiempoActivacion = 5f;
    public float timerActivacion = 6f; // Tiempo de activacion del objeto enlazado

    public override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        // Gestion de espera del boton
        if (timerActivacion <= tiempoActivacion)
        {
            timerActivacion += Time.deltaTime;

            // Si al sumar ha acabado el tiempo, desactiva el objeto asociado
            if (timerActivacion > tiempoActivacion)
            {
                base.Activate(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si activas el boton con una coz o un escupitajo
        if (other.gameObject.CompareTag("Coz") || other.gameObject.CompareTag("Escupitajo"))
        {
            // Iniciar la cuenta atras, activar la animacion del boton i activar el objeto asociado
            timerActivacion = 0;
            interactAnimator.SetTrigger("Pushed");
            base.Activate(true);
        }
    }
}
