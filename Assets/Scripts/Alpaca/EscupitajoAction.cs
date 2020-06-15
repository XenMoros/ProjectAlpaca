using System.Collections.Generic;
using UnityEngine;

public class EscupitajoAction : MonoBehaviour
{
    // Elementos precacheados en Inspector
    public ParticleSystem escupitajoParticleSystem; // El sistema de particulas del chorro de escupitajo
    public List <EscupitajoScript> escupitajos; // Lista de escupitajos disponibles
    public EscupitajoScript escupitajoPrefab; // Prefab de los escupitajos
    public Transform cabeza; // Cabeza de la alpca
    public Transform recamara; // Posicion de la Recamara
    public Animator alpacaAnimator; // Animator de la alpaca
    public CustomInputManager inputManager; // Input manager 
    public AlpacaMovement alpacaMovement; // Movimiento de la Alpaca

    // Variables publicas de control
    [Range(0.1f,10)]public float tiempoEntreDisparos = 2f; // Tiempo de espera entre disparos
    [Range(1,10)]public int numeroDisparos = 3; // Numero de disparos en la recamara
    public bool autoapuntado = true; // Booleano de control de autoapuntado

    // Variables internas 
    private int nBala = 0; // Bala siguiente a ser lanzada

    // Variables de autoapuntado
    private Collider[] colliderList; // Lista de colliders del autoapuntado
    private Vector3 direccion; // Direccion en la que disparar con autoapuntado

    // Timers 
    private float timerEntreDisparos = 0; // Timer desde el ultimo disparo

    private void Start()
    { // Al empezar genera los escupitajos de la pool en la recamara
        escupitajos = new List<EscupitajoScript>();
        for(int i = 0; i < numeroDisparos; i++)
        {
            escupitajos.Add(Instantiate(escupitajoPrefab, recamara.position,Quaternion.identity,recamara));
        }
    }

    void Update()
    {
        if (!alpacaMovement.pause && !(alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Cozeo || alpacaMovement.arrastrando 
            || alpacaAnimator.GetCurrentAnimatorStateInfo(1).IsName("Berreo") || alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Subida || 
            alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Caida))
        {  // Si no esta en pausa, no estas ni coceando, ni arrastrando, ni enla animacion de berreo ni en el aire mira si puedes escupir

            if (tiempoEntreDisparos > 0)
            { // Avanza (hacia 0) el temporizador entre disparos
                timerEntreDisparos -= Time.deltaTime;
            }
            
            if (inputManager.GetAxis("Escupir") == 1 && timerEntreDisparos <= 0)
            {// Si esta el gatillo a fondo y no estas en tiempo de espera
                alpacaAnimator.SetTrigger("Escupitajo"); // Ejecuta la animacion de escupir
                timerEntreDisparos = tiempoEntreDisparos; // Empieza a contar el tiempo de enfriamiento
            }
        }
    }

    public void Escupir()
    { // Animation event, escupe un proyectil
        ParticulasEscupitajo(); // Activa las particulas de salida del escupitajo

        if (autoapuntado)
        { // Si el autoapuntado esta activado
            // Genera una lista de todos los objetos de tipo "Camara Lens, Guardia o BotonPared" que esten en el box de autoapuntado
            colliderList = Physics.OverlapBox(transform.position + transform.forward * 15 + transform.up * 2, new Vector3(5, 2, 12), transform.rotation, LayerMask.GetMask("CameraLens", "BotonPared", "Guardia"), QueryTriggerInteraction.Collide);

            // Si la lista no esta vacia, apunta automatiacamente al primer elemento
            if (colliderList.Length > 0)
            {
                direccion = colliderList[0].transform.position - cabeza.position;
            }
            // Si no hay nada que autoapuntar delante, dispara de frente
            else
            {
                direccion = transform.forward;
            }
        }
        else
        { // Si no hay autoapuntado dispara de frente
            direccion = transform.forward;
        }

        // Ordena a la bala que toca que dispare en la direccion calculada
        escupitajos[nBala].Escupir(direccion.normalized,cabeza.position);
        nBala += 1; // Recarga a la siguiente bala

        // Si llegas al final de la lista de balas, vuelve a empezar
        if (nBala > (escupitajos.Count - 1))
        {
            nBala = 0;
        }

    }

    void ParticulasEscupitajo()
    { // Activa una iteracion del particle system de las particulas de salida del escupitajo
        escupitajoParticleSystem.Play(true);
    }

    public void SetInputManager(CustomInputManager manager)
    { // Setea el input manager segun entrada, para actores externos
        inputManager = manager;
    }
}
