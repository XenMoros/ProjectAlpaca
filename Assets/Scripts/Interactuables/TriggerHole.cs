using UnityEngine;

public class TriggerHole : MonoBehaviour
{    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Caja"))
        {// Si la caja entra en el trigger, informale que esta sobre un agujero
            other.gameObject.GetComponent<CajaScript>().Agujero(transform.position);
        }
    }
}
