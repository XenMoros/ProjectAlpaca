using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Elevador : MonoBehaviour, IActivable
{
    //public Animator animator;
    public GameObject activacionEnCadenaObj;
    public IActivable activacionEnCadena;
   
    //public List<Transform> objetivos =  new List<Transform>();
    public WaypointManager waypointManager;
    //int objetivoActual= 0;
    public bool activada = false;
    public float speed;
    

    private void Start()
    {
        if (activacionEnCadenaObj != null)
        {
            activacionEnCadena = activacionEnCadenaObj.GetComponent<IActivable>();
        }
    }

    private void Update()
    {

        if (Vector3.Distance(transform.position, waypointManager.RetornarWaypoint().RetornarPosition()) > 0.2f)
        {
            transform.Translate((waypointManager.RetornarWaypoint().RetornarPosition() - transform.position).normalized * speed * Time.deltaTime,Space.World);
        }
        else
        {
            transform.position = waypointManager.RetornarWaypoint().RetornarPosition();
            if (activada)
            {
                if (waypointManager.waypointActual < waypointManager.waypointList.Count-1)
                {
                    waypointManager.AvanzarWaypoint();
                }
            }
            else
            {
                if (waypointManager.waypointActual > 0)
                {
                    waypointManager.RetrocederWaypoint();
                }
            }
        }
    }

    public void SetActivationState(bool activateState)
    {

        activada = activateState;

        if (activacionEnCadena != null)
        {
            activacionEnCadena.SetActivationState(activateState);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Caja") && other.gameObject.transform.Equals(other.gameObject.GetComponent<CajaScript>().entorno))
        {
            other.gameObject.GetComponent<CajaScript>().SetParent(transform,true);
        }
    }*/

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Caja"))
        {
            other.gameObject.GetComponent<CajaScript>().SetParent(transform, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Caja") && other.gameObject.transform.parent.Equals(transform))
        {
            other.gameObject.GetComponent<CajaScript>().SetParent();
        }
    }
}
