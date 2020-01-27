using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventGuardia : MonoBehaviour
{

    public GuardiaMovement guardiaMovement;

    public void PararGuardia()
    {
        guardiaMovement.AnimationEvent();
    }

    public void SalirEscupitajo()
    {
        guardiaMovement.QuitarseEscupitajos();
    }
}
