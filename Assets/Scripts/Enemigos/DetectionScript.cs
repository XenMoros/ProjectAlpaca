using UnityEngine;

public class DetectionScript : MonoBehaviour
{
    public LenteScript lenteScript; // El script de la lente
    public Transform generalCamera; // La posicion de la camara
    public Transform player; // Pa posicion de la alpaca

    public float distanceView; // La distancia de vision
    bool lanzarRayos; // Flag de si intentas mirar o no

    RaycastHit hit; // Salida de los RayCast
    Vector3 direction; // Direccion del RayCast

    // Update is called once per frame
    void Update()
    {
        
        if (lenteScript.active && !lenteScript.pausa && lanzarRayos)
        {
            hit = new RaycastHit(); // Renueva la estructura de Hit
            direction = (player.position - generalCamera.position).normalized; // Calcula en que direccion esta la alpaca
            //Debug.DrawLine(generalCamera.position, generalCamera.position + direction * distanceView);
            if (Physics.Raycast(generalCamera.position, direction, out hit, distanceView))
            { // Si el RayCast choca con algo
                if (hit.collider.name == "Alpaca")
                {// Si es la alpaca, informa que le has dado
                    lenteScript.SetAlpacaHit(true);
                }
                else
                {// Si no es la alpaca di que la has perdido
                    lenteScript.SetAlpacaHit(false);
                }
            }
            else
            {// Si no choca has perdido a la alpaca
                lenteScript.SetAlpacaHit(false);
            }
        }
        else if(lenteScript.active && !lenteScript.pausa)
        {// Si la camara esta activada pero no puedes lanzar rayos, informa de la perdida de la alpaca
            lenteScript.SetAlpacaHit(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {// Activa el lanzamiento de rayos cuando el jugador entre en su rango
        if (other.gameObject.CompareTag("Player"))
        { 
            lanzarRayos = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {// Desactiva el lanzamiento de rayos cuando el jugador entre en su rango
        if (other.gameObject.CompareTag("Player"))
        {
            lanzarRayos = false;
        }
    }

}
