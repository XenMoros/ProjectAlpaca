using UnityEngine;

public class LenteScript : Enemy
{
    bool alpacaHit; // Flag de si has visto la Alpacaa
    public bool alerta; // Flag de si esta la camara alerta

    public GameObject cameraLight; //GameObject de la luz de la camara
    public Transform generalCamera; // Posicion de la camara
    public Transform player; //Posicion del jugador
    Vector3 objective; // Posicion del objetivo donde mirar
    public WaypointManager waypointManager; // Waypoints de la camara
    public float rotationSpeed = 20f, maxAnglePerSecond = 30; // Velocidades de rotacion maximas y normales

    float timer = 0; // Tiempo que lleva la camara viendote
    public float tiempoMuerte = 8f; // Tiempo hasta reiniciar el nivel

    private void Start()
    {
        objective = waypointManager.RetornarWaypoint().RetornarPosition(); // Al empezar, el objetivo donde mirar es hacia el primer waypoint
    }

    // Update is called once per frame
    void Update()
    {
        
        if (active && !pausa)
        { // Si la camara esta activa i no en pausa
            if (alerta)
            { // Si esta alerta
                if (alpacaHit)
                { // Si ve a la alpaca
                    generalCamera.LookAt(player.position, Vector3.up); // La camara mira a la alpaca directamente
                    timer += Time.deltaTime; // Cuenta tiempo que te ha visto
                }
                else
                { // Si pierde la alpaca, deja de estar alerta
                    alerta = false;
                }

                if(timer > tiempoMuerte)
                { // Si te ve suficiente tiempo reinicia el nivel
                    enemyManager.ReloadLevel();
                }
            }
            else
            {// Si no esta alerta
                if (alpacaHit)
                { // Si te ve empieza a estar alerta y a contar el tiempo
                    alerta = true;
                    timer = 0;
                }
                else
                {// Si no te ve
                    GirarCamara(); // Gira la camara para mirar a el objetivo
                    if (Vector3.Dot(generalCamera.forward, (objective - generalCamera.position).normalized) > 0.999f)
                    { // Si estas mirando al objetivo, cambia el waypoint al siguiente
                        waypointManager.AvanzarWaypoint();
                        objective = waypointManager.RetornarWaypoint().RetornarPosition();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Escupitajo"))
        { // Si te choca un escupitajo desactiva la camara
            active = false;
            Destroy(this.gameObject);
        }
    }

    public void SetAlpacaHit(bool hit)
    { // Setea el flag AlpacaHit
        alpacaHit = hit;
        if (hit)
        { // Si te esta viendo la luz es roja
            cameraLight.GetComponent<Light>().color = Color.red;
        }
        else
        { // Si no te ve la luz es blanca
            cameraLight.GetComponent<Light>().color = Color.white;
        }
    }

    private void GirarCamara() // Girar la camara gradualmente hacia su objetivo
    {
        // First calculate the look vector as normal

        Vector3 newForward = Vector3.Slerp(generalCamera.forward,(objective - generalCamera.position).normalized, Time.deltaTime * rotationSpeed);

        // Now check if the new vector is rotating more than allowed
        float angle = Vector3.Angle(generalCamera.forward, newForward);
        float maxAngle = maxAnglePerSecond * Time.deltaTime;
        if (angle > maxAngle)
        {
            // It's rotating too fast, clamp the vector
            newForward = Vector3.Slerp(generalCamera.forward, newForward, maxAngle / angle);
        }

        // Assign the new forward to the transform
        generalCamera.forward = newForward;
    }

    public override void SetActivationState(bool activateState)
    { // Setear el estado de activacion
        base.SetActivationState(activateState);
        cameraLight.SetActive(activateState);
    }
}
