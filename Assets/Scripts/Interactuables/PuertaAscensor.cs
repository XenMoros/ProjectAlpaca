using UnityEngine;
using System.Collections;

public class PuertaAscensor : Puerta
{
    public Ascensor ascensor;
    public bool posicionArriba;

    private void OnEnable()
    {
        if (ascensor != null)
        {
            if (posicionArriba)
            {
                ascensor.OnReachUp += AbrirPuerta;
            }
            else
            {
                ascensor.OnReachDown += AbrirPuerta;
            }
            
            ascensor.OnLeave += CerrarPuerta;
        }
    }

    private void OnDisable()
    {
        if (ascensor != null)
        {
            if (posicionArriba)
            {
                ascensor.OnReachUp -= AbrirPuerta;
            }
            else
            {
                ascensor.OnReachDown -= AbrirPuerta;
            }
            ascensor.OnLeave -= CerrarPuerta;
        }
    }
}
