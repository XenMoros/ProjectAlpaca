using System.Collections.Generic;
using UnityEngine;

public class EscupitajoAction : MonoBehaviour
{
    // Elementos precacheados en Inspector
    public ParticleSystem escupitajoParticleSystem;
    public List <EscupitajoScript> escupitajos; // Lista de escupitajos disponibles
    public EscupitajoScript escupitajoPrefab; // Prefab de los escupitajos
    public Transform cabeza; // Cabeza de la alpca
    public Transform recamara; // Posicion de la Recamara
    public Animator alpacaAnimator;
    public CustomInputManager inputManager;
    public AlpacaMovement alpacaMovement;

    // Variables publicas de control
    [Range(0.1f,10)]public float tiempoEntreDisparos = 2f;
    [Range(1,10)]public int numeroDisparos = 3;
    public bool autoapuntado = true;

    // Variables internas 
    private int nBala = 0; // Bala siguiente a ser lanzada

    // Variables de autoapuntado
    private Collider[] colliderList;
    private Vector3 direccion;

    // Timers 
    private float timerEntreDisparos = 0;

    private void Start()
    {
        escupitajos = new List<EscupitajoScript>();
        for(int i = 0; i < numeroDisparos; i++)
        {
            escupitajos.Add(Instantiate(escupitajoPrefab, recamara.position,Quaternion.identity,recamara));
        }

        
    }

    void Update()
    {
        if (!alpacaMovement.pause && !(alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Cozeo || alpacaMovement.arrastrando 
            || alpacaAnimator.GetCurrentAnimatorStateInfo(1).IsName("Berreo")))
        {
            // Suma el tiempo de espera
            if (tiempoEntreDisparos > 0)
            {
                timerEntreDisparos -= Time.deltaTime;
            }
            // Si esta el gatillo a fondo y no estas en tiempo de espera
            // Intenta escupir
            if (inputManager.GetAxis("Escupir") == 1 && timerEntreDisparos <= 0)
            {
                //Escupir();
                
                alpacaAnimator.SetTrigger("Escupitajo");
                timerEntreDisparos = tiempoEntreDisparos; // Empieza a contar el tiempo de enfriamiento
            }
        }
    }

    public void Escupir()
    {
        ParticulasEscupitajo();

        if (autoapuntado)
        {
            // Genera una lista de todos los objetos de tipo "Camara Lens, Guardia o BotonPared" que esten en el box de autoapuntado
            colliderList = Physics.OverlapBox(transform.position + transform.forward * 15 + transform.up * 4, new Vector3(5, 4.5f, 15), transform.rotation, LayerMask.GetMask("CameraLens", "BotonPared", "Guardia"), QueryTriggerInteraction.Collide);

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
        {
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
    {
        escupitajoParticleSystem.transform.position = cabeza.position;
        escupitajoParticleSystem.transform.localEulerAngles = transform.localEulerAngles;
        escupitajoParticleSystem.Play();
    }

    public void SetInputManager(CustomInputManager manager)
    {
        inputManager = manager;
    }
}
