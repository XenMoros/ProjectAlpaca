using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Elevador : MonoBehaviour, IActivable
{
   
    //public List<Transform> objetivos =  new List<Transform>();
    public WaypointManager waypointManager;
    //int objetivoActual= 0;
    public bool activada = false;
    public float speed;
    

    private void Update()
    {
        if (activada)
        {
            if (Vector3.Distance(transform.position, waypointManager.RetornarWaypoint().RetornarPosition()) > 0.2f)
            {
                transform.Translate((waypointManager.RetornarWaypoint().RetornarPosition() - transform.position).normalized * speed * Time.deltaTime, Space.World);
            }
            else
            {
                transform.position = waypointManager.RetornarWaypoint().RetornarPosition();
                activada = false;
            }
        }
    }

    public void SetActivationState(bool activateState)
    {
        if (activateState)
        {
            SetActivationState();
        }
        else
        {
            waypointManager.RetrocederWaypoint();
            activada = true;
        }
        
    }

    public void SetActivationState()
    {
        waypointManager.AvanzarWaypoint();
        activada = true;
    }

    public void SetActivationState(int activateState)
    {
        if (!waypointManager.SetWaypoint(activateState)) Debug.Log("Conmutador '" + gameObject.name + "': Piso " + activateState + " no encontrado.");
        else activada = true;
    }

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
