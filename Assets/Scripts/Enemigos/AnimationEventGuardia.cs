using UnityEngine;

public class AnimationEventGuardia : MonoBehaviour
{// Eventos de animacion del guardia
    public GuardiaMovement guardiaMovement; // El script del guardia

    public void FinalBuscar()
    { // Animation event, final de la animacion de buscar
        guardiaMovement.FinalBuscar();
    }

    public void FinalAturdido()
    { // Animation event, final de aturdimiento
        guardiaMovement.FinalAturdido();
    }
}
