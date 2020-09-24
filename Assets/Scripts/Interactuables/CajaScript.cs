using UnityEngine;

public class CajaScript : MonoBehaviour
{
    public Transform entorno; // Entorno al que pertenece la caja
    public InteractScript interactScript; // Scropt de interaccion de la Alpaca

    // Variables publicas de control
    public float speed = 15; // Velocidad de movimiento de la caja
    public float tiempoMovimiento = 0.5f; // Tiempo en el que se mueve la caja

    // Flags de estado
    public bool activateM = false; // Si la caja se mueve

    // Objetos para controlar la expansion del Collider en caida 
    private Rigidbody cajaRB; // Rigidbody de la caja

    // Timers de control
    float timerMovimiento = 0; // Tiempo que lleva moviendose la caja

    // Variables privadas
    Vector3 cajadirection = new Vector3(1,0,1); // Direccion de movimiento

    void Start()
    {
        // Capturamos el Rigidbody y el Collider de la caja
        cajaRB = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Restamos al timer de movimiento
        if(activateM) timerMovimiento -= Time.deltaTime;       
    }

    private void FixedUpdate()
    {
        Movimiento(); // Mira si se mueve la caja (con fisicas)
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paredes") || collision.gameObject.CompareTag("Escenario"))
        {
            if (interactScript.hitInfoBool)
            { // Si colisionas con el escenario Estando interactuando con la alpaca, compara las direcciones para desacoplar (si es necesario)
                interactScript.CompararNormales(collision, this);
            }          
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        { // Si estas DENTRO del suelo intenta mover la caja fuera
            Movimiento();
        }
        else if (collision.gameObject.CompareTag("Paredes") || collision.gameObject.CompareTag("Escenario"))
        { // Si es contra paredes o escenario comprueba si ha de parar movimiento
            interactScript.CompararNormales(collision, this);
        }
    }

    // Funcion para mover la caja
    void Movimiento()
    {
        // Si la caja puede moverse, muevela en la direcion y con la velocidad undicadas
        if (activateM)
        {
            cajaRB.velocity = cajadirection * speed + Vector3.up * cajaRB.velocity.y;

            // Desactiva el movimiento al acabar el tiempo asignado
            if (timerMovimiento <= 0)
            {
                activateM = false;
            }
        }
        else
        {// Si ha acabado el movimiento, frena la caja gradualmente
            cajaRB.AddForce(-cajaRB.velocity * 0.5f, ForceMode.VelocityChange);
        }
    }

    public void Agujero(Vector3 Hole)
    { // Si estas en un agujero, para el movimiento y reposiciona la caja
        transform.position = Hole + Vector3.up;
        activateM = false;
    } 

    public void SetParent()
    { // Set el padre de la caja al entorno
        transform.parent = entorno;
    }

    public void SetParent(Transform newParent,bool forzado)
    { // Set el padre de la caja al provisto, mirando si fueras o no. SI no fuerzos solo lo asigna si estaba como hija del entorno
        if (forzado)
        {
            transform.parent = newParent.transform;
        }
        else if (transform.parent.Equals(entorno))
        {
            transform.parent = newParent.transform;
        }
        
    }

    public void PushCaja(Vector3 direction)
    { // Dar un empujon a la caja, cribando componentes pequeñas y haciendolo al final solo en el eje X o el Z
        cajadirection = direction;
        if (Mathf.Abs(cajadirection.x) < 0.7f) cajadirection.x = 0;
        cajadirection.y = 0;
        if (Mathf.Abs(cajadirection.z) < 0.7f) cajadirection.z = 0;
        cajadirection.Normalize(); // Normaliza la direccion
        timerMovimiento = tiempoMovimiento; // Resetea el movimiento de la caja
        activateM = true; // Activa el movimiento de la caja
    }

    public void EliminarMovimiento()
    { // Elimina las constraint de movimiento (cuando dejas la caja)
        cajaRB.constraints = RigidbodyConstraints.None;
        cajaRB.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void ActivarMovimiento()
    { // Bloquea las constraints de movimiento (para cuando arrastras la caja)
        cajaRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }
}

 
