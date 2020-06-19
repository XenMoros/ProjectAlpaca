using UnityEngine;
using System.Collections.Generic;

public class Interactuable : MonoBehaviour
{
    public Animator interactAnimator; // Animador del interactuable
    public List<GameObject> activatedGO = new List<GameObject>(); // Lista de objetos a activar
    internal List<IActivable> activatedObjScript = new List<IActivable>(); // Lista de scripts activables
    public Transform interactPosition;

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
        foreach (IActivable activable in activatedObjScript)
        { // Al activar envia la señal a todos los objetos
            activable.SetActivationState();
        }
    }

    public virtual void Activate(bool state)
    {
        foreach (IActivable activable in activatedObjScript)
        { // Al activar envia la señal a todos los objetos
            activable.SetActivationState(state);
        }
    }

    public virtual void Activate(int activation)
    {
        foreach (IActivable activable in activatedObjScript)
        { // Al activar envia la señal a todos los objetos
            activable.SetActivationState(activation);
        }
    }

}
