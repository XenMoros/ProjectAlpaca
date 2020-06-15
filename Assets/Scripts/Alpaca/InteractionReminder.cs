using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionReminder : MonoBehaviour
{
    public SpriteRenderer renderizador; // Renderer del reminder
    public bool interaccion,arrastre; // Booleanos de poder interaccionar o arrastrar
    
    // Update is called once per frame
    void Update()
    {
        if(interaccion || arrastre)
        { // Si algun booleano esta activo, activa el renderizador
            renderizador.enabled = true;
        }
        else if(renderizador.enabled == true)
        { // Sino desactivalo si estaba activado
            renderizador.enabled = false;
        }
    }

    public void SetArrastre(bool estado)
    { // Setea el booleano de arrastre
        arrastre = estado;
    }

    public void SetInteraccion(bool estado)
    { // Setea el booleano de interaccionar
        interaccion = estado;
    }
}
