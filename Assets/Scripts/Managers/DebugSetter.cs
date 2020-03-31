using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSetter : MonoBehaviour
{
    public bool debugMode;
    AlpacaMovement alpaca;
    public CustomInputManager inputManager;

    private void Start()
    {
        GameObject objeto = GameObject.FindGameObjectWithTag("Player");
        alpaca = objeto.GetComponent<AlpacaMovement>();
        alpaca.SetInputManager(inputManager);
        objeto.GetComponent<AlpacaSound>().SetInputManager(inputManager);
        objeto.GetComponent<CozScript>().SetInputManager(inputManager);
        objeto.GetComponent<EscupitajoAction>().SetInputManager(inputManager);
        objeto.GetComponent<InteractScript>().SetInputManager(inputManager);

    }

    private void Update()
    {
        if (debugMode)
        {
            StaticManager.SetPause(false);
            alpaca.SetPause();
        }
        else
        {
            StaticManager.SetPause(true);
            alpaca.SetPause();
        }
    }

}
