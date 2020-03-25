using UnityEngine;

public class Palanca : MonoBehaviour
{
    // Elementos precacheados en Inspector
    public Animator palancaAnimator; // El animator de la palanca
    public GameObject activate; // El GameObject a activar 
    bool palancaState = false;

    // Interfaz Iactivable del objeto a activar
    IActivable activateObj;
    private void Start()
    {
        // Asignacion de la interfaz IActivable correspodiente
        activateObj = activate.GetComponent<IActivable>();
    }

    // En activar la palanca, empezar la animacion y activar el objeto asociado
    public void ActivatePalanca()
    {

        palancaState = !palancaState;
        activateObj.SetActivationState(palancaState);
        palancaAnimator.SetBool("Activada", palancaState);

    }
}
