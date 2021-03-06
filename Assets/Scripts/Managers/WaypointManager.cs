﻿using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public List<WaypointScript> waypointList;
    public int primerWaipoint;
    internal int waypointActual = 0;
    int operacionWaypoint = 1;
    public bool rebote;

    private void Start()
    {
        if(primerWaipoint >=0 && primerWaipoint < waypointList.Count)
        {
            waypointActual = primerWaipoint;
        }
    }

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

    public void RetrocederWaypoint()
    {
        waypointActual -= operacionWaypoint;

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

    public bool SetWaypoint(int i)
    {
        if(i>=0 && i < waypointList.Count)
        {
            waypointActual = i;
            return true;
        }
        return false;
    }

    public int WaypointNumber()
    {
        return waypointActual;
    }

    public WaypointScript RetornarWaypoint()
    {
        return waypointList[waypointActual];
    }
}
