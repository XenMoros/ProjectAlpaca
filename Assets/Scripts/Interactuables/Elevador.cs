using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador : MonoBehaviour, IActivable
{
    //public Animator animator;
    public GameObject activacionEnCadenaObj;
    public IActivable activacionEnCadena;
    public List<Transform> objetivos =  new List<Transform>();
    int objetivoActual= 0;
    public bool activada = false;
    public float speed;
    

    private void Start()
    {
        if (activacionEnCadenaObj != null)
        {
            activacionEnCadena = activacionEnCadenaObj.GetComponent<IActivable>();
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, objetivos[objetivoActual].position) > 0.2f)
        {
            transform.Translate((objetivos[objetivoActual].position - transform.position).normalized * speed * Time.deltaTime);
        }
        else if((Vector3.Distance(transform.position, objetivos[objetivoActual].position) <= 0.2f))
        {
            transform.position = objetivos[objetivoActual].position;

            if (objetivoActual < (objetivos.Count - 1) && activada)
            {
                objetivoActual++;
            }
            if(objetivoActual > 0 && !activada)
            {
                objetivoActual--;
            }
        }
    }

    public void SetActivationState(bool activateState)
    {
        if (activateState)
        {
            objetivoActual++;
            activada = true;
        }

        else
        {
            objetivoActual--;
            activada = false;

        }

        if (activacionEnCadena != null)
        {
            activacionEnCadena.SetActivationState(activateState);
        }
    }
}
