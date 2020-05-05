using UnityEngine;

public class FloorButtonScript : MonoBehaviour
{
    // Elementos precacheados en Inspector
    public Animator floorButton; // El animator del boton
    public GameObject activableObj; // Objeto controlado por el boton

    // Interfaz IActivable del objeto a activar
    private  IActivable activable;
    bool activado = false;

    private void Start()
    {
        // Capturar el elemento IActivable del objeto asociado al boton
        activable = activableObj.GetComponent<IActivable>();

    }

    private void OnTriggerExit(Collider other)
    {
        // En caso de salir del boton, desactivar el objeto asociado y revertir animacion
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Guardia") || other.gameObject.CompareTag("Caja"))
        {
            activado = false;
            floorButton.SetBool("Active", activado);
            activable.SetActivationState(activado);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        // En caso de salir del boton, desactivar el objeto asociado y revertir animacion
        if (!activado && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Guardia") || other.gameObject.CompareTag("Caja")))
        {
            activado = true;
            floorButton.SetBool("Active", activado);
            activable.SetActivationState(activado);
        }

    }
}
