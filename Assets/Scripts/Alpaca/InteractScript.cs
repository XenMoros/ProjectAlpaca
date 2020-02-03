using UnityEngine;

[RequireComponent(typeof(AlpacaMovement))]
public class InteractScript : MonoBehaviour
{

    // Referencias precacheadas en inspector de otros GameObjects
    public Transform entorno;
    public AlpacaMovement alpacaMovement;

    // Valores para casteo de rayos
    LayerMask cajaLayerMask;
    RaycastHit hitInfo;

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
        // En caso de estar en influencia de una caja y no estar en el aire
        if (other.CompareTag("ArrastreCaja") && !alpacaMovement.onAir)
        {
            // Si mantienes la X te acopla la caja
            if (Input.GetButton("X"))
            {
                AcoplarCaja(other);
            }
            // Si sueltas la X te desacopla la caja con reset de caida
            else
            {
                DesacoplarCaja(other, true);
            }
        }
        // En caso de pasar a estar cayendo mientras arrastras una caja, desacoplar la misma
        else if (other.CompareTag("ArrastreCaja") && alpacaMovement.arrastrando)
        {
            DesacoplarCaja(other, false);
        }
        // Si estaas en la influencia de una palanca
        else if (other.CompareTag("Palanca"))
        {
            // Si pulsas X activar la palanca
            if (Input.GetButtonDown("X"))
            {
                other.gameObject.GetComponent<Palanca>().ActivatePalanca();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // En caso de salir de la influencia de la caja, desacoplarte automaticamente
        if (other.CompareTag("ArrastreCaja") && alpacaMovement.arrastrando)
        {
                DesacoplarCaja(other, true);
        }
        
    }

    // Acople y reclamar la caja para arrastrarla
    private void AcoplarCaja(Collider other)
    {
        if (other.transform.parent.parent.gameObject != this.gameObject)
        {
            //Compureba que mires a la caja
            if (Physics.Raycast(transform.position + new Vector3(0, transform.localScale.y / 4f, 0), transform.forward, out hitInfo, 5f, cajaLayerMask, QueryTriggerInteraction.Ignore))
            { 
                // Si el objeto con el que choca el rayo casteado coincide con el objeto del trigger, asigna
                if (hitInfo.collider.gameObject == other.transform.parent.gameObject)
                {
                // Reposicion mirando directamente la cara de la caja
                transform.forward = -hitInfo.normal;
                    // Asigna la caja como hija tuya para que te siga
                    other.transform.parent.gameObject.GetComponent<CajaScript>().AsociarPadre(this.transform);
                    // Flags de movimiento
                    alpacaMovement.SetArrastre(true);
                }
            }
        }
    }

    // Desacople de la caja en caso de estar llevandola, con puesta a cero de la caida segun bool
    private void DesacoplarCaja(Collider other, bool conResetCaida)
    {
        if (other.transform.parent.parent.gameObject == this.gameObject)
        {
            // Desacoplarte la caja
            other.transform.parent.gameObject.GetComponent<CajaScript>().AsociarPadre(entorno);
            // Flags de movimiento
            alpacaMovement.SetArrastre(false);
            // Resetea la caida en caso necesario (por bug)
            if (conResetCaida)
            {
                alpacaMovement.alpacaRigidbody.velocity.Set(0,0,0);
            }
        }
    }

}
