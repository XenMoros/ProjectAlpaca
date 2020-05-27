using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Interactuable : MonoBehaviour
{
    public Animator interactAnimator;
    public List<GameObject> activatedGO = new List<GameObject>();
    internal List<IActivable> activatedObjScript = new List<IActivable>();

    // Use this for initialization
    public virtual void Start()
    {
        foreach (GameObject objeto in activatedGO)
        {
            activatedObjScript.Add(objeto.GetComponent<IActivable>());
        }

    }

    public virtual void Activate()
    {
        foreach (IActivable activable in activatedObjScript)
        {
            activable.SetActivationState();
        }
    }

    public virtual void Activate(bool state)
    {
        foreach (IActivable activable in activatedObjScript)
        {
            activable.SetActivationState(state);
        }
    }

    public virtual void Activate(int activation)
    {
        foreach (IActivable activable in activatedObjScript)
        {
            activable.SetActivationState(activation);
        }
    }

}
