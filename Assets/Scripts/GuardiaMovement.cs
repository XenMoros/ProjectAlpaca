using UnityEngine;
using UnityEngine.AI;

public class GuardiaMovement : MonoBehaviour
{
    // Elementos precacheados desde inspector
    public EntradaYSalidaGM gameManager; // GameManager para reiniciar el nivel
    public Transform player; // Alpaca
    public NavMeshAgent agente; // El pathfinding del agente

    // Variables publicas de movimiento
    public float fieldOfView; // Campo de vision del guardia
    public float distanciaVolverPosicion; // Distancia minima para considerar volver al inicio

    // Boleanos de estado
    private bool cegacion = false;

    // Informacion de casteo de Rayos
    private RaycastHit hitInfo;

    // Variables de movimiento
    private Vector3 objective; // Objetivo
    private Vector3 initialPosition; // Posicion inicial
    private Quaternion initialRotation; // Rotacion inicial
    private readonly float tiempoCegacion = 5f; // Tiempo que el guardia esta cegado

    // Timers de uso
    private float timerCegacion = 6f;

    private void Start()
    {
        // Captura de condiciones iniciales de transform 
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        // Si esta cegado
        if (cegacion)
        {
            // Aumentar el timer
            timerCegacion += Time.deltaTime;
            // Si ya has pasado toda la cegacion, dejas de estar cegado para el siguiente frame
            if (timerCegacion > tiempoCegacion)
            {
                agente.isStopped = false;
                cegacion = false;
            }
        }
        //Si no esta cegadp
        else
        { 
            //Intenta buscar objetivo
            if (BuscarObjetivo())
            {
                // Si encuentra un objetivo (te ve o oye) marca dicha posicion como nuevo objetivo
                SetObjective(objective);
            }

            // Si ha acabado de investigar o te pierde, vuelve a su posicion original
            if (!agente.pathPending)
            {

                if (agente.remainingDistance <= (agente.stoppingDistance + 2f))
                {

                    if (!agente.hasPath || agente.velocity.sqrMagnitude == 0f)
                    {
                        // Si esta lejos de su posicion original, marca como objetivo la misma
                        if (Vector3.Distance(agente.destination, initialPosition) > distanciaVolverPosicion)
                        {
                            SetObjective(initialPosition);
                        }
                        // Si ya ha llegado a su posicion original simplemente vuelve a mirar hacia donde miraba al principio
                        else
                        {
                            transform.rotation = initialRotation;
                        }

                    }
                }
            }
        }
    }

    // Marca la posicion position como objetivo del agente
    public void SetObjective(Vector3 position)
    {
        agente.destination = position;
    }

    // Funcion de busqueda de objetivo
    private bool BuscarObjetivo()
    {
        Vector3 playerDirection = (player.position - transform.position).normalized; // Direccion a la que esta la Alpaca
        float angle = Vector3.Dot(transform.forward, playerDirection); // Angulo entre donde mira el Agente y la Alpaca

        // Si la alpaca esta en angulo de vision
        if (angle >= Mathf.Cos(fieldOfView)) 
        {
            // Lanza un rayo a la alpaca a ver si la ve (puede haber obstaculos en medio
            if (Physics.Raycast(transform.position, playerDirection, out hitInfo, 50f))
            {

                if (hitInfo.collider.CompareTag("Player"))
                {
                    // Si si la ve marca como objetivo la alpaca, y se encara para mirar a la Alpaca
                    objective = player.position;
                    transform.forward = new Vector3(playerDirection.x,0,playerDirection.z);
                    return true; // Retorna como que SI ha encontrado a la alpaca
                }
            }
        }
        // En caso de que la alpaca no este en angulo de vision o tenga obstaculos, NO encuentra a la Alpaca
        return false;
    }

    // Gestion de colisiones
    private void OnCollisionEnter(Collision collision)
    {
        // Si atrapa a la alpaca reinicia el nivel (de momento si toca, se tendra que mimar mas)
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.Reload();
        }
        // En caso de chocar con una caja que le caiga desde Arriba, el Agente muere
        if (collision.gameObject.CompareTag("Caja"))
        {
            if (collision.GetContact(0).normal == Vector3.down)
            {
                Destroy(this.gameObject);
            }
        }
        // En caso de que le escupas y no este cegado, parar el agente y empezar la cuenta del cegado
        if (collision.gameObject.CompareTag("Escupitajo") && !cegacion)
        {
            timerCegacion = 0;
            cegacion = true;
            agente.isStopped = true;
        }
    }
}
