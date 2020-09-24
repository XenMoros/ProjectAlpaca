using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardChangeWaypoint : MonoBehaviour
{
    public WaypointManager defaultWaypointManager, zoneWaypointManager;
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Guardia"))
        {
            other.GetComponent<GuardiaMovement>().CambiarWaypointManager(zoneWaypointManager);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Guardia"))
        {
            other.GetComponent<GuardiaMovement>().CambiarWaypointManager(defaultWaypointManager);
        }
    }
}
