using UnityEngine;

public class AlpacaMovement : MonoBehaviour
{
    //Input manager a usar (Prod o Debug)
    public CustomInputManager inputManager;

    // Referencias cacheadas a otros Elementos en escena
    public Transform camara; // La camara activa
    public Animator alpacaAnimator; // Animator de la alpaca
    public AlpacaSound sonidos; // Gestor de Sonidos de la alpaca
    public Rigidbody alpacaRB; // Rigidbody de la alpaca

    // Variables publicas de movimiento
    public MovementVariables movimiento; // Serializable de variables respecto movimiento
    public JumpSettings salto; // Serializable de variables respecto salto

    // Buffers de input
    float axisV, axisH; // Storage de los valores de entrada en vertial i horizontal

    // Direcciones de input
    Vector3 targetDirection; // Direccion que queremos que se mueva (en world coordinates)
    Vector3 axisDirection; // Direccion del input

    // Flags de situacion de movimiento
    internal bool onAir = false, arrastrando = false; // Booleanos segun si estas en el aire o arrastrando

    // Timers de control de sucesos
    private float timerSlowMovementOnJump = 9, timerStunCaida = 9, timerBlockDirectionArrastre = 3f;
    private float timerFasesSalto = 999f, timerBotonSalto = 999f;

    // Direcciones de movimiento
    Vector3 direccionMovimiento = Vector3.zero; // Direccion hacia donde mover la alpaca
    Vector2 direccionArrastre = Vector2.zero; // Direccion en la que se arrastran cosas
    private Vector3 planeNormal = Vector3.up; // Orientacion del plano en el que te mueves
    Vector3 lastVelocity = Vector3.zero; // Ultima velocidad de la alpaca (para pausa)

    // Valores para casteo de rayos
    private RaycastHit hitInfo; // Salida de los Physics Raycast

    // Fases de movimiento
    internal enum FaseMovimiento { Subida, Caida, Idle, Andar, Correr, IdleArrastre, Arrastrar, Cozeo };/// Enumerador con las fases
    /// Enumerador de las posibles fases
    /// Subida: Estas saltando y subiendo
    /// Caida: En el aire i cayendo
    /// Idle: En reposo
    /// Andar: Andando
    /// Correr: Corriendo
    /// IdleArrastre: Arrastrando sin movimiento
    /// Arrastrar: Arrastrando
    /// Cozeo: Coceando
    /// </summary>
    internal FaseMovimiento faseMovimiento = FaseMovimiento.Idle, faseMovimientoAnt = FaseMovimiento.Idle; //Fase de movimiento actual i fase de movimiento del frame anterior

    internal float velocidadVertical; // Velocidad vertical que ha de tener la alpaca
    private bool botonSoltado; // Si has soltado el boton de salto
    public LayerMask layerReposicionarSuelo; // LayerMask con las layers en las que la alpaca puede andar por encima


    // Bool de pause
    internal bool pause { get; set; } = true; // Bool de pausa
    bool cambioPausa = false; // Control para cambio de pausa diferido

    // Draw gizmos a descomentar para debug purposes
    void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(transform.position - transform.up * 0.1f, Vector3.one * 0.2f);
        //Gizmos.DrawWireCube(transform.position + transform.up * (1.73f + 0.1f), Vector3.one * 0.2f);
        //Gizmos.DrawWireCube(transform.position, Vector3.right * 0.8f + Vector3.forward * 0.5f + Vector3.up * 0.3f);

        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireCube(transform.position + alpacaBoxCollider.center, alpacaBoxCollider.size * 0.8f);
        //Gizmos.DrawWireCube(transform.position + alpacaBoxCollider.center + transform.forward * (alpacaBoxCollider.size.z / 2f + direccionMovimientoAnt.magnitude * Time.deltaTime * movimiento.speedMultiplier), alpacaBoxCollider.size * 0.9f - Vector3.forward * alpacaBoxCollider.size.z * 0.85f);
    }

    // Al iniciar capturar la main camera y set el drag del Rigidbody a 10
    void Start()
    {
        camara = Camera.main.transform;
        alpacaRB.drag = 10;
    }

    void Update()
    {
        if (!pause)
        {
            // GET AXIS INFO
            axisV = Mathf.Floor(+inputManager.GetAxis("MovementVertical") * 1000f) / 1000f;
            axisH = Mathf.Floor(+inputManager.GetAxis("MovementHorizontal") * 1000f) / 1000f;

            // if input is low, consider it zero
            if (Mathf.Abs(axisV) < 0.05)
            {
                axisV = 0;
            }
            if (Mathf.Abs(axisH) < 0.05)
            {
                axisH = 0;
            }

            // GET COORDINATES OF DESIRED MOVEMENT (targetDirection)
            targetDirection = GetTargetDirection();

            // Si no estas stuneada por ningun motivo
            if (faseMovimiento!= FaseMovimiento.Cozeo && timerStunCaida > salto.stunCaida)
            {
                
                // Saltar al recibir input i no estar en el aire ni arrastrando
                if (inputManager.GetButtonDown("Jump") && !onAir && !arrastrando)
                {
                    onAir = true; // Marca que estaras en el aire
                    faseMovimiento = FaseMovimiento.Subida; // Marca fase de movimiento como subida
                    velocidadVertical = salto.velocidadInicialSalto; // Set de la velocidad vertical
                    timerFasesSalto = -salto.minimoTiempoSalto; // Reinicia el tiempo de salto 
                    timerBotonSalto = 0; // Reinicia el tiempo de boton de salto
                    timerSlowMovementOnJump = 0; // Reinicia el tiempo de slow del movimienoto al saltar
                    botonSoltado = false; // Marca como que estas apretando el boton
                    planeNormal = Vector3.up; // Marca el plano de movimiento horizontal como el XZ
                }

                if (inputManager.GetButtonUp("Jump") && faseMovimiento == FaseMovimiento.Subida)
                { // Al soltar el boton de salto mientras subes marca como que has soltado el boton
                    botonSoltado = true;
                }

                CalculoSalto(); // Calcula la componente vertical del movimiento y el plano sobre el que estas en caso de no estar saltando

                // Solo añade movimiento si estas introduciendo algun imput
                if (axisV != 0 || axisH != 0)
                {
                    alpacaRB.drag = movimiento.movingDrag; // Set el coeficiente de friccion al dinamic0
                    //Si no esta arrastrando, recolocar la alpaca
                    if (!arrastrando)
                    {
                        //Gira la alpaca hacia el movimiento deseado
                        GirarAlpaca();
                    }

                    // Calcular direccion de movimiento en tierra
                    if (!onAir)
                    {
                        if (!arrastrando)
                        { //Si no arrastras, la direccion es hacia donde el input
                            direccionMovimiento = targetDirection;
                            if (Input.GetKey(inputManager.walk))
                            { // SI estas pulsando la tecla de andar, clampea el movimiento para conseguir dicho efecto
                                float clampFactor = Mathf.Sqrt(2) * 3;
                                direccionMovimiento /= clampFactor;
                            }

                            if (direccionMovimiento.magnitude > 0.35f)
                            { // Si hay suficiente movimiento estas corriendo
                                faseMovimiento = FaseMovimiento.Correr;
                            }
                            else
                            { // Si hay menos movimiento estas andando
                                faseMovimiento = FaseMovimiento.Andar;
                            }
                        }
                        else
                        { // Si esta arrastrando siempre anda hacia atras

                            if(timerBlockDirectionArrastre <= movimiento.blockDireccionArrastre)
                            { // Si no se ha bloqueado la direccion de arrastre te mueves segun si el stich se alinea con la alpaca y 
                              // va moviendo progresivamente la direccion arrastre para adequarla a tu input
                                if (Vector3.Dot(targetDirection, -transform.forward) > 0)
                                { // Si te alineas con el culo alpaca te mueves
                                    direccionMovimiento = -transform.forward * Vector3.Dot(targetDirection, -transform.forward) / movimiento.slowArrastre;
                                    direccionArrastre += new Vector2(axisV, axisH);
                                }
                                else
                                { // Si intentas andar hacia alante no te mueves
                                    direccionMovimiento = Vector3.zero;
                                }

                                timerBlockDirectionArrastre += Time.deltaTime; // Aumenta timer de block direccion arrastre

                                if(timerBlockDirectionArrastre > movimiento.blockDireccionArrastre)
                                { // Cuando acaba la fase de entrada de input para direccion arrastre, normaliza esta ultima
                                    direccionArrastre.Normalize();
                                }
                            }
                            else
                            { // Si la direccion esta bloqueada solo te mueves si el stick se alinea con esa direccion
                                Vector2 axisDirection = new Vector2(axisV, axisH);
                                if (Vector2.Dot(axisDirection, direccionArrastre) > 0)
                                { // Si el mando se alinea con la direccion arrastre te mueves
                                    direccionMovimiento = -transform.forward * Vector2.Dot(axisDirection, direccionArrastre) / movimiento.slowArrastre;
                                }
                                else
                                { // Si apuntas al contrario de la direccion arrastre no te mueves
                                    direccionMovimiento = Vector3.zero;
                                }
                            }
                            if(direccionMovimiento != Vector3.zero)
                            {
                                faseMovimiento = FaseMovimiento.Arrastrar;
                            }
                            else faseMovimiento = FaseMovimiento.IdleArrastre; // Set la fase de movimiento como arrastre
                        }
                    }
                    else
                    { // Si estas en el aire la direccion de movimiento es la target direccion pero menos importante
                        direccionMovimiento = targetDirection * salto.axisInfluenceOnAir;
                    }

                }
                else if (!onAir) // Si no hay input i estas en el suelo, el movimiento es zero
                {
                    alpacaRB.drag = movimiento.staticDrag; // Set el coeficiente de friccion al estatico
                    if (!arrastrando) faseMovimiento = FaseMovimiento.Idle; // Si no arrastra la fase es Idle
                    else faseMovimiento = FaseMovimiento.IdleArrastre; // Si arrastra la fase es IdleArrastrando
                    direccionMovimiento = Vector3.zero; // El movimiento es 0
                }

                if (timerSlowMovementOnJump < salto.slowMovementOnJump)
                { // Si acabas de saltar el movimiento se reduce durante un rato
                    direccionMovimiento *= Mathf.Max(1 - salto.reduccionVelocidadSalto, 0);

                    timerSlowMovementOnJump += Time.deltaTime;
                }

            }
            else
            { //Si estas en stun o en cozeo el movimiento es zero
                alpacaRB.AddForce(-alpacaRB.velocity,ForceMode.VelocityChange);
            }

            if (timerStunCaida < salto.stunCaida)
            { // avanza el timer de stun en caso de estar activo
                timerStunCaida += Time.deltaTime;
            }
            GestorAnimacion(); // LLama al gestor de animacion
        }
    }

    private void FixedUpdate()
    { // El movimiento al ser por fisicas se realiza en FixedTime
        float velocity = alpacaRB.velocity.magnitude; // Velocidad actual de la alpaca
        float maxVelocity = movimiento.speedMultiplier; // Maxima velocidad que queremos
        float velocityYChange; // Cambio de velocidad en este paso

        if(arrastrando)
        { // Si esta arrastrando la maxima velocidad es reducida setun paramentros de movimiento
            maxVelocity /= movimiento.slowArrastre;
        }


        if (faseMovimiento == FaseMovimiento.Caida || faseMovimiento == FaseMovimiento.Subida)
        { // Si esta subiendo o cayendo calcula el cambio de velocidad segun la nueva - la actual (para compensarla)
            velocityYChange = velocidadVertical - alpacaRB.velocity.y;
        }
        else
        { // Si no no hay cambios por codigo de la velocidad
            velocityYChange = 0;
        }

        // El cambio en el movimiento sera la proyeccion de la direccion de movimiento sobre el plano del suelo + la velocidad vertical segun salto
        Vector3 newMovement = Vector3.ProjectOnPlane(direccionMovimiento, planeNormal) + Vector3.up * velocityYChange;

        if (velocity > maxVelocity)
        { // Si estamos superando la velocidad maxima calculamos una compoennte extra "break" para compensar ese exceso
            float breakspeed = velocity - maxVelocity;
            Vector3 breakVelocity = alpacaRB.velocity.normalized * breakspeed;
            alpacaRB.AddForce(newMovement - breakVelocity,ForceMode.VelocityChange);
        }
        else
        { // Si no superamos la maxima simplemente aplicamos la peticion como cambio
            alpacaRB.AddForce(newMovement, ForceMode.VelocityChange);
        }
    }

    private void LateUpdate()
    {
        if (cambioPausa)     // La pausa se gestiona a LateUpdate para no hacer acciones al salir del menu de pausa en ese frame
        {
            cambioPausa = false;
            pause = !pause;

            if (pause)
            { // Si entras a la pausa guarda la velocidad que tenias y frena el movimiento
                lastVelocity = alpacaRB.velocity;
                alpacaRB.velocity = Vector3.zero;
                alpacaRB.useGravity = false;
            }
            else
            { // Si sales de la pausa recupera el movimiento que tenias
                alpacaRB.velocity = lastVelocity;
                lastVelocity = Vector3.zero;
                alpacaRB.useGravity = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopMovimientoVertical(collision); // Al chocar intenta parar el movimiento vertical (si es necesario)
    }

    private void OnCollisionStay(Collision collision)
    {
        StopMovimientoVertical(collision); // Al chocar intenta parar el movimiento vertical (si es necesario)
    }

    private void StopMovimientoVertical(Collision collision)
    {
        // Si choca contra algo andable y esta cayendo
        if ((collision.collider.CompareTag("Suelo") ||
            collision.collider.CompareTag("Caja") ||
            collision.collider.CompareTag("Escenario") ||
            collision.collider.CompareTag("Untagged") ||
            collision.collider.CompareTag("Elevador")) &&
            faseMovimiento == FaseMovimiento.Caida)
        {
            faseMovimiento = FaseMovimiento.Idle; // Setea la fase a Idle
            onAir = false; // Dejas de estar en el aire
            velocidadVertical = 0; // Para la velocidad vertical
            if (timerFasesSalto > 0.24f)
            { // Si has saltado suficiente rato, activa el stun de caida
                timerStunCaida = 0;
            }
            timerFasesSalto = 0; // Resetea el timer de salto
        }
    }


    private void CalculoSalto() // Calculo de la velocidad vertical segun la fase de movimiento
    {
        switch (faseMovimiento)
        {
            case FaseMovimiento.Subida:
                // De subida
                if (Physics.BoxCast(transform.position + transform.up * 1.73f, Vector3.right * 0.6f + Vector3.forward * 0.4f + Vector3.up * 0.2f, transform.up, out hitInfo, transform.rotation, 0.1f, layerReposicionarSuelo))
                { // Si chocas con algo para el movimiento y empieaza a caer
                    faseMovimiento = FaseMovimiento.Caida;
                    velocidadVertical = 0;
                    timerFasesSalto = 0;
                }
                else
                { // Si no chocas calcula la velocidad segun la formula (ver ClculoFormula) y avanza los timers pertinentes
                    velocidadVertical = salto.velocidadInicialSalto * CalculoFormula(timerFasesSalto, salto.minimoTiempoSalto);

                    if (!botonSoltado && timerBotonSalto < salto.maximoTiempoBoton)
                    { // Si sigue pulsando el boton de salto y aun esta dentro de tiempo, avanza el timer del boton
                        timerBotonSalto += Time.deltaTime;
                    }
                    else
                    { // Si ha soltado el boton o ha pasado el tiempo de alargar el salto, avanza el timer de salto
                        timerFasesSalto += Time.deltaTime;
                    }

                    if (timerFasesSalto > 0)
                    { // Si ha saltado ya el tiempo neccesario pasa a fase de caida
                        faseMovimiento = FaseMovimiento.Caida;
                    }
                }
                break;
            case FaseMovimiento.Caida: 
                // Durante la caida calcula la velocidad segun la formula, y la limita a una velocidad ternimal (y avanza los timers)
                velocidadVertical = salto.velocidadInicialSalto * CalculoFormula(timerFasesSalto, salto.minimoTiempoSalto);
                if (velocidadVertical < -salto.velocidadTerminalCaida)
                {
                    velocidadVertical = -salto.velocidadTerminalCaida;
                }

                timerFasesSalto += Time.deltaTime;

                break;
            default:
                // Si no estas ni subiendo ni cayendo
                if (!Physics.BoxCast(transform.position + transform.up * 0.4f, Vector3.right * 0.6f + Vector3.forward * 0.4f + Vector3.up * 0.2f, -transform.up, out hitInfo, transform.rotation, 0.6f))
                {// Si no tienes suelo bajo los pies, empieza a caer
                    faseMovimiento = FaseMovimiento.Caida;
                    onAir = true;
                    timerFasesSalto = 0;
                    planeNormal = Vector3.up;
                }
                else
                {// Si si que estas sobre algo
                    velocidadVertical = 0; // La velocidad vertical es 0
                    planeNormal = hitInfo.normal; // Hace set de la orientacion del plano de movimiento
                }
                break;
        }
    }

    // Gestor de los stats de animacion
    private void GestorAnimacion()
    {
        if (faseMovimientoAnt != faseMovimiento)
        {// Si la fase de movimiento ha cambiado, desactiva el flag de la fase anterior y activa la de la actual (teniendo la coz en cuenta)
            if (faseMovimiento == FaseMovimiento.Cozeo)
            {
                alpacaAnimator.SetTrigger(faseMovimiento.ToString());
                alpacaAnimator.SetBool(faseMovimientoAnt.ToString(), false);
            }
            else if(faseMovimientoAnt != FaseMovimiento.Cozeo)
            {
                alpacaAnimator.SetBool(faseMovimiento.ToString(), true);
                alpacaAnimator.SetBool(faseMovimientoAnt.ToString(), false);
            }
            else
            {
                alpacaAnimator.SetBool(faseMovimiento.ToString(), true);
            }
        }

        faseMovimientoAnt = faseMovimiento; // Setea la fase de movimiento anterior

    }

    public void SetParent()
    {// Setear el parent de nuestra alpaca a la raiz de la scene
        transform.parent = null;
    }

    public void SetParent(Transform newParent, bool forzado)
    {// Setear el parent de la alpaca al objeto "newParent"
        if (forzado)
        {// Si es forzado simplemente setea el parent
            transform.parent = newParent.transform;
        }
        else if (transform.parent==null)
        {// Si no es forzado comprueba solo hacer el Set si el parent era la raiz
            transform.parent = newParent.transform;
        }
    }

    private float CalculoFormula(float tiempo, float margen)
    {// Formula para calcular la velocidad del salto, realiza una forma de raiz segun el coeficiente escogido, 
        float result;
        float div = tiempo / margen;

        result = (-1) * (Mathf.Sign(div) * Mathf.Pow((Mathf.Abs(div)), 1 / salto.coeficienteRaiz));

        return result;
    }

    // Encara la alpaca segun input
    private void GirarAlpaca()
    {
        // First calculate the look vector as normal
        Vector3 newForward = Vector3.Slerp(transform.forward, targetDirection.normalized, Time.deltaTime * movimiento.rotationSpeed);

        // Now check if the new vector is rotating more than allowed
        float angle = Vector3.Angle(transform.forward, newForward);
        float maxAngle = movimiento.maxAnglePerSecond * Time.deltaTime;
        if (angle > maxAngle)
        {
            // It's rotating too fast, clamp the vector
            newForward = Vector3.Slerp(transform.forward, newForward, maxAngle / angle);
        }

        // Assign the new forward to the transform
        transform.forward = newForward;
    }

    // Recepcion de input i determinar la direccion del mismo segun la camara
    private Vector3 GetTargetDirection()
    {
        axisDirection = new Vector2(axisV, axisH); // La direccion del eje (input del mando/raton)
        Vector3 camaraDirection = new Vector3(camara.forward.x, 0, camara.forward.z).normalized; // La direccion de la camara en plano XZ
        Vector3 camaraPerpendicular = new Vector3(-camaraDirection.z, 0, camaraDirection.x); // Perpendicular de la camara (en plano XZ)
        return (camaraDirection * axisDirection.x - camaraPerpendicular * axisDirection.y); // La direccion de input segun la camara (plano XZ)
    }

    Vector2 GetAxisDirection(Vector3 a,Vector3 b)
    { // Consigue la direccion de un vector b respecto a un vector a, en coordenadas locales de A y segun el plano que formen

        a = new Vector3(a.x, 0, a.z).normalized; // Normaliza los vectores de entrada sin componente Y
        b = new Vector3(b.x, 0, b.z).normalized;

        float V, H;
        V = Vector3.Dot(a, b); // La componente Vertical es el cosinus del angulo de ambos vectores
        H = Vector3.Cross(a, b).magnitude; // La componente horizontal sera el sinus  de ambos vectores

        return new Vector2(V, H);
    }

    public void SetArrastre(bool arrastre)
    {     // Funcion publica para marcar la dimension de arrastre desde otros actores
        arrastrando = arrastre; // Set si esta arrastrando o no
        alpacaRB.velocity = Vector3.zero; // Detiene la alaca al cambiar de estado
        alpacaRB.drag = movimiento.staticDrag;
        if (arrastrando)
        { // Si empieza a arrastrar setea la direccion de input de arrastre y resetea el timer del block de la direccion
            direccionArrastre = GetAxisDirection(camara.forward, transform.forward);
            timerBlockDirectionArrastre = 0f;
        }
    }

    public void SetPause()
    {// Compureba si ha de activar la pausa del personaje
        if (StaticManager.pause != pause)
        {
            cambioPausa = true;
        }
    }

    public void SetInputManager (CustomInputManager manager)
    { // asigna el inputManager segun la peticion desde otros actores
        inputManager = manager;
    }

}

[System.Serializable]
public class MovementVariables 
{ // Variables de movimiento
    [Range(0, 50f)] public float speedMultiplier = 7.5f; // Velocidad maxima de movimiento
    [Range(1, 10f)] public float slowArrastre = 4; // Factor de slow para el arrastre
    [Range(0, 30f)] public float movingDrag = 5; // Coeficiente de friccion dinamico
    [Range(0, 30f)] public float staticDrag = 10; // Coeficiente de friccion estatico
    [Range(0, 5f)] public float blockDireccionArrastre = 2; // Tiempo hasta que el arrastre se lockea segun direccion de input
    [Range(0, 720)] public float rotationSpeed = 360; // Velocidad de rotacion
    [Range(0, 1440)] public float maxAnglePerSecond = 720; // Maximo angulo/segundo por frame de rotacion
}

[System.Serializable]
public class JumpSettings
{ // Variables de salto
    [Range(0, 10f)] public float velocidadInicialSalto = 7; // Velocidad inicial del salto 
    [Range(0, 1f)] public float maximoTiempoBoton = 0.4f; // Maximo tiempo que cuenta con el boton pulsado
    [Range(0, 1f)] public float minimoTiempoSalto = 0.2f; // Minimo tiempo de subida de un salto
    [Range(0, 30f)] public float velocidadTerminalCaida = 10; // Velocidad terminal de caida de la alpaca
    [Range(0, 10f)] public float coeficienteRaiz = 1.5f; // Coeficiente de la raiz de la formula de velocidad vertical
    [Range(0, 2f)] public float axisInfluenceOnAir = 0.5f; // Influencia del input en el salto
    [Range(0, 1f)] public float slowMovementOnJump = 0.2f; // Tiempo despues de saltar en el que se te baja la velocidad
    [Range(0, 1f)] public float reduccionVelocidadSalto = 0.5f; // Coeficiente de reduccion de input al principio del salto
    [Range(0, 1f)] public float stunCaida = 0.05f; // Tiempo de Stun al caer contra una superficie
}
