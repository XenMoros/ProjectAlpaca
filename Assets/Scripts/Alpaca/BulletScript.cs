using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Variables publicas de control
    [Range(0,50)]public float speed = 25f; // Velocidad de las balas
    public GameObject particulas; // Particulas del escupitajo
    public ParticleSystem hit; // Sistema de particulas de choque

    // Flags de movimiento
    private bool puedeMoverse; // Booleano de si se puede mover o no 
    bool recolocarsePetition; // Booleano para pedir que vuelva a racamara

    void Start()
    {
        // Asignar todas las balas quietas de inicio, i con los sistemas de particulas desactivados
        puedeMoverse = false;
        particulas.SetActive(false);
        recolocarsePetition = false;
    }

    void Update()
    {
        if (puedeMoverse && !StaticManager.pause)
        { // Si el escupitajo puede moverse que lo haga.
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        if (recolocarsePetition)
        { // Si se pide la recolocacion, volver a la recamara
            ReColocarse();
            recolocarsePetition = false;
        }
    }

    public void Escupir(Vector3 direccion,Vector3 inicio)
    { // Al escupir
        particulas.SetActive(false); // Desactiva las particulas
        transform.forward = direccion.normalized; // Alinea el escupitajo con la direccion del disparo
        transform.position = inicio; // Traslada el escupitajo al inicio
        puedeMoverse = true; // Marca que se puede mover
        particulas.SetActive(true); // Activa las particulas del escupitajo despues de ponerlo en su sitio
    }

    public void ReColocarse()
    {// Reposicionar la bala en la recamara
        particulas.SetActive(false); // Desactivar las particulas
        puedeMoverse = false; // Marca que NO se mueva
        transform.localPosition = Vector3.zero; // Traslada el escupitajo a la recamara
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        { // Al chocar con qualquier cosa salvo el jugador
            hit.Play(true); // Activa el sistema de particulas del choque
            recolocarsePetition = true; // activa la peticion de recolocar la bala
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BotonPared"))
        {// Al interaccionar con un boton
            hit.Play(true); // Activa el sistema de particulas del choque
            recolocarsePetition = true; // activa la peticion de recolocar la bala
        }       
    }

}
