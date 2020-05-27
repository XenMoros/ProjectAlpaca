using UnityEngine;

public class FloorButtonScript : Interactuable
{
    bool activado = false;

    public override void Start()
    {
        base.Start();
    }

    private void OnTriggerExit(Collider other)
    {
        // En caso de salir del boton, desactivar el objeto asociado y revertir animacion
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Guardia") || other.gameObject.CompareTag("Caja"))
        {
            activado = false;
            interactAnimator.SetBool("Active", activado);
            base.Activate(activado);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        // En caso de salir del boton, desactivar el objeto asociado y revertir animacion
        if (!activado && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Guardia") || other.gameObject.CompareTag("Caja")))
        {
            activado = true;
            interactAnimator.SetBool("Active", activado);
            base.Activate(activado);
        }

    }
}
