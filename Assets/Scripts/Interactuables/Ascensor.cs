using UnityEngine;
using UnityEditor;

public class Ascensor : Elevador
{

    public event ReachLeaveAction OnReachDown, OnReachUp;

    public override void Update()
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
                if (waypointManager.waypointActual == 0 && OnReachDown!=null)
                {
                    OnReachDown();
                }
                else if(waypointManager.waypointActual == 1 && OnReachUp != null)
                {
                    OnReachUp();
                }
            }
        }
    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}