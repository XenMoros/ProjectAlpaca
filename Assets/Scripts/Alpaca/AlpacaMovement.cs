using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AlpacaMovement : MonoBehaviour
{
    // Referencias cacheadas a otros Elementos en escena
    public Transform camara;
    public Rigidbody alpacaRigidbody;
    public Animator alpacaAnimator;

    // Buffers de input
    float axisV, axisH;

    // Direcciones de input
    Vector3 targetDirection;
    Vector3 axisDirection;

    // Valores de movimiento
    public MovementVariables movimiento;
    public JumpSettings salto;    

    // Flags de situacion de movimiento
    internal bool onAir = true, cozeando = false, arrastrando = false;

    // Timers de control de sucesos
    private float timerSlowMovementOnJump = 9, timerStunCaida=9;

    // Direcciones de movimiento
    Vector3 direccionMovimiento = Vector3.zero;
    Vector3 direccionMovimientoAnt = Vector3.zero;

    // Valores capturados on Start
    private float massaIncial;

    // Valores para casteo de rayos
    RaycastHit hitInfo;


    // Valor interno de escalado al modificar velocidad en el aire
    private float escaladoMovimientoEnAire;

    public void Reset()
    {
        alpacaRigidbody = GetComponent<Rigidbody>();
        camara = Camera.main.transform;
    }
    private void Start()
    {
        massaIncial = alpacaRigidbody.mass;
    }

    void Update()
    {

        // GET AXIS INFO
        axisV = Mathf.Floor(+Input.GetAxis("LS_v") * 1000f) / 1000f;
        axisH = Mathf.Floor(+Input.GetAxis("LS_h") * 1000f) / 1000f;

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

        //Comprovar si estas en el aire
        onAir = ComprovarCaida();

        // Si no estas stuneada por ningun motivo
        if (!cozeando && timerStunCaida > salto.stunCaida)
        {

            // Saltar mediante fisicas al recibir input i no estar en el aire ni arrastrando
            if (Input.GetButtonDown("A") && !onAir && !arrastrando)
            {
                alpacaRigidbody.AddForce(new Vector3(0, 1, 0) * salto.fuerzaSalto, ForceMode.Impulse);
                timerSlowMovementOnJump = 0;
            }

            // ONLY MODIFI DIRECTION IF AXIS IS != 0
            if (axisV != 0 || axisH != 0)
            {

                alpacaAnimator.SetBool("Moviendose", true);

                //Si no esta arrastrando, recolocar la alpaca
                if (!arrastrando)
                {
                    //Gira la alpaca hacia el movimiento deseado
                    GirarAlpaca();
                }

                // Calcular direccion de movimiento en tierra
                if (!onAir)
                {
                    //Si no arrastras, la direccion es acia alante
                    if (!arrastrando)
                    {
                        direccionMovimiento = transform.forward;
                        direccionMovimiento *= targetDirection.magnitude;
                    }
                    else //Si esta arrastrando siempre anda hacia atras
                    {
                        direccionMovimiento = -transform.forward;

                        if (Vector3.Dot(targetDirection, -transform.forward) > 0)
                        {
                            direccionMovimiento *= targetDirection.magnitude * Vector3.Dot(targetDirection, -transform.forward) / movimiento.slowArrastre;
                        }
                        else
                        {
                            direccionMovimiento *= 0;
                        }

                    }
                }
                else //En el aire la direccion es la anterior mas una modificacion segun input
                {

                    direccionMovimiento = direccionMovimientoAnt + targetDirection * salto.axisInfluenceOnAir * Time.deltaTime;
                    if (direccionMovimiento.magnitude > direccionMovimientoAnt.magnitude * salto.maximaAcelAire)
                    {

                        if (direccionMovimientoAnt.magnitude > 0.1) 
                        {
                           escaladoMovimientoEnAire  = (direccionMovimientoAnt.magnitude * salto.maximaAcelAire) / direccionMovimiento.magnitude;
                        }
                        else
                        {
                            escaladoMovimientoEnAire = 1 / salto.maximaAcelAire;
                        }
                        direccionMovimiento.Scale(new Vector3(escaladoMovimientoEnAire, escaladoMovimientoEnAire, escaladoMovimientoEnAire));
                    }
                }

            }
            else if (!onAir) // Si no hay input i estas en el suelo, el movimiento es zero
            {
                direccionMovimiento = Vector3.zero;
                alpacaAnimator.SetBool("Moviendose", false);
            }

            // Comprueba si tienes algo enmedio del movimiento para evitar chocar i entrar dentro de un obstaculo
            //quitando dicha componente del movimiento
            if (Physics.Raycast(transform.position + new Vector3(0, -transform.localScale.y / 2.1f, 0), direccionMovimiento.normalized, out hitInfo, 1.5f))
            { 
                Vector3 proyeccion = Vector3.Project(direccionMovimiento, hitInfo.normal);
                direccionMovimiento -= proyeccion;
            }

            //Mover la alpaca
            if (timerSlowMovementOnJump < salto.slowMovementOnJump)
            {
                direccionMovimiento *= Mathf.Max(1- salto.reduccionVelocidadSalto *Time.deltaTime,0);

                timerSlowMovementOnJump +=Time.deltaTime;
            }
            transform.Translate(movimiento.speedMultiplier * direccionMovimiento * Time.deltaTime, Space.World);
            direccionMovimientoAnt = direccionMovimiento;
        }
        else //Si estas en stun el movimiento es zero
        {
            direccionMovimientoAnt = Vector3.zero;
        }

        if (timerStunCaida < salto.stunCaida)
        {
            timerStunCaida += Time.deltaTime;
        }
    }

    // Comprovacion de si estas en el aire
    private bool ComprovarCaida()
    {
        // Compureba si la velocidad de caida por el motor de fisicas es significativa
        if(alpacaRigidbody.velocity.y > 0.005 || alpacaRigidbody.velocity.y < -0.005)
        {
            // Si estas cayendo la gravedad es modificada segun input
            if(alpacaRigidbody.velocity.y < 0)
            {
                alpacaRigidbody.mass =  salto.massaCaida;
            }
            return true;
        }

        // Si ya no caes pero justo aterrizas, empezar un pequeño stun de caida
        if (onAir)
        {
            timerStunCaida = 0;
        }
        // Ademas reiniciar la gravedad a la normal
        alpacaRigidbody.mass = massaIncial;
        return false;
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
        axisDirection = new Vector2(axisV, axisH);
        Vector3 camaraDirection = new Vector3(camara.forward.x, 0, camara.forward.z).normalized;
        Vector3 camaraPerpendicular = new Vector3(-camaraDirection.z, 0, camaraDirection.x);
        return (camaraDirection * axisDirection.x - camaraPerpendicular * axisDirection.y);
    }
    
    // Funcion publica para marcar la dimension de arrastre desde otros actores
    public void SetArrastre(bool arrastre)
    {
        arrastrando = arrastre;
    }

    public void SetPause(bool state)
    {

    }
}

[System.Serializable]
public class MovementVariables
{
    [Range(0, 50f)] public float speedMultiplier = 10;
    [Range(1, 10f)] public float slowArrastre = 4;
    [Range(0, 720)] public float rotationSpeed = 360;
    [Range(0, 1440)] public float maxAnglePerSecond = 720;
}

[System.Serializable]
public class JumpSettings
{
    [Range(0, 2000f)] public float fuerzaSalto = 10;
    [Range(0, 1f)] public float axisInfluenceOnAir = 0.5f;
    [Range(0, 5f)] public float maximaAcelAire = 1.2f;
    [Range(0, 1f)] public float slowMovementOnJump = 0.3f;
    [Range(0, 20)] public float reduccionVelocidadSalto = 7;
    [Range(100f, 500f)] public float massaCaida = 18.5f;
    [Range(0, 1f)] public float stunCaida = 0.05f;
}
