using UnityEngine;

[RequireComponent(typeof(AlpacaMovement))]
public class CozScript : MonoBehaviour
{
    // Elementos precacheados en Inspector
    public AlpacaMovement alpacaMovement; //El movimiento de la Alaca
    public CustomInputManager inputManager;

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
        coz.SetActive(true);
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
