using Pathfinding;
using UnityEngine;
using UnityEngine.AI;

public class GuardiaMovement : Enemy
{
    // Elementos precacheados desde inspector
    //public EntradaYSalidaGM gameManager; // Esto será en un futuro un EnemyManager
    public Transform player; // Alpaca
    public IAstarAI agent;
    public Animator guardiaAnimator;
    public WaypointManager waypointManager;
    public AnimationEventGuardia AEGuardia;
    EnemyManager enemyManager;

    // Variables publicas de movimiento
    public float fieldOfView; // Campo de vision del guardia
    public float distanciaVolverPosicion; // Distancia minima para considerar volver al inicio

    // Boleanos de estado
    private bool cegacion = false;

    // Informacion de casteo de Rayos
    private RaycastHit hitInfo;

    // Variables de movimiento
    private Vector3 objective; // Objetivo
    private Vector3 lastPosition; // Posicion inicial
    private Quaternion lastRotation;
    private Quaternion initialRotation; // Rotacion inicial
    private float timerEnEstado;
    private float tiempoEnEstado; // Tiempo que el guardia esta cegado
    public float correrSpeed, andarSpeed;
    bool estabaPatrullando;

    internal enum Estado {SinCambios ,Idle, Patrullando, Volviendo, Buscando, Investigar, Perseguir, Aturdido};

    internal Estado estado, estadoSiguiente;
    
    private void Start()
    {
        estado = Estado.Idle;
        estabaPatrullando = false;
        agent = GetComponent<IAstarAI>();
    }
    private void LateUpdate()
    {
        if (estadoSiguiente != estado && estadoSiguiente != Estado.SinCambios)
        {
            if (estadoSiguiente == Estado.Aturdido || estadoSiguiente == Estado.Idle || estadoSiguiente == Estado.Buscando)
            {
                agent.canMove = false;
                agent.canSearch = false;

                if (estadoSiguiente == Estado.Idle)
                {
                    tiempoEnEstado = waypointManager.RetornarWaypoint().RetornarTiempo();
                }
                else
                {
                    tiempoEnEstado = 5f;
                }
            }
            else if (estado == Estado.Aturdido || estado == Estado.Idle || estado == Estado.Buscando)
            {
                agent.canMove = true;
                agent.canSearch = true; 
            }

            if (estado == Estado.Idle || estado == Estado.Patrullando)
            {
                if (estado == Estado.Patrullando)
                {
                    estabaPatrullando = true;
                }
                else
                {
                    estabaPatrullando = false;
                }

                if(estadoSiguiente != Estado.Patrullando && estadoSiguiente != Estado.Idle)
                {
                    lastPosition = transform.position;
                    lastRotation = transform.rotation;
                }                
            }

            if (estadoSiguiente == Estado.Perseguir || estadoSiguiente == Estado.Investigar || estadoSiguiente == Estado.Patrullando || estadoSiguiente == Estado.Volviendo)
            {
                SetObjective(objective);

                if (estadoSiguiente == Estado.Perseguir)
                {
                    agent.maxSpeed = correrSpeed;
                }
                else
                {
                    agent.maxSpeed = andarSpeed;
                }
                
            }
            
            estado = estadoSiguiente;
            estadoSiguiente = Estado.SinCambios;

            ControlDeAnimaciones();

            timerEnEstado = 0f;

        }
    }

    private void Update()
    {
        timerEnEstado += Time.deltaTime;

        switch (estado)
        {
            case Estado.Idle:

                if (BuscarObjetivo())
                {
                    CambiarEstado(Estado.Perseguir);
                }
                else if (timerEnEstado > tiempoEnEstado && waypointManager.waypointList.Count > 1)
                {
                    waypointManager.AvanzarWaypoint();
                    objective = waypointManager.RetornarWaypoint().RetornarPosition();
                    CambiarEstado(Estado.Patrullando);
                }
                break;
            case Estado.Patrullando:

                if (BuscarObjetivo())
                {

                    CambiarEstado(Estado.Perseguir);
                }
                else if (agent.reachedEndOfPath && !agent.pathPending)
                {
                    if (waypointManager.RetornarWaypoint().RetornarTiempo() >= 0)
                    {
                        CambiarEstado(Estado.Idle);
                    }
                    else
                    {
                        waypointManager.AvanzarWaypoint();
                        objective = waypointManager.RetornarWaypoint().RetornarPosition();
                        SetObjective(objective);
                    }
                }
                break;
            case Estado.Perseguir:

                if (BuscarObjetivo())
                {
                    SetObjective(objective);
                }
                else if (agent.reachedEndOfPath && !agent.pathPending)
                {
                    CambiarEstado(Estado.Buscando);
                }
                break;
            case Estado.Aturdido:
                
                break;
            case Estado.Volviendo:

                if (BuscarObjetivo())
                {
                    CambiarEstado(Estado.Perseguir);
                }
                else if (agent.reachedEndOfPath && !agent.pathPending)
                {
                    if (estabaPatrullando)
                    {
                        objective = waypointManager.RetornarWaypoint().RetornarPosition();
                        CambiarEstado(Estado.Patrullando);
                    }
                    else
                    {
                        transform.rotation = lastRotation;
                        CambiarEstado(Estado.Idle);
                    }
                }
                break;
            case Estado.Investigar:

                if (BuscarObjetivo())
                {
                    CambiarEstado(Estado.Perseguir);
                }
                else if (agent.reachedEndOfPath && !agent.pathPending)
                {
                    CambiarEstado(Estado.Buscando);
                }
                break;
            case Estado.Buscando:
                if (BuscarObjetivo())
                {
                    CambiarEstado(Estado.Perseguir);
                }
                break;

            default:
                break;
        }


    }

    // Marca la posicion position como objetivo del agente
    public void SetObjective(Vector3 position)
    {
        agent.destination = position;
        if (!agent.pathPending)
        {
            agent.SearchPath();
        }
    }

    public void FinalBuscar()
    {
        CambiarEstado(Estado.Volviendo);
        objective = lastPosition;
    }

    public void FinalAturdido()
    {
        if (BuscarObjetivo())
        {
            CambiarEstado(Estado.Perseguir);
        }
        else
        {
            CambiarEstado(Estado.Buscando);
        }
    }

        internal void CambiarEstado(Estado estadoPropuesto)
    {
        if ((int)estadoPropuesto > (int)estadoSiguiente)
        {
            estadoSiguiente = estadoPropuesto;
        }
    }

    public void GuardiaEscucha (Vector3 origen)
    {
        CambiarEstado(Estado.Investigar);
        objective = origen;
    }

    void ControlDeAnimaciones()
    {
        switch(estado)
        {
            case Estado.Aturdido:
                guardiaAnimator.SetTrigger("Aturdido");
                break;
            case Estado.Perseguir:
                guardiaAnimator.SetTrigger("Perseguir");
                break;
            case Estado.Patrullando:
            case Estado.Investigar:
            case Estado.Volviendo:
                guardiaAnimator.SetTrigger("Caminar");
                break;
            case Estado.Buscando:
                guardiaAnimator.SetTrigger("Buscando");
                break;
            case Estado.Idle:
                guardiaAnimator.SetTrigger("Idle");
                break;

        }
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
            //gameManager.Reload();
            
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
        if (collision.gameObject.CompareTag("Escupitajo"))
        {
            CambiarEstado(Estado.Aturdido);
        }
    }

    public override void SetPause(bool state)
    {
        base.SetPause(state);

    }
}
