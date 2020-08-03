using Pathfinding;
using UnityEngine;

public class GuardiaMovement : Enemy
{
    // Elementos precacheados desde inspector
    //public EntradaYSalidaGM gameManager; // Esto será en un futuro un EnemyManager
    public Transform player; // Posicion de la Alpaca
    public Transform cabeza; // Posicion de la cabeza
    public IAstarAI agent; // Agente de PathFinding
    public Animator guardiaAnimator; // Animator del guardia
    public WaypointManager waypointManager; // Puntos de ruta del guardia
    public AnimationEventGuardia AEGuardia; // Eventos de animacion del guardia


    // Variables publicas de movimiento
    public float fieldOfView; // Campo de vision del guardia
    public float distanciaVolverPosicion; // Distancia minima para considerar volver al inicio
    [Range(0f,100f)] public float distanciaAlerta = 10; // Distancia desde la que el guardia escucha a la alpaca
    public bool debug = false;

    // Informacion de casteo de Rayos
    private RaycastHit hitInfo;

    // Variables de movimiento
    private Vector3 objective; // Objetivo
    private Vector3 lastPosition; // Posicion inicial
    private Quaternion lastRotation; // Ultima Rotacion antes de la persecucion
    private float timerEnEstado; // Timer en el estado actual
    private float tiempoEnEstado; // Tiempo que el guardia esta cegado
    public float correrSpeed, andarSpeed; // Velocidades de movimiento
    bool estabaPatrullando; // Flag de si estaba patrullando o no

    /// <summary>
    /// Enumerador de la maquina de estados. ESTRICA Y ORDENADA, los estados mayores (cuanto mas abajo en la lista) tienen mas prioridad
    /// SinCambios: Estado ficticio que marca que la maquina de estados NO ha de realizar ningun cambio
    /// Idle: Estado de parado
    /// Patrullando: Estado de guardia en patrulla
    /// Volviendo: Estado de guardia volviendo de investigar o buscar a su ultimo punto de idle/patrulla
    /// Buscando: Estado en el que te ha perdido y intenta buscarte EN EL SITIO
    /// Investigar: Estodo de guardia donde va a un punto a investigar un ruido o similares
    /// Perseguir: Estado donde te esta persiguiendo el guardia
    /// Aturdido: Guardia incapacitado temporalmente
    /// </summary>
    internal enum Estado {SinCambios ,Idle, Patrullando, Volviendo, Buscando, Investigar, Perseguir, Aturdido};

    internal Estado estado, estadoSiguiente; // Estados del guarda, el actual de la maquina de estados y el siguiente al que ha de transicionar

    private void OnDrawGizmos()
    {
        if (debug)
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, distanciaAlerta);
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.left, distanciaAlerta);
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, distanciaAlerta);
        }
    }

    private void Start()
    { // Al incio
        estado = Estado.Idle; // El estado inicial es Idle
        estabaPatrullando = false; // No patrulla
        agent = GetComponent<IAstarAI>(); // Set del agente de Pathfinding
    }

    private void LateUpdate() // La actualizacion de estados se realiza en LateUpdate
    {
        if (!pausa && active)
        {// Si no esta pausado y esta activo
            if (estadoSiguiente != estado && estadoSiguiente != Estado.SinCambios)
            {// Si hay algun cambio de estado
                if (estadoSiguiente == Estado.Aturdido || estadoSiguiente == Estado.Idle || estadoSiguiente == Estado.Buscando)
                {// Si el estado siguiente es aturdido, Idle o Buscando, el agente no se mueve
                    agent.canMove = false;
                    agent.canSearch = false;

                    if (estadoSiguiente == Estado.Idle)
                    {// Si es Idle mira el tiempo que ha de estar segun el que informe el waypoint
                        tiempoEnEstado = waypointManager.RetornarWaypoint().RetornarTiempo();
                    }
                    else
                    {// Si no marca 5 segundos como tiempo maximo
                        tiempoEnEstado = 5f;
                    }
                }
                else if (estado == Estado.Aturdido || estado == Estado.Idle || estado == Estado.Buscando)
                {// Si el estado en el que estabas era Aturdido, Idle o Buscando, vuelve a activar el movimiento
                    agent.canMove = true;
                    agent.canSearch = true;
                }

                if (estado == Estado.Idle || estado == Estado.Patrullando)
                {// Si venias de un estado Idle o Patrulla
                    if (estado == Estado.Patrullando)
                    {// Si era patrulla mmarca que estabas en patrulla
                        estabaPatrullando = true;
                    }
                    else
                    {// Sino marca que NO estabas de patrulla
                        estabaPatrullando = false;
                    }

                    if (estadoSiguiente != Estado.Patrullando && estadoSiguiente != Estado.Idle)
                    {// SI el estado siguiento NO es ni idle ni patrulla, guarda la posicion y la rotacion que tienes
                        lastPosition = transform.position;
                        lastRotation = transform.rotation;
                    }
                }

                if (estadoSiguiente == Estado.Perseguir || estadoSiguiente == Estado.Investigar || estadoSiguiente == Estado.Patrullando || estadoSiguiente == Estado.Volviendo)
                {// Si el estado siguiente es Perseguir, Investigar, Patrulla o Volver
                    SetObjective(objective); // Marca el objetivo del PathFinding

                    if (estadoSiguiente == Estado.Perseguir)
                    {// Si toca perseguir pon la velocidad del agente segun correr
                        agent.maxSpeed = correrSpeed;
                    }
                    else
                    {// Sino el agente ira andando
                        agent.maxSpeed = andarSpeed;
                    }

                }

                ControlDeAnimaciones(); // Gestiona el cambio de animaciones

                estado = estadoSiguiente; // Marca el estado actual como ya cambiado

                timerEnEstado = 0f; // Reinicia el tiempo en este estado

            }
            else if (estadoSiguiente == Estado.Investigar)
            {
                SetObjective(objective);
            }

            estadoSiguiente = Estado.SinCambios; 

        }
    }

    private void Update()
    {
        if (!pausa && active)
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
                    else if (FinalCamino())
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
                    else if (FinalCamino())
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
                    else if (FinalCamino())
                    {
                        if (estabaPatrullando)
                        {
                            objective = waypointManager.RetornarWaypoint().RetornarPosition();
                            CambiarEstado(Estado.Patrullando);
                        }
                        else
                        {
                            if(Vector3.Distance(transform.position, waypointManager.RetornarWaypoint().RetornarPosition()) > 2f){
                                SetObjective(waypointManager.RetornarWaypoint().RetornarPosition());
                            }
                            else
                            {
                                CambiarEstado(Estado.Idle);
                                transform.rotation = lastRotation;
                                transform.position = waypointManager.waypointList[waypointManager.waypointActual].transform.position;
                            }

                        }
                    }
                    break;
                case Estado.Investigar:

                    if (BuscarObjetivo())
                    {
                        CambiarEstado(Estado.Perseguir);
                    }
                    else if (FinalCamino())
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
    }

    bool FinalCamino()
    {
        return ((agent.reachedEndOfPath && !agent.pathPending));
    }

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

    internal bool CambiarEstado(Estado estadoPropuesto)
    {
        if ((int)estadoPropuesto > (int)estadoSiguiente)
        {
            estadoSiguiente = estadoPropuesto;
            return true;
        }
        return false;
    }

    public void GuardiaEscucha (Vector3 origen)
    {
        if (CambiarEstado(Estado.Investigar))
        {
            objective = origen;
        }
    }

    void ControlDeAnimaciones()
    {// Gestiona las animaciones
        switch(estadoSiguiente)
        {
            case Estado.Aturdido: // Activa el trigger de Aturdido
                guardiaAnimator.SetTrigger("Aturdido");
                break;
            case Estado.Perseguir: // Activa el trigger de Perseguir
                guardiaAnimator.SetTrigger("Perseguir");
                break;
            case Estado.Patrullando:
            case Estado.Investigar:
            case Estado.Volviendo: // Para estas tres, activa el bool de Caminar
                guardiaAnimator.SetBool("Caminar",true);
                break;
            case Estado.Buscando: // Activa el bool de Buscando
                guardiaAnimator.SetBool("Buscando", true);
                break;
            case Estado.Idle: // Activa el bool de Idle
                guardiaAnimator.SetBool("Idle", true);
                break;
        }

        switch (estado)
        {
            case Estado.Patrullando:
            case Estado.Investigar:
            case Estado.Volviendo: 
                guardiaAnimator.SetBool("Caminar", false);
                break;
            case Estado.Buscando: 
                guardiaAnimator.SetBool("Buscando", false);
                break;
            case Estado.Idle: 
                guardiaAnimator.SetBool("Idle", false);
                break;
        }
    }

    // Funcion de busqueda de objetivo
    private bool BuscarObjetivo()
    {
        Vector3 playerDirection = (player.position - cabeza.position).normalized; // Direccion a la que esta la Alpaca
        float angle = Vector3.Dot(transform.forward, playerDirection); // Angulo entre donde mira el Agente y la Alpaca

        // Si la alpaca esta en angulo de vision
        if (angle >= Mathf.Cos(fieldOfView)) 
        {
            // Lanza un rayo a la alpaca a ver si la ve (puede haber obstaculos en medio
            if (Physics.Raycast(cabeza.position, playerDirection, out hitInfo, 50f))
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
            enemyManager.ReloadLevel();
            
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
        if (collision.gameObject.CompareTag("Escupitajo") && estado != Estado.Aturdido)
        {
            CambiarEstado(Estado.Aturdido);
        }
    }

    public override void AlertarEnemigo(Vector3 position)
    {
        if (Vector3.Distance(position, transform.position) < distanciaAlerta)
        {
            GuardiaEscucha(position);
        }
    }

    public override void SetPause()
    { // Setea la pausa del guardia
        base.SetPause();

        HabilitarGuardia();
    }

    public override void SetActivationState(bool activateState)
    { // Set de el estado de activacion
        base.SetActivationState(activateState);

        HabilitarGuardia();
    }

    void HabilitarGuardia()
    { // Gestiona la valocidad de animaciones del guardia segun la velocidad
        if (!pausa && active)
        {
            agent.canMove = true;
            guardiaAnimator.speed = 1; // Pon la velocidad de animaciones normal
        }
        else
        {
            agent.canMove = false;
            guardiaAnimator.speed = 0; // Para las animaciones
        }
    }
}
