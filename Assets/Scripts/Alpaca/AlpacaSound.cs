using UnityEngine;

public class AlpacaSound : MonoBehaviour
{
    // Elementos precacheados en Inspector
    public AudioManager audioControll; // Controlador de Audio
    public AudioSource audioSource; // AudioSource de la Alpaca

    void Update()
    {
        if (Input.GetButtonDown("Y"))
        {
            // Al Berrear, reproducimos el audio 0 desde nuestra source
            audioControll.PlaySound(0, audioSource);

            //Ademas alertamos a todos los guardias de donde se ha berreado, en un radio de 100 unidades
            Collider[] possibleEnemiesWhoHeardMe = Physics.OverlapSphere(transform.position, 100, LayerMask.GetMask("Guardia"));
            foreach (Collider enemy in possibleEnemiesWhoHeardMe)
            {
                enemy.GetComponent<GuardiaMovement>().SetObjective(transform.position);
            }
        }
        // Al Cocear, reproducimos el audio 1 desde nuestra source
        if (Input.GetButtonDown("B"))
        {
            audioControll.PlaySound(1, audioSource);
        }
    }
}
