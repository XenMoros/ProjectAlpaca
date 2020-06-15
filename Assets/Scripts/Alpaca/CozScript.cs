using UnityEngine;

[RequireComponent(typeof(AlpacaMovement))]
public class CozScript : MonoBehaviour
{
    // Elementos precacheados en Inspector
    public AlpacaMovement alpacaMovement; //El movimiento de la Alaca
    public CustomInputManager inputManager; // Input manager (segun prod o debug)
    public LayerMask collideLayers; // Layers con las que colisionara la coz 
    RaycastHit hitInfo; // Informacion del hit del raycast
    public Transform coz; // Posicion desde donde se da la coz

    public void Reset()
    { // Al attachear este componente busca el AlpacaMovement automaticamente
        alpacaMovement = GetComponent<AlpacaMovement>();
    }

    void Update()
    {
        if (!alpacaMovement.pause)
        {
            if (inputManager.GetButtonDown("Coz") && !(alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Subida || alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Caida
                || alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Cozeo || alpacaMovement.arrastrando))
            { // Si picas el boton de cozeo y no estas ni en el aire, ni coceando ya ni arrastrando, ponte en fase de cozeo
                alpacaMovement.faseMovimiento = AlpacaMovement.FaseMovimiento.Cozeo;
            }
        }
    }

    public void ActivarColliderCoz()
    { // Animation event, lanza un raycast para ver si la coz da con algo
        if(Physics.BoxCast(coz.position, new Vector3(1,1,0.1f),-alpacaMovement.transform.forward,out hitInfo,alpacaMovement.transform.rotation,1f, collideLayers, QueryTriggerInteraction.Ignore))
        {
            if (hitInfo.collider.CompareTag("Caja"))
            { // Si das con una caja, empujala segun la cara en la que le das
                hitInfo.collider.GetComponent<CajaScript>().PushCaja(-hitInfo.normal);
            }
        }
    }

    public void TerminarCozeo() 
    { // Animation event, al acabar el coceo pasa a Idle
        alpacaMovement.faseMovimiento = AlpacaMovement.FaseMovimiento.Idle;
    }

    public void SetInputManager(CustomInputManager manager)
    { // Set del input manager segun entrada, para actores externos
        inputManager = manager;
    }
}
