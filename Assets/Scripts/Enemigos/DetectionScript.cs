using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    public LenteScript lenteScript;
    public Transform generalCamera;
    public Transform player;

    public float distanceView;
    bool lanzarRayos;
   // public bool alpacaHit;
    RaycastHit hit;
    Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        
        if (lenteScript.active && !lenteScript.pausa && lanzarRayos)
        {
            hit = new RaycastHit();
            direction = (player.position - generalCamera.position).normalized;
            Debug.DrawLine(generalCamera.position, generalCamera.position + direction * distanceView);
            if (Physics.Raycast(generalCamera.position, direction, out hit, distanceView))
            {
                if (hit.collider.name == "Alpaca")
                {
                    Debug.Log("Alpaca");
                    lenteScript.SetAlpacaHit(true);
                }
                else
                {
                    Debug.Log("No Alpaca");
                    lenteScript.SetAlpacaHit(false);
                }
            }
            else
            {
                Debug.Log("No Choque");
                lenteScript.SetAlpacaHit(false);
            }
        }
        else if(lenteScript.active && !lenteScript.pausa)
        {
            lenteScript.SetAlpacaHit(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lanzarRayos = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lanzarRayos = false;
        }
    }

}
