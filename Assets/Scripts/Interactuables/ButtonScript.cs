using UnityEngine;

public class ButtonScript : Interactuable
{
    // Valores de control del proceso
    public float tiempoActivacion = 5f; // Tiempo que estara activo el boton
    public float timerActivacion = 6f; // Tiempo de activacion del objeto enlazado

    public override void Start()
    {
        base.Start(); // Ejecuta el Start base
        tipoInteractuable = Tipo.BotonPared;
    }

    private void Update()
    {
        if (timerActivacion <= tiempoActivacion)
        { // Si aun no ha terminado el tiempo
            timerActivacion += Time.deltaTime; // Suma el tiempo que ha pasado

            // Si al sumar ha acabado el tiempo, desactiva los objetos asociados
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
