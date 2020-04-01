using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHole : MonoBehaviour
{    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Caja"))
        {
            other.gameObject.GetComponent<CajaScript>().Agujero(transform.position);
        }
    }
}
