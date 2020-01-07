using UnityEngine;

public class FloorButtonScript : MonoBehaviour
{
    // Elementos precacheados en Inspector
    public Animator FloorButton; // El animator del boton
    public GameObject activableObj; // Objeto controlado por el boton

    // Interfaz IActivable del objeto a activar
    private  IActivable activable;

    private void Start()
    {
        // Capturar el elemento IActivable del objeto asociado al boton
        activable = activableObj.GetComponent<IActivable>();
    }


    private void OnTriggerEnter(Collider other)
    {
        // En caso que la Alpaca, un Guardia o una Caja se suban al boton, activar la animacion y el objeto asociado
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Guardia") || other.gameObject.CompareTag("Caja"))
        {
            FloorButton.SetBool("ActivateButton", true);
            activable.SetActivationState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // En caso de salir del boton, desactivar el objeto asociado y revertir animacion
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Guardia") || other.gameObject.CompareTag("Caja"))
        {
            FloorButton.SetBool("ActivateButton", false);
            activable.SetActivationState(false);
        }
    }
}
