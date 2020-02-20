using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador : MonoBehaviour, IActivable
{
    //public Animator animator;
    public GameObject activacionEnCadenaObj;
    public IActivable activacionEnCadena;
    public Transform finalPosition;
    Vector3 initialPosition;
    public float speed;
    

    private void Start()
    {
        if (activacionEnCadenaObj != null)
        {
            activacionEnCadena = activacionEnCadenaObj.GetComponent<IActivable>();
        }

        initialPosition = transform.position;
    }

    public void SetActivationState(bool activateState)
    {
        if (activateState)
        {
            //animator.SetBool("Activado", true)  
            StartCoroutine(Movimiento(finalPosition.position));
        }

        else
        {
            //animator.SetBool("Activado", false);
            StartCoroutine(Movimiento(initialPosition));

        }

        if (activacionEnCadena != null)
        {
            activacionEnCadena.SetActivationState(activateState);
        }
    }

    public IEnumerator Movimiento(Vector3 objetivo)
    {

        while (Vector3.Distance(objetivo, transform.position) > 0.5f)
        {
            transform.Translate((objetivo-transform.position).normalized * speed * Time.deltaTime);
            yield return null;
        }

        transform.position = objetivo;
    }
}
