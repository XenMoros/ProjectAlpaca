using UnityEngine;
using UnityEngine.SceneManagement;

// PENDIENTE REESTRUCTURAR TIMERS I SCRIPT DE RECARGA DE NIVEL
public class EntradaYSalidaGM : MonoBehaviour
{
    // Elementos precacheados en inspector
    public Transform spawnPoint; // Punto de spawn
    public Transform playerT; // Alpaca
    public Transform camara; // Camara
    public AlpacaMovement MovimientoAlpaca; // Movimiento de la alpaca

    // Variables de control
    private float tiempoEsperaInicio = 1f;

    // Timers de control
    public float timer = 1;

    void Start()
    {
        // Reposiciona el jugador y la camara al iniciar la escena
        camara.position = spawnPoint.position + new Vector3(0, 1, -1.5f);
        playerT.position = spawnPoint.position;
        timer = 0;        
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        DevolverMovimiento();
    }

    public void DevolverSP() // UNUSED
    {
        MovimientoAlpaca.enabled = false;
        playerT.eulerAngles = new Vector3(0, 0, 0);
        playerT.position = spawnPoint.position;
        camara.position = spawnPoint.position + new Vector3(0, 1, -1.5f);
    }

    public void DevolverMovimiento()
    {
        if (timer < 0)
        {
            MovimientoAlpaca.enabled = true;
        }
    }

    public void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
