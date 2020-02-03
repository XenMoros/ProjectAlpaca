using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador : MonoBehaviour, IActivable
{
    public Animator animator;
    public GameObject activacionEnCadenaObj;
    public IActivable activacionEnCadena;

    private void Start()
    {
        if (activacionEnCadenaObj != null)
        {
            activacionEnCadena = activacionEnCadenaObj.GetComponent<IActivable>();
        }
    }

    public void SetActivationState(bool activateState)
    {
        if (activateState)
        {
            animator.SetBool("Activado", true);            
        }

        else
        {
            animator.SetBool("Activado", false);
        }

        if (activacionEnCadena != null)
        {
            activacionEnCadena.SetActivationState(activateState);
        }
    }
}
