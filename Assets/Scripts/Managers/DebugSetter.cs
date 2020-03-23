using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSetter : MonoBehaviour
{
    public bool debugMode;
    AlpacaMovement alpaca;

    private void Start()
    {
        alpaca = GameObject.FindGameObjectWithTag("Player").GetComponent<AlpacaMovement>();
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
