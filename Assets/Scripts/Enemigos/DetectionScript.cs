using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    public LenteScript lenteScript;
    public Transform generalCamera;
    public Transform player;
    bool lanzarRayos;
   // public bool alpacaHit;
    RaycastHit hit;
    Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        direction = player.position - generalCamera.position;
        Debug.DrawRay(generalCamera.position, direction * 13, Color.red);
        HitAlpaca();
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

    void HitAlpaca()
    {
        if (lanzarRayos)
        {
            if (Physics.Raycast(generalCamera.position, direction, out hit))
            {
                if (hit.collider.name == "Alpaca")
                {
                    lenteScript.SetAlpacaHit(true);
                   // alpacaHit = true;
                }
                else
                {
                    lenteScript.SetAlpacaHit(false);
                   // alpacaHit = false;
                }
            }
        }
    }
}
