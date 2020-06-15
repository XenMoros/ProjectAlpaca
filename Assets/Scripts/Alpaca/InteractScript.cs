using UnityEngine;

[RequireComponent(typeof(AlpacaMovement))]
public class InteractScript : MonoBehaviour
{

    // Referencias precacheadas en inspector de otros GameObjects
    public Transform entorno; // Entorno de los objetos interactuables
    public AlpacaMovement alpacaMovement; // Script de movimiento de la Alpaca
    public CustomInputManager inputManager; // Input manager del momento (prod o Debug)
    public InteractionReminder interactReminder; // Punto de recordatorio de interaccion posible

    // Valores para casteo de rayos
    LayerMask cajaLayerMask; // Layer de las cajas
    RaycastHit hitInfo; // Informacion de los hits del Raycast
    public bool hitInfoBool; // booleano de si ha hecho hit para uso de actores externos
    private bool canInteract;

    public void Reset()
    {
        alpacaMovement = GetComponent<AlpacaMovement>(); // Al añadirse a la alpaca busca el script de movimiento automaticamente
    }

    private void Start()
    {
        // LayerMask solo con los objetos tipo "Caja" para el Raycast del acople de cajas
        cajaLayerMask = LayerMask.GetMask("Caja");
        canInteract = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!alpacaMovement.pause)
        {
            // En caso de estar en influencia de una caja y no estar en el aire
            if (other.CompareTag("ArrastreCaja") && !alpacaMovement.onAir)
            { 
                if (other.transform.parent.parent.gameObject != this.gameObject)
                {
                    //Compureba que mires a la caja
                    if (Physics.Raycast(transform.position + new Vector3(0, transform.localScale.y / 4f, 0), transform.forward, out hitInfo, 5f, cajaLayerMask, QueryTriggerInteraction.Ignore))
                    { // Si tienes la caja delante
                        other.GetComponentInParent<CajaScript>().ActivarMovimiento(); // Activa el movimiento de la caja
                        hitInfoBool = true; // Flag de que miras a la caja
                        if (hitInfo.collider.gameObject == other.transform.parent.gameObject)
                        { // Si el objeto con el que choca el rayo casteado coincide con el objeto del trigger
                            interactReminder.SetArrastre(true); // Muestra el reminder de interaccion

                            if (inputManager.GetButton("Interact"))
                            {// Si mantienes la X te acopla la caja
                                AcoplarCaja(other);
                            }
                        }
                        else
                        { // Si el objeto que miras NO es el del trigger no muestres el reminder de interaccion
                            interactReminder.SetArrastre(false);
                        }
                    }
                    else
                    { // SI no miras la caja desactiva el reminder de interaccion Y el flag de mirar a la caja
                        interactReminder.SetArrastre(false);
                        hitInfoBool = false;
                    }
                }
                else
                { // si ya estas arrastrando la caja desactiva el reminder de interaccion
                    interactReminder.SetArrastre(false);
                }
                
                if (!inputManager.GetButton("Interact"))
                { // Si sueltas la X te desacopla la caja con reset de caida Y elimina el movimiento de la caja
                    other.GetComponentInParent<CajaScript>().EliminarMovimiento();
                    DesacoplarCaja(other, true);
                }
            }
            
            else if (other.CompareTag("ArrastreCaja") && alpacaMovement.arrastrando)
            {// En caso de pasar a estar cayendo mientras arrastras una caja, desacoplar la misma
                DesacoplarCaja(other, false);
            }

            if (other.CompareTag("Interactuable") && !alpacaMovement.arrastrando)
            { // Si lo que tienes es una palanca/conmutador
                interactReminder.SetInteraccion(true); // Muestra el reminder de interaccion

                if (inputManager.GetButton("Interact") && canInteract)
                { // Si pulsas X activar el interactuable
                    other.gameObject.GetComponent<Interactuable>().Activate();
                    canInteract = false;
                }

                if (!inputManager.GetButton("Interact") && !canInteract)
                {
                    canInteract = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!alpacaMovement.pause)
        {
            
            if (other.CompareTag("ArrastreCaja") && alpacaMovement.arrastrando)
            { // En caso de salir de la influencia de la caja mientras arrastras
                other.GetComponentInParent<CajaScript>().EliminarMovimiento(); // Elimina el movimiento de la caja
                DesacoplarCaja(other, true); // Desacopla la caja
                interactReminder.SetArrastre(false); // Desactiva el reminder de interaccion
            }
            else if (other.CompareTag("ArrastreCaja"))
            { // En caso de salir si no estabas arrastrando
                interactReminder.SetArrastre(false); // Desactiva el reminder de interaccion
            }
            if (other.CompareTag("Palanca") || other.CompareTag("Conmutador"))
            { //
                interactReminder.SetInteraccion(false);
            }
        }
    }

    // Acople y reclamar la caja para arrastrarla
    private void AcoplarCaja(Collider other)
    {
       if(other.transform.parent.parent != this.transform)
        { // Solo acoplar cajas que no esten ya ligadas a la Alpaca
            interactReminder.SetArrastre(false); // Desactivar el reminder de interaccion
            transform.forward = -hitInfo.normal; // Reposicion mirando directamente la cara de la caja
            float projection = Vector3.Project(other.transform.position - transform.position, transform.forward).magnitude - 1.73f; // Calcula la distancia de la alpaca a la caja 
            if (projection != 0)
            { // Si la alpaca no esta justo al lado de la caja, muevela para que coincida donde toca
                transform.Translate(transform.forward * projection,Space.World);
            }
            other.transform.parent.gameObject.GetComponent<CajaScript>().SetParent(this.transform, true); // Enlaza la caja como hija de la alpaca
            alpacaMovement.SetArrastre(true); // Set el modo de movimiento de la alpaca a "arrastre"
        }

    }

    // Desacople de la caja en caso de estar llevandola, con puesta a cero de la caida segun bool
    private void DesacoplarCaja(Collider other, bool conResetCaida)
    {
        if (other.transform.parent.parent == this.transform)
        { // Solo si la caja esta acoplada a esta alpaca
            other.transform.parent.gameObject.GetComponent<CajaScript>().SetParent(); // Desacoplarte la caja
            alpacaMovement.SetArrastre(false); // Sale del modo de movimiento Arrastre de la alpaca
            if (conResetCaida)
            /*{ Resetea la velocidad de caida en caso necesario
                alpacaMovement.velocidadVertical = 0;
            }*/
            interactReminder.SetArrastre(true); // Desactiva el reminder de interaccion
        }
    }

    public void SetInputManager(CustomInputManager manager)
    { // Enlaza el input manager segun la entrada para actores externos
        inputManager = manager;
    }

    public void CompararNormales(Collision col, CajaScript cajaScript)
    { // Compara la normal de una cierta colision con la de la caja a enlazar
        if (hitInfoBool)
        { 
            if (hitInfo.normal == -col.contacts[0].normal)
            { // Si las normales coinciden, elimina el movimiento de la caja
                cajaScript.EliminarMovimiento();
            }
        }        
    }
}
