using UnityEngine;

public class AlpacaMovement : MonoBehaviour
{
    public CustomInputManager inputManager;
    // Referencias cacheadas a otros Elementos en escena
    public Transform camara;
    public BoxCollider alpacaBoxCollider;
    public Animator alpacaAnimator;
    public AlpacaSound sonidos;

    // Variables publicas de movimiento
    public MovementVariables movimiento;
    public JumpSettings salto;

    // Buffers de input
    float axisV, axisH;

    // Direcciones de input
    Vector3 targetDirection;
    Vector3 axisDirection;

    // Flags de situacion de movimiento
    internal bool onAir = false, arrastrando = false;

    // Timers de control de sucesos
    private float timerSlowMovementOnJump = 9, timerStunCaida = 9;
    private float timerFasesSalto = 999f, timerBotonSalto = 999f;

    // Direcciones de movimiento
    Vector3 direccionMovimiento = Vector3.zero;
    Vector3 direccionMovimientoAnt = Vector3.zero;

    // Valores para casteo de rayos
    private RaycastHit hitInfo;

    // Valor interno de escalado al modificar velocidad en el aire
    private float escaladoMovimientoEnAire;

    // Propiedades del Salto
    internal enum FaseMovimiento { Subida, Caida, Idle, Andar, Correr, Arrastrar, Cozeo };
    internal FaseMovimiento faseMovimiento = FaseMovimiento.Idle, faseMovimientoAnt = FaseMovimiento.Idle;

    internal float velocidadVertical, velocidadEntradaFaseFrenado;
    private bool botonSoltado;
    public LayerMask layerReposicionarSuelo;


    //boleano de pause
    internal bool pause { get; set; } = true;
    bool cambioPausa = false;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up * 0.1f, Vector3.one * 0.2f);
        Gizmos.DrawWireCube(transform.position + transform.up * (1.73f + 0.1f), Vector3.one * 0.2f);
        Gizmos.DrawWireCube(transform.position, Vector3.right * 0.8f + Vector3.forward * 0.5f + Vector3.up * 0.3f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + alpacaBoxCollider.center, alpacaBoxCollider.size * 0.8f);
        Gizmos.DrawWireCube(transform.position + alpacaBoxCollider.center + transform.forward * (alpacaBoxCollider.size.z / 2f + direccionMovimientoAnt.magnitude * Time.deltaTime * movimiento.speedMultiplier), alpacaBoxCollider.size * 0.9f - Vector3.forward * alpacaBoxCollider.size.z * 0.85f);

    }
    void Start()
    {
        camara = Camera.main.transform;
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
                //if (Input.GetButtonDown("A") && !onAir && !arrastrando)
                if (inputManager.GetButtonDown("Jump") && !onAir && !arrastrando)
                {
                        onAir = true;
                    faseMovimiento = FaseMovimiento.Subida;
                    velocidadVertical = salto.velocidadInicialSalto;
                    timerFasesSalto = -salto.minimoTiempoSalto;
                    timerBotonSalto = 0;
                    timerSlowMovementOnJump = 0;
                    botonSoltado = false;
                }

                //if (Input.GetButtonUp("A") && faseMovimiento == FaseMovimiento.Subida)
                if (inputManager.GetButtonUp("Jump") && faseMovimiento == FaseMovimiento.Subida)
                {
                    botonSoltado = true;
                }

                CalculoSalto();

                // ONLY MODIFI DIRECTION IF AXIS IS != 0
                if (axisV != 0 || axisH != 0)
                {

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
                            if (direccionMovimiento.magnitude > 0.35f)
                            {
                                faseMovimiento = FaseMovimiento.Correr;
                            }
                            else
                            {
                                faseMovimiento = FaseMovimiento.Andar;
                            }
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
                            faseMovimiento = FaseMovimiento.Arrastrar;
                        }
                    }
                    else //En el aire la direccion es la anterior mas una modificacion segun input
                    {

                        direccionMovimiento = direccionMovimientoAnt * 0.998f + targetDirection * salto.axisInfluenceOnAir * Time.deltaTime;
                        if (direccionMovimiento.magnitude > direccionMovimientoAnt.magnitude * salto.maximaAcelAire)
                        {
                            if (direccionMovimientoAnt.magnitude > 0.1)
                            {
                                escaladoMovimientoEnAire = (direccionMovimientoAnt.magnitude * salto.maximaAcelAire) / direccionMovimiento.magnitude;
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
                    faseMovimiento = FaseMovimiento.Idle;
                    direccionMovimiento = Vector3.zero;
                }

                // Comprueba si tienes algo enmedio del movimiento para evitar chocar i entrar dentro de un obstaculo
                //quitando dicha componente del movimiento
                if (Physics.BoxCast(transform.position + alpacaBoxCollider.center, alpacaBoxCollider.size / 2 * 0.9f - Vector3.forward * alpacaBoxCollider.size.z / 2 * 0.6f, direccionMovimiento.normalized, out hitInfo, transform.rotation, alpacaBoxCollider.size.z / 2))
                {
                    Vector3 proyeccion = Vector3.Project(direccionMovimiento, hitInfo.normal);
                    direccionMovimiento -= proyeccion;
                }

                //Mover la alpaca
                if (timerSlowMovementOnJump < salto.slowMovementOnJump)
                {
                    direccionMovimiento *= Mathf.Max(1 - salto.reduccionVelocidadSalto * Time.deltaTime, 0);

                    timerSlowMovementOnJump += Time.deltaTime;
                }
                transform.Translate((movimiento.speedMultiplier * direccionMovimiento + Vector3.up * velocidadVertical) * Time.deltaTime, Space.World);
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
            GestorAnimacion();
        }
    }


    private void LateUpdate()
    {
        switch (faseMovimiento)
        {
            case FaseMovimiento.Subida:

                break;
            case FaseMovimiento.Caida:

                break;
            default:
                if (Physics.BoxCast(transform.position + transform.up * 0.5f, Vector3.right * 0.6f + Vector3.forward * 0.4f + Vector3.up * 0.2f, -transform.up, out hitInfo, transform.rotation, 0.7f, layerReposicionarSuelo))
                {
                    transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
                    velocidadVertical = 0;
                    //GirarVerticalAlpaca(hitInfo.normal);
                }
                break;
        }

        if (cambioPausa)
        {
            cambioPausa = false;
            pause = !pause;
        }
    }

    private void CalculoSalto()
    {

        switch (faseMovimiento)
        {
            case FaseMovimiento.Subida:
                if (Physics.BoxCast(transform.position + transform.up * 1.73f, Vector3.right * 0.6f + Vector3.forward * 0.4f + Vector3.up * 0.2f, transform.up, out hitInfo, transform.rotation, 0.1f, layerReposicionarSuelo))
                {
                    faseMovimiento = FaseMovimiento.Caida;
                    velocidadVertical = 0;
                }
                else
                {
                    velocidadVertical = salto.velocidadInicialSalto * CalculoFormula(timerFasesSalto, salto.minimoTiempoSalto);

                    if (!botonSoltado && timerBotonSalto < salto.maximoTiempoBoton)
                    {
                        timerBotonSalto += Time.deltaTime;
                    }
                    else
                    {
                        timerFasesSalto += Time.deltaTime;
                    }

                    if (timerFasesSalto > 0)
                    {
                        faseMovimiento = FaseMovimiento.Caida;
                    }
                }
                break;
            case FaseMovimiento.Caida:
                if (Physics.BoxCast(transform.position + transform.up * 0.5f, Vector3.right * 0.6f + Vector3.forward * 0.4f + Vector3.up * 0.2f, -transform.up, out hitInfo, transform.rotation, 0.7f, layerReposicionarSuelo))
                {
                    transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
                    faseMovimiento = FaseMovimiento.Idle;
                    onAir = false;
                    velocidadVertical = 0;
                    timerStunCaida = 0;
                }
                else
                {
                    velocidadVertical = salto.velocidadInicialSalto * CalculoFormula(timerFasesSalto, salto.minimoTiempoSalto);
                    if (velocidadVertical < -salto.velocidadTerminalCaida)
                    {
                        velocidadVertical = -salto.velocidadTerminalCaida;
                    }
                }
                timerFasesSalto += Time.deltaTime;
                break;
            default:
                if (!Physics.BoxCast(transform.position + transform.up * 0.4f, Vector3.right * 0.6f + Vector3.forward * 0.4f + Vector3.up * 0.2f, -transform.up, out hitInfo, transform.rotation, 0.6f))
                {
                    faseMovimiento = FaseMovimiento.Caida;
                    onAir = true;
                    timerFasesSalto = 0;
                }
                else
                {
                    velocidadVertical = 0;
                }
                break;
        }

    }

    private void GestorAnimacion()
    {
        if (faseMovimientoAnt != faseMovimiento)
        {
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

        faseMovimientoAnt = faseMovimiento;

    }

    private float CalculoFormula(float tiempo, float margen)
    {
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

    public void SetPause()
    {
        if (StaticManager.pause != pause)
        {
            cambioPausa = true;
        }
    }

    public void SetInputManager (CustomInputManager manager)
    {
        inputManager = manager;
    }

}

[System.Serializable]
public class MovementVariables
{
    [Range(0, 10f)] public float speedMultiplier = 7.5f;
    [Range(1, 10f)] public float slowArrastre = 4;
    [Range(0, 720)] public float rotationSpeed = 360;
    [Range(0, 1440)] public float maxAnglePerSecond = 720;
}

[System.Serializable]
public class JumpSettings
{
    [Range(0, 10f)] public float velocidadInicialSalto = 7;
    [Range(0, 1f)] public float maximoTiempoBoton = 0.4f;
    [Range(0, 1f)] public float minimoTiempoSalto = 0.2f;
    [Range(0, 30f)] public float velocidadTerminalCaida = 15;
    [Range(0, 10f)] public float coeficienteRaiz = 1.5f;
    [Range(0, 2f)] public float axisInfluenceOnAir = 1f;
    [Range(0, 5f)] public float maximaAcelAire = 1.2f;
    [Range(0, 1f)] public float slowMovementOnJump = 0.2f;
    [Range(0, 20)] public float reduccionVelocidadSalto = 2;
    [Range(0, 1f)] public float stunCaida = 0.05f;
}
