using UnityEngine;

[RequireComponent(typeof(AlpacaMovement))]
public class InteractScript : MonoBehaviour
{

    // Referencias precacheadas en inspector de otros GameObjects
    public Transform entorno;
    public AlpacaMovement alpacaMovement;
    public CustomInputManager inputManager;
    public InteractionReminder interactReminder;

    // Valores para casteo de rayos
    LayerMask cajaLayerMask;
    RaycastHit hitInfo;
    public bool hitInfoBool;

    public void Reset()
    {
        alpacaMovement = GetComponent<AlpacaMovement>();
    }

    private void Start()
    {
        // LayerMask solo con los objetos tipo "Caja" para el Raycast del acople de cajas
        cajaLayerMask = LayerMask.GetMask("Caja");
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
                    {
                        other.GetComponentInParent<CajaScript>().ActivarMovimiento();
                        hitInfoBool = true;
                        // Si el objeto con el que choca el rayo casteado coincide con el objeto del trigger, asigna
                        if (hitInfo.collider.gameObject == other.transform.parent.gameObject)
                        {
                            interactReminder.SetArrastre(true);
                            // Si mantienes la X te acopla la caja
                            if (inputManager.GetButton("Interact"))
                            {
                                AcoplarCaja(other);
                            }
                        }
                        else
                        {
                            interactReminder.SetArrastre(false);
                        }
                    }
                    else
                    {
                        interactReminder.SetArrastre(false);
                        hitInfoBool = false;
                    }
                }
                else
                {
                    interactReminder.SetArrastre(false);
                }
                // Si sueltas la X te desacopla la caja con reset de caida
                if (!inputManager.GetButton("Interact"))
                {
                    other.GetComponentInParent<CajaScript>().EliminarMovimiento();
                    DesacoplarCaja(other, true);
                }
            }
            // En caso de pasar a estar cayendo mientras arrastras una caja, desacoplar la misma
            else if (other.CompareTag("ArrastreCaja") && alpacaMovement.arrastrando)
            {
                DesacoplarCaja(other, false);
            }
            // Si estaas en la influencia de una palanca
            
            if ((other.CompareTag("Conmutador") || other.CompareTag("Palanca")) && !alpacaMovement.arrastrando)
            {
                interactReminder.SetInteraccion(true);
                // Si pulsas X activar la palanca
                if (inputManager.GetButtonDown("Interact"))
                {
                    if (other.CompareTag("Palanca")) other.gameObject.GetComponent<Palanca>().Activate();
                    else other.gameObject.GetComponent<Conmutador>().Activate();

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!alpacaMovement.pause)
        {
            // En caso de salir de la influencia de la caja, desacoplarte automaticamente
            if (other.CompareTag("ArrastreCaja") && alpacaMovement.arrastrando)
            {
                other.GetComponentInParent<CajaScript>().EliminarMovimiento();
                DesacoplarCaja(other, true);
                interactReminder.SetArrastre(false);
            }
            else if (other.CompareTag("ArrastreCaja"))
            {
                interactReminder.SetArrastre(false);
            }
            if (other.CompareTag("Palanca") || other.CompareTag("Conmutador"))
            {
                interactReminder.SetInteraccion(false);
            }
        }
    }

    // Acople y reclamar la caja para arrastrarla
    private void AcoplarCaja(Collider other)
    {
        interactReminder.SetArrastre(false);
        // Reposicion mirando directamente la cara de la caja
        transform.forward = -hitInfo.normal;
        float projection = Vector3.Project(other.transform.position - transform.position,transform.forward).magnitude - 1.73f;
        if (projection != 0)
        {
            transform.Translate(- transform.forward * projection);
        }
        // Asigna la caja como hija tuya para que te siga
        other.transform.parent.gameObject.GetComponent<CajaScript>().SetParent(this.transform,true);
        // Flags de movimiento
        alpacaMovement.SetArrastre(true);
    }

    // Desacople de la caja en caso de estar llevandola, con puesta a cero de la caida segun bool
    private void DesacoplarCaja(Collider other, bool conResetCaida)
    {
        if (other.transform.parent.parent.gameObject == this.gameObject)
        {
            // Desacoplarte la caja
            other.transform.parent.gameObject.GetComponent<CajaScript>().SetParent();
            // Flags de movimiento
            alpacaMovement.SetArrastre(false);
            // Resetea la caida en caso necesario (por bug)
            if (conResetCaida)
            {
                alpacaMovement.velocidadVertical = 0;
            }
            interactReminder.SetArrastre(true);
        }
    }

    public void SetInputManager(CustomInputManager manager)
    {
        inputManager = manager;
    }

    public void CompararNormales(Collision col, CajaScript cajaScript)
    {
        if (hitInfoBool)
        {
            if (hitInfo.normal == -col.contacts[0].normal)
            {
                cajaScript.EliminarMovimiento();
            }
        }        
    }
}
