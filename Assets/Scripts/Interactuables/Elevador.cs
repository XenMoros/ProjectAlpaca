using UnityEngine;

public class Elevador : MonoBehaviour, IActivable
{
    public delegate void ReachLeaveAction();
    public event ReachLeaveAction OnLeave, OnReach;

    public WaypointManager waypointManager; // Los puntos de ruta del elevador
    public bool activada = false; // Si esta activado o no
    public float speed; // La velocidad del elevador


    public virtual void Update()
    {
        if (activada)
        { // Si el elevador puede moverse
            if (Vector3.Distance(transform.position, waypointManager.RetornarWaypoint().RetornarPosition()) > 0.2f)
            { // Si estas lejos aun, avanza el elevador
                transform.Translate((waypointManager.RetornarWaypoint().RetornarPosition() - transform.position).normalized * speed * Time.deltaTime, Space.World);
            }
            else
            { // si estas muy cerca colocate en el punto exacto y desactiva el movimiento
                transform.position = waypointManager.RetornarWaypoint().RetornarPosition();
                activada = false;
                OnReach?.Invoke();
            }
        }
    }

    public void SetActivationState(bool activateState)
    { // Si se activa con booleano, en caso de ser false retrocede, sino avanza el elevador
        if (!activada)
        {
            if (activateState)
            {
                SetActivationState();
            }
            else
            {
                OnLeave?.Invoke();
                waypointManager.RetrocederWaypoint();
                activada = true;
            }
        }
    }

    public void SetActivationState()
    { // Cuando se activa el elevador, avanza el waypoint i marca como movimiento activada
        if (!activada)
        {
            waypointManager.AvanzarWaypoint();
            activada = true;
            OnLeave?.Invoke();
        }
    }

    public void SetActivationState(int activateState)
    { // Si se activa con un numero, el ascensor va directo a ese numero (de existir)
        if (!waypointManager.SetWaypoint(activateState))
        {

        }
        else
        {
            OnLeave?.Invoke();
            activada = true;
        }
    }

    public virtual void OnTriggerStay(Collider other)
    { // Si tienes una caja o player encima, esos pasan a ser tus hijos
        if (other.gameObject.CompareTag("Caja") && !other.transform.parent.Equals(transform))
        {
            other.gameObject.GetComponent<CajaScript>().SetParent(transform, false);
        }
        else if(other.gameObject.CompareTag("Player") && !other.transform.parent.Equals(transform))
        {
            other.transform.parent.gameObject.GetComponent<AlpacaMovement>().SetParent(transform, false);
        }
    }

    public virtual void OnTriggerExit(Collider other)
    { // Al salir la caja o player de encima, desacoplatelos
        if (other.gameObject.CompareTag("Caja") && other.transform.parent.Equals(transform))
        {
            other.gameObject.GetComponent<CajaScript>().SetParent();
        }
        else if (other.gameObject.CompareTag("Player") && other.transform.parent.parent.Equals(transform))
        {
            other.transform.parent.gameObject.GetComponent<AlpacaMovement>().SetParent();
        }
    }
}
