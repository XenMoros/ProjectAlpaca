using UnityEngine;

[RequireComponent(typeof(AlpacaMovement))]
public class CozScript : MonoBehaviour
{
    // Elementos precacheados en Inspector
    public AlpacaMovement alpacaMovement; //El movimiento de la Alaca
    public CustomInputManager inputManager;
    public LayerMask collideLayers;
    RaycastHit hitInfo;
    // GameObject de la Coz
    public GameObject coz;

    public void Reset()
    {
        alpacaMovement = GetComponent<AlpacaMovement>();
        coz = GameObject.Find("Coz");
    }

    void Start()
    {
        //Capturar la coz y desactivarla
        coz = GameObject.FindGameObjectWithTag("Coz");
        coz.SetActive(false);
    }

    void Update()
    {
        if (!alpacaMovement.pause)
        {
            Debug.DrawLine(coz.transform.position, coz.transform.position + (-alpacaMovement.transform.forward * 1f));
            // Mirar las acciones de la coz
            if (inputManager.GetButtonDown("Coz") && !(alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Subida || alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Caida
                || alpacaMovement.faseMovimiento == AlpacaMovement.FaseMovimiento.Cozeo || alpacaMovement.arrastrando))
            {
                alpacaMovement.faseMovimiento = AlpacaMovement.FaseMovimiento.Cozeo;
            }
        }
    }

    public void ActivarColliderCoz()
    {
        Debug.Log("A cozear");
        //coz.SetActive(true);
        if(Physics.BoxCast(coz.transform.position, new Vector3(1,1,0.1f),-alpacaMovement.transform.forward,out hitInfo,alpacaMovement.transform.rotation,1f, collideLayers, QueryTriggerInteraction.Ignore))
        {
            Debug.Log("Di con algo");
            if (hitInfo.collider.CompareTag("Caja"))
            {
                Debug.Log("Di con la caja");
                Debug.Log(hitInfo.normal);
                hitInfo.collider.GetComponent<CajaScript>().PushCaja(-hitInfo.normal);
            }
        }
    }

    public void TerminarCozeo() 
    {
        coz.SetActive(false);
        alpacaMovement.faseMovimiento = AlpacaMovement.FaseMovimiento.Idle;
    }

    public void SetInputManager(CustomInputManager manager)
    {
        inputManager = manager;
    }
}
