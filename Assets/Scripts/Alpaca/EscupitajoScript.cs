using UnityEngine;

public class EscupitajoScript : MonoBehaviour
{
    // Variables publicas de control
    [Range(0,50)]public float speed = 25f; // Velocidad de las balas
    public GameObject particulas;
    public ParticleSystem hit;

    // Flags de movimiento
    private bool puedeMoverse;
    bool recolocarsePetition;

    // Variables internas de movimiento
    //private Vector3 direccionMovimiento;

    void Start()
    {
        // Asignar todas las balas quietas de inicio
        puedeMoverse = false;
        particulas.SetActive(false);
        recolocarsePetition = false;
    }

    void Update()
    {
        if (puedeMoverse)
        {
            //transform.Translate(transform.forward * speed * Time.deltaTime);
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        if (recolocarsePetition)
        {
            ReColocarse();
            recolocarsePetition = false;
        }
    }

    // Colocar la bala y empezar movimiento
    public void Escupir(Vector3 direccion,Vector3 inicio)
    {
        particulas.SetActive(false);
        transform.forward = direccion.normalized;        
        transform.position = inicio;
        puedeMoverse = true;
        particulas.SetActive(true);
    }

    // Reposicionar la bala en la recamara
    public void ReColocarse()
    {
        particulas.SetActive(false);
        puedeMoverse = false;
        transform.localPosition = Vector3.zero;       
    }

    public void ChangeSpeed(float newVelocity)
    {
        speed = newVelocity;
    }

    public void ChangeDirection(Vector3 newDirection)
    {
        transform.forward = newDirection;
    }


    private void OnCollisionEnter(Collision collision)
    {
        // Al chocar, parar el movimiento y reposicionar las balas
        if (!collision.gameObject.CompareTag("Player"))
        {
            hit.Play(true);
            recolocarsePetition = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Al interaccionar con un boton, parar el movimiento y recolocarse
        if (other.gameObject.CompareTag("BotonPared"))
        {
            hit.Play(true);
            recolocarsePetition = true;
        }       
    }



}
