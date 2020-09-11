using UnityEngine;
using System.Collections.Generic;

public class Interactuable : MonoBehaviour, IActivable
{
    public Animator interactAnimator; // Animador del interactuable
    public List<GameObject> activatedGO = new List<GameObject>(); // Lista de objetos a activar
    internal List<IActivable> activatedObjScript = new List<IActivable>(); // Lista de scripts activables
    public Transform interactPosition;
    public bool active = true;
    public InteractuableAudioManager interactuableAudioManager;

    public enum Tipo { Palanca, Ascensor, BotonSuelo, BotonPared }
    public Tipo tipoInteractuable;

    public virtual void Start()
    {
        foreach (GameObject objeto in activatedGO)
        { // Al empezar captura todos los scripts de los activables
            activatedObjScript.Add(objeto.GetComponent<IActivable>());
        }

    }

    public virtual void Activate()
    {
        interactuableAudioManager.ActivateAudio();

        foreach (IActivable activable in activatedObjScript)
        { // Al activar envia la señal a todos los objetos
            activable.SetActivationState();
        }
    }

    public virtual void Activate(bool state)
    {
        if(state) interactuableAudioManager.ActivateAudio();
        else interactuableAudioManager.DeactivateAudio();

        foreach (IActivable activable in activatedObjScript)
        { // Al activar envia la señal a todos los objetos
            activable.SetActivationState(state);
        }
    }

    public virtual void Activate(int activation)
    {
        interactuableAudioManager.ActivateAudio();

        foreach (IActivable activable in activatedObjScript)
        { // Al activar envia la señal a todos los objetos
            activable.SetActivationState(activation);
        }
    }

    public void SetActivationState(bool activateState)
    {
        active = activateState;
    }

    public void SetActivationState()
    {
        active = true;
    }

    public void SetActivationState(int activateState)
    {
        if (activateState > 0)
        {
            active = true;
        }
        else active = false;
    }
}
