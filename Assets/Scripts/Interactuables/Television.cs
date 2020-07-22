using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : MonoBehaviour, IActivable
{
    public MeshRenderer pantalla;
    public bool estado;

    void Start()
    {
        pantalla.enabled = estado;
    }

    public void SetActivationState(bool activateState)
    {
        estado = activateState;
        ActualizarEstado();
    }

    public void SetActivationState()
    {
        estado = true;
        ActualizarEstado();
    }

    public void SetActivationState(int activateState)
    {
        if (activateState > 0)
        {
            estado = true;
        }
        else
        {
            estado = false;
        }
        ActualizarEstado();
    }

    private void ActualizarEstado()
    {
        pantalla.enabled = estado;
    }
}
