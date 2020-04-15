using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LenteScript : Enemy
{
    bool alpacaHit;
    public bool alerta;

    public GameObject cameraLight;
    public Transform generalCamera;
    public Transform player;
    Vector3 objective;
    public WaypointManager waypointManager;
    public float rotationSpeed = 20f, maxAnglePerSecond = 30;

    float timer = 0;
    public float tiempoMuerte = 5f;

    private void Start()
    {
        objective = waypointManager.RetornarWaypoint().RetornarPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (active && !pausa)
        {
            if (alerta)
            {
                if (alpacaHit)
                {
                    generalCamera.LookAt(player.position, Vector3.up);
                    timer += Time.deltaTime;
                }
                else
                {
                    alerta = false;
                }

                if(timer > tiempoMuerte)
                {
                    Debug.Log("Moriste");
                }
            }
            else
            {
                if (alpacaHit)
                {
                    alerta = true;
                    timer = 0;
                }
                else
                {
                    GirarCamara();
                    if (Vector3.Dot(generalCamera.forward, (objective - generalCamera.position).normalized) > 0.999f)
                    {
                        waypointManager.AvanzarWaypoint();
                        objective = waypointManager.RetornarWaypoint().RetornarPosition();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Escupitajo"))
        {
            active = false;
            Destroy(this.gameObject);
        }
    }

    public void SetAlpacaHit(bool hit)
    {
        alpacaHit = hit;
        if (hit)
        {
            cameraLight.GetComponent<Light>().color = Color.red;
        }
        else
        {
            cameraLight.GetComponent<Light>().color = Color.white;
        }
    }

    private void GirarCamara()
    {
        // First calculate the look vector as normal

        Vector3 newForward = Vector3.Slerp(generalCamera.forward,(objective - generalCamera.position).normalized, Time.deltaTime * rotationSpeed);

        // Now check if the new vector is rotating more than allowed
        float angle = Vector3.Angle(generalCamera.forward, newForward);
        float maxAngle = maxAnglePerSecond * Time.deltaTime;
        if (angle > maxAngle)
        {
            // It's rotating too fast, clamp the vector
            newForward = Vector3.Slerp(generalCamera.forward, newForward, maxAngle / angle);
        }

        // Assign the new forward to the transform
        generalCamera.forward = newForward;
    }

    public override void SetActivationState(bool activateState)
    {
        base.SetActivationState(activateState);
        cameraLight.SetActive(activateState);
    }
}
