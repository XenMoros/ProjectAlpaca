using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionReminder : MonoBehaviour
{
    public SpriteRenderer renderizador;
    public bool interaccion,arrastre;
    
    // Update is called once per frame
    void Update()
    {
        if(interaccion || arrastre)
        {
            renderizador.enabled = true;
        }
        else if(renderizador.enabled == true)
        {
            renderizador.enabled = false;
        }
    }

    public void SetArrastre(bool estado)
    {
        arrastre = estado;
    }

    public void SetInteraccion(bool estado)
    {
        interaccion = estado;
    }
}
