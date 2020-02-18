using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador : MonoBehaviour, IActivable
{
    //public Animator animator;
    public GameObject activacionEnCadenaObj;
    public IActivable activacionEnCadena;
    public Transform finalPosition;
    float speed = 500f;
    

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
            //animator.SetBool("Activado", true)  
            transform.position = Vector3.MoveTowards(transform.position, finalPosition.position, speed * Time.deltaTime);
            //Debug.Log("lol");
        }

        else
        {
            //animator.SetBool("Activado", false);
            transform.position = Vector3.MoveTowards(finalPosition.position, transform.position, speed * Time.deltaTime);

        }

        if (activacionEnCadena != null)
        {
            activacionEnCadena.SetActivationState(activateState);
        }
    }
}
