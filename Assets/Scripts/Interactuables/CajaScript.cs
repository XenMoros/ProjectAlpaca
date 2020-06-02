using UnityEngine;

public class CajaScript : MonoBehaviour
{
    public Transform entorno;
    public InteractScript interactScript;
    // Variables publicas de control
    public float speed = 15; // Velocidad de movimiento de la caja
    public float tiempoMovimiento = 0.5f;
    //public LayerMask layerBoxCast;
    // Flags de estado
    public bool activateM = false; // Si la caja se mueve

    // Variables de RayCast para parar la caja
    //private RaycastHit hit;
    //RaycastHit boxCastHit;

    // Objetos para controlar la expansion del Collider en caida 
    private Rigidbody cajaRB; // Rigidbody de la caja

    // Timers de control
    float timerMovimiento = 0;

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

    /*private void LateUpdate()
    {
        if (Physics.BoxCast(transform.position, new Vector3(1, 0.5f, 1), -transform.up, out boxCastHit, Quaternion.identity, 1f, layerBoxCast))
        {
            transform.position = new Vector3(transform.position.x, boxCastHit.point.y + 1f, transform.position.z);
            //cajaRB.velocity = Vector3.zero;
        }
    }*/

    private void FixedUpdate()
    {
        Movimiento();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si la caja es coceada
        /*if (collision.gameObject.CompareTag("Coz"))
        {
            Debug.Log(collision.contactCount);
            // Calcula desde donde ha sido cozeada
            if (collision.contacts[0].normal.x != 0)
            {
                if (collision.contacts[0].normal.x >= 0)
                {
                    cajadirection.x = 1;
                    cajadirection.z = 0;
                }

                if (collision.contacts[0].normal.x <= 0)
                {
                    cajadirection.x = -1;
                    cajadirection.z = 0;
                }
            }
            if (collision.contacts[0].normal.z != 0)
            {
                if (collision.contacts[0].normal.z >= 0)
                {
                    cajadirection.x = 0;
                    cajadirection.z = 1;
                }

                if (collision.contacts[0].normal.z <= 0)
                {
                    cajadirection.x = 0;
                    cajadirection.z = -1;
                }
            }

            cajadirection = Vector3.zero;
            ContactPoint[] contacts = new ContactPoint[collision.contactCount];
            collision.GetContacts(contacts);
            foreach(ContactPoint contact in contacts)
            {
                cajadirection += contact.normal;
            }
            cajadirection.y = 0;
            cajadirection.Normalize();

            // Calcula si la caja puede ser movida
            //CalcularMovimiento();
            // Set el tiempo de movimiento de la caja
            activateM = true;
            timerMovimiento = tiempoMovimiento;
        }
        // Si choca con las paredes, parar la caja
        else */
        if (collision.gameObject.CompareTag("Paredes") || collision.gameObject.CompareTag("Escenario"))
        {
            //activateM = false;

            if (interactScript.hitInfoBool)
            {
                interactScript.CompararNormales(collision, this);
            }          
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            Movimiento();
        }
        else if (collision.gameObject.CompareTag("Paredes") || collision.gameObject.CompareTag("Escenario"))
        {
            interactScript.CompararNormales(collision, this);
        }
    }

    // Funcion para mover la caja
    void Movimiento()
    {
        // Si la caja puede moverse, muevela en la direcion y con la velocidad undicadas
        if (activateM)
        {
            //transform.Translate(cajadirection * speed * Time.deltaTime,Space.World);
            //cajaRB.AddForce(cajadirection * speed, ForceMode.Acceleration);
            cajaRB.velocity = cajadirection * speed + Vector3.up * cajaRB.velocity.y;
            // Desactiva el movimiento al acabar el tiempo asignado
            if (timerMovimiento <= 0)
            {
                activateM = false;
            }
        }
        else
        {
            cajaRB.AddForce(-cajaRB.velocity * 0.5f, ForceMode.VelocityChange);
        }
    }

    // Prevision de movimiento
    /*void CalcularMovimiento()
    {
        // Si el movimiento se chocara con algo, y ese algo esta cerca, paralo
        if (Physics.Raycast(transform.position, cajadirection, out hit))
        {
            if (hit.collider != null)
            {
                if (hit.distance > 2.1f)
                {
                    activateM = true;
                }
                if (hit.distance <= 2.1f)
                {
                    activateM = false;
                }
            }
        }    
    }*/

    public void Agujero(Vector3 Hole)
    {
        transform.position = Hole + Vector3.up;
        activateM = false;
    }

    public void SetParent()
    {
        transform.parent = entorno;
    }

    public void SetParent(Transform newParent,bool forzado)
    {
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
    {
        cajadirection = direction;
        if (Mathf.Abs(cajadirection.x) < 0.7f) cajadirection.x = 0;
        cajadirection.y = 0;
        if (Mathf.Abs(cajadirection.z) < 0.7f) cajadirection.z = 0;
        cajadirection.Normalize();
        Debug.Log("Direccion Movimiento: " + cajadirection);
        timerMovimiento = tiempoMovimiento;
        activateM = true;
    }

    public void EliminarMovimiento()
    {
        cajaRB.constraints = RigidbodyConstraints.None;
        cajaRB.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void ActivarMovimiento()
    {
        cajaRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }
}

 
