using UnityEngine;

public class CozScript : MonoBehaviour
{
    // Elementos precacheados en Inspector
    public AlpacaMovementNew aMovementNew; //El movimiento de la Alaca

    // GameObject de la Coz
    public GameObject coz;

    // Variables de control de la coz
    public float tiempoCozeo = 0.2f;

    // Timers de control
    float timerCozeo = 0;

    void Start()
    {
        //Capturar la coz y desactivarla
        coz = GameObject.FindGameObjectWithTag("Coz");
        coz.SetActive(false);
    }

    void Update()
    {
        if(timerCozeo > 0) 
        { 
            timerCozeo -= Time.deltaTime;
        }
        // Mirar las acciones de la coz
        CozActivate();
    }

    void CozActivate()
    {
        // Al pulsar la tecla, activar el coceo, parar la Alpaca y empezar el timer
        if (Input.GetButtonDown("B"))
        {
            aMovementNew.cozeando = true;

            coz.SetActive(true);
            timerCozeo = tiempoCozeo;
        }
        //Si no hemos pulsado y se acaba el tiempo, desactivar el coceo y destrabar la Alpaca
        else if (timerCozeo <= 0)
        {
            coz.SetActive(false);
            aMovementNew.cozeando = false;
        }

    }
}
