using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{
    public float tiempoEnWaypoint;

    
    public Vector3 RetornarPosition()
    {
        return transform.position;
    }

    public float RetornarTiempo()
    {
        return tiempoEnWaypoint;
    }
}
