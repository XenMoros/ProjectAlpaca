using UnityEngine;
using System.Collections;

public class DispensaCajas : MonoBehaviour, IActivable
{
    // Elementos precacheados en inspector
    public GameObject cajaPrefab; // Prefab de la caja a spawnear

    // Variables publicas de control del dispensador
    public float alturaDeSpawn = 20f;
    // GameObject donde guardaremos la Caja que spawneemos
    private GameObject caja;

    public void SetActivationState(bool activateState)
    {
        // Cuando se active
        if (activateState)
        {
            //Si ya hay una caja asociada, destruirla
            if (caja != null)
            {
                Destroy(caja);
            }

            // Instanciar una nueva caja a una altura alturaDeSpawn en vertical del punto de spawn
            caja = Instantiate(cajaPrefab, transform.position + transform.up * alturaDeSpawn, Quaternion.identity);
        }
    }
}
