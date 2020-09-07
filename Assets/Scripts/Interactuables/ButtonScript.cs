using UnityEngine;

public class ButtonScript : Interactuable
{
    // Valores de control del proceso
    public float tiempoActividad = 5f; // Tiempo que estara activo el boton
    private float timerActivacion; // Tiempo de activacion del objeto enlazado
    public BotonParedAudioManager botonAudioManager;

    public override void Start()
    {
        base.Start(); // Ejecuta el Start base
        tipoInteractuable = Tipo.BotonPared;
        timerActivacion = tiempoActividad + 1;
    }

    private void Update()
    {
        if (timerActivacion <= tiempoActividad)
        { // Si aun no ha terminado el tiempo
            timerActivacion += Time.deltaTime; // Suma el tiempo que ha pasado

            // Si al sumar ha acabado el tiempo, desactiva los objetos asociados
            if (timerActivacion > tiempoActividad)
            {
                base.Activate(false);
                botonAudioManager.StopTicTacAudio();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            // Si activas el boton con una coz o un escupitajo
            if (other.gameObject.CompareTag("Coz") || other.gameObject.CompareTag("Escupitajo"))
            {
                // Iniciar la cuenta atras, activar la animacion del boton i activar el objeto asociado
                timerActivacion = 0;
                interactAnimator.SetTrigger("Pushed");
                base.Activate(true);
                botonAudioManager.StartTicTacAudio();
            }
        }
    }

}
