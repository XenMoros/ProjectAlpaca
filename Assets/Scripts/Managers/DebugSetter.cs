using Assets.Scripts.Managers;
using UnityEngine;

public class DebugSetter : MonoBehaviour
{
    public bool debugMode; // Flag de si activar el debugMode
    AlpacaMovement alpaca; // Referencia a la alpaca
    public CustomInputManager inputManager; // Referencia al InputManager de debug
    public MusicManager audioManager; // Referencia al AudioManager de Debug

    private void Start()
    { // Setea todas las interrelaciones entre scripts necesarias por no existir los managers
        GameObject objeto = GameObject.FindGameObjectWithTag("Player");
        alpaca = objeto.GetComponent<AlpacaMovement>();
        alpaca.SetInputManager(inputManager);
        alpaca.GetComponent<AlpacaAudioManager>().SetManagers(inputManager, null);
        objeto.GetComponent<CozScript>().SetInputManager(inputManager);
        objeto.GetComponent<EscupitajoAction>().SetInputManager(inputManager);
        objeto.GetComponent<InteractScript>().SetInputManager(inputManager);

    }

    private void Update()
    {
        if (debugMode)
        { // Si el debugMode esta activado, quita la pausa
            StaticManager.SetPause(false);
            //alpaca.SetPause();
        }
        else
        { // Si no esta activado pausa todo
            StaticManager.SetPause(true);
            //alpaca.SetPause();
        }
    }

}
