using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public List<WaypointScript> waypointList;
    int waypointActual = 0;
    int operacionWaypoint = 1;
    public bool rebote;

    public void AvanzarWaypoint()
    {
        waypointActual += operacionWaypoint;

        if (rebote)
        {
            if (waypointActual <= 0)
            {
                operacionWaypoint = 1;
                waypointActual = 0;
            }
            else if (waypointActual >= waypointList.Count - 1)
            {
                operacionWaypoint = -1;
                waypointActual = waypointList.Count - 1;
            }
        }
        else
        {
            if (waypointActual >= waypointList.Count)
            {
                waypointActual = 0;
            }
        }

        
    }

    public WaypointScript RetornarWaypoint()
    {
        return waypointList[waypointActual];
    }
}
