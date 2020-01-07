using UnityEngine;

// PENDIENTE REESTRUCTURAR SCRIPTS DE RECARGA DE NIVEL
public class FinalTrigger : MonoBehaviour
{
    // Objetos precacheados en Inspector
    public EntradaYSalidaGM gameManager; // GameManager

    private void OnTriggerEnter(Collider other)
    {
        // Cuando la Alpaca llegue al final, y si ha pasado suficiente tiempo, reinicia el nivel
        if (other.gameObject.CompareTag("Player") && gameManager.timer <= 0)
        {
            gameManager.Reload();
            gameManager.timer = 1;
        }
    }
}
