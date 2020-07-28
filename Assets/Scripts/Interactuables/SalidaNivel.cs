using UnityEngine;

public class SalidaNivel : MonoBehaviour
{

    LevelManager levelManager; // El level manager

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { // Si el player llega a la salida da la señal de nivel completado
            Debug.Log("Salida");
            StartCoroutine(levelManager.LevelComplete());
        }
    }

    public void SetLevelManager(LevelManager levelManager)
    { // Enlaza el level manager segun la entrada
        this.levelManager = levelManager;
    }
}
