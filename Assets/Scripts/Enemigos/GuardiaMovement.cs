using UnityEngine;
using UnityEngine.AI;

public class GuardiaMovement : Enemy
{
    // Elementos precacheados desde inspector
    //public EntradaYSalidaGM gameManager; // Esto será en un futuro un EnemyManager
    public Transform player; // Alpaca
    public NavMeshAgent agente; // El pathfinding del agente
    public Animator guardiaAnimator;
    public WaypointManager waypointManager;
    public AnimationEventGuardia AEGuardia;

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
    private Quaternion initialRotation; // Rotacion inicial
    private float timerEnEstado;
    private float tiempoEnEstado; // Tiempo que el guardia esta cegado
    public float correrSpeed, andarSpeed;
    bool estabaPatrullando;

    internal enum Estado {SinCambios ,Idle, Patrullando, Volviendo, Buscando, Investigar, Perseguir, Aturdido};

    internal Estado estado, estadoSiguiente;
    
    private void Start()
    {
        // Captura de condiciones iniciales de transform 
        //initialPosition = transform.position;
        //initialRotation = transform.rotation;

        //animationEvent.time = pararseGuardia.length;
        estado = Estado.Idle;
        estabaPatrullando = false;
    }

    /*private void Update()
    {
        // Si esta cegado
        if (cegacion)
        {         
            agente.isStopped = true;
        }
        // Si no esta cegado
        else
        {
            agente.isStopped = false;

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
    }*/
    private void LateUpdate()
    {
        if (estadoSiguiente != estado && estadoSiguiente != Estado.SinCambios)
        {
            if (estadoSiguiente == Estado.Aturdido || estadoSiguiente == Estado.Idle || estadoSiguiente == Estado.Buscando)
            {
                agente.isStopped = true;

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
                agente.isStopped = false;
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
                }                
            }

            if (estadoSiguiente == Estado.Perseguir || estadoSiguiente == Estado.Investigar || estadoSiguiente == Estado.Patrullando || estadoSiguiente == Estado.Volviendo)
            {
                SetObjective(objective);

                if (estadoSiguiente == Estado.Perseguir)
                {                    
                    agente.speed = correrSpeed;
                }
                else
                {
                    agente.speed = andarSpeed;
                }
                if(estadoSiguiente== Estado.Volviendo)
                {
                    SetObjective(lastPosition);
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
        Debug.Log(estado);
        Debug.Log(estadoSiguiente);
        Debug.Log(timerEnEstado);
        Debug.Log(tiempoEnEstado);
        Debug.Log(agente.hasPath);
        Debug.Log(agente.velocity.sqrMagnitude);

        timerEnEstado += Time.deltaTime;

        switch (estado)
        {
            case Estado.Idle:
                //guardiaAnimator.SetBool("Idle", true);

                if (BuscarObjetivo())
                {
                    //guardiaAnimator.SetBool("Idle", false);
                    CambiarEstado(Estado.Perseguir);
                }
                else if (timerEnEstado > tiempoEnEstado && waypointManager.waypointList.Count > 1)
                {
                    //guardiaAnimator.SetBool("Idle", false);
                    waypointManager.AvanzarWaypoint();
                    objective = waypointManager.RetornarWaypoint().RetornarPosition();
                    CambiarEstado(Estado.Patrullando);
                }
                break;
            case Estado.Patrullando:
                //guardiaAnimator.SetBool("Caminar", true);

                if (BuscarObjetivo())
                {
                    //guardiaAnimator.SetBool("Caminar", false);

                    CambiarEstado(Estado.Perseguir);
                }
                else if (!agente.pathPending)
                {
                    if (agente.remainingDistance <= (agente.stoppingDistance + 2f))
                    {
                        if (!agente.hasPath || agente.velocity.sqrMagnitude <= 0.2f)
                        {
                            if (waypointManager.RetornarWaypoint().RetornarTiempo() >= 0)
                            {
                                //guardiaAnimator.SetBool("Caminar", false);

                                CambiarEstado(Estado.Idle);
                            }
                            else
                            {
                                waypointManager.AvanzarWaypoint();
                                objective = waypointManager.RetornarWaypoint().RetornarPosition();
                                SetObjective(objective);
                            }
                        }
                    }
                }
                break;
            case Estado.Perseguir:
                //guardiaAnimator.SetBool("Perseguir", true);

                if (BuscarObjetivo())
                {
                    SetObjective(objective);
                }
                // Si ha acabado de investigar o te pierde, vuelve a su posicion original
                else if (!agente.pathPending)
                {
                    if (agente.remainingDistance <= (agente.stoppingDistance + 2f))
                    {
                        if (!agente.hasPath || agente.velocity.sqrMagnitude == 0f)
                        {
                            //guardiaAnimator.SetBool("Perseguir", false);
                            CambiarEstado(Estado.Buscando);
                        }
                    }
                }
                break;
            case Estado.Aturdido:
                
                break;
            case Estado.Volviendo:
                //guardiaAnimator.SetBool("Caminar", true);

                if (BuscarObjetivo())
                {
                    CambiarEstado(Estado.Perseguir);
                }
                else if (!agente.pathPending)
                {
                    if (agente.remainingDistance <= (agente.stoppingDistance + 2f))
                    {
                        if (!agente.hasPath || agente.velocity.sqrMagnitude == 0f)
                        {
                            if(estabaPatrullando)
                            {
                                objective = waypointManager.RetornarWaypoint().RetornarPosition();
                                CambiarEstado(Estado.Patrullando);
                            }
                            else
                            {
                                CambiarEstado(Estado.Idle);
                            }
                            
                        }
                    }
                }
                break;
            case Estado.Investigar:
                /*guardiaAnimator.SetBool("Caminar", true);
                guardiaAnimator.SetBool("Perseguir", false);
                guardiaAnimator.SetBool("Idle", false);
                guardiaAnimator.SetBool("Buscando", false);*/

                if (BuscarObjetivo())
                {
                    //guardiaAnimator.SetBool("Caminar", false);
                    CambiarEstado(Estado.Perseguir);
                }
                else if (!agente.pathPending)
                {
                    if (agente.remainingDistance <= (agente.stoppingDistance + 2))
                    {
                        if (!agente.hasPath || agente.velocity.sqrMagnitude == 0f)
                        {
                            //guardiaAnimator.SetBool("Caminar", false);
                            CambiarEstado(Estado.Buscando);
                        }
                    }
                }
                break;
            case Estado.Buscando:
                //guardiaAnimator.SetBool("Buscando", true);
                if (BuscarObjetivo())
                {
                    //guardiaAnimator.SetBool("Buscando", false);
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
        agente.destination = position;
    }

    public void FinalBuscar()
    {
        CambiarEstado(Estado.Volviendo);
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
            Debug.Log("Moriste");
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
