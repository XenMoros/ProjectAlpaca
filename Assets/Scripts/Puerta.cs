using UnityEngine;

public class Puerta : MonoBehaviour, IActivable
{
    // Elementos necesarios
    MeshRenderer meshRenderer; // El Render de la puerta
    Collider puertaCollider; // El collider de la puerta

    private void Start()
    {
        // Asignamos los elementos en start
        meshRenderer = GetComponent<MeshRenderer>();
        puertaCollider = GetComponent<Collider>();
    }

    // En caso de activacion, cambiar el estado de enable del renderizado y el collider de la puerta
    public void SetActivationState(bool activateState)
    {
        meshRenderer.enabled = !activateState;
        puertaCollider.enabled = !activateState;
    }
}
