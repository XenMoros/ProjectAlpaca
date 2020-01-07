using UnityEngine;

public class CajaScript : MonoBehaviour
{
    // Variables publicas de control
    public float speed = 15; // Velocidad de movimiento de la caja
    public float tiempoMovimiento = 0.5f;

    // Flags de estado
    private bool expanded = false; // Si el Collider esta expandido
    private bool activateM = false; // Si la caja se mueve

    // Variables de RayCast para parar la caja
    private RaycastHit hit;

    // Objetos para controlar la expansion del Collider en caida 
    private Rigidbody cajaRB; // Rigidbody de la caja
    private BoxCollider cajaBC; // Collider de la caja

    // Timers de control
    float timerMovimiento = 0;

    // Variables privadas
    Vector3 cajadirection = new Vector3(1,0,1); // Direccion de movimiento

    void Start()
    {
        // Capturamos el Rigidbody y el Collider de la caja
        cajaRB = gameObject.GetComponent<Rigidbody>();
        cajaBC = gameObject.GetComponent<BoxCollider>();
    }

    void Update()
    {
        // Restamos al timer de movimiento
        timerMovimiento -= Time.deltaTime;
        // Prueba de mover la caja
        Movimiento();
        
        // Si la caja esta cayendo, aumenta su hitbox de caida
        if (cajaRB.velocity.y < -0.1 && !expanded)
        {
            cajaBC.size = new Vector3(2, 1, 2);
            expanded = true;
        }
        // Si la caja NO esta cayendo reset la escala del collider a tamaño de la caja
        if (expanded && cajaRB.velocity.y >=0)
        {            
            cajaBC.size = new Vector3(1, 1, 1);
            expanded = false;            
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si la caja es coceada
        if (collision.gameObject.CompareTag("Coz"))
        {            
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

            // Calcula si la caja puede ser movida
            CalcularMovimiento();
            // Set el tiempo de movimiento de la caja
            timerMovimiento = tiempoMovimiento;            
        }
        // Si choca con las paredes, parar la caja
        else if (collision.gameObject.CompareTag("Paredes") || collision.gameObject.CompareTag("Escenario"))
        {
            activateM = false;
        }
    }

    // Funcion para asociarle el padre de la Caja
    public bool AsociarPadre(Transform padre)
    {
        try
        {
            this.transform.SetParent(padre);
        }
        catch
        {
            return false;
        }
        return true;

    }

    // Funcion para mover la caja
    void Movimiento()
    {
        // Si la caja puede moverse, muevela en la direcion y con la velocidad undicadas
        if (activateM)
        {
            transform.Translate(cajadirection * speed * Time.deltaTime,Space.World);
        }
        // Desactiva el movimiento al acabar el tiempo asignado
        if (timerMovimiento <= 0)
        {
            activateM = false;
        }
    }

    // Prevision de movimiento
    void CalcularMovimiento()
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
    }
}

 
