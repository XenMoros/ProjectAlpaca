using UnityEngine;

public class Palanca : Interactuable
{
    bool palancaState = false;

    public override void Start()
    {
        base.Start();
    }

    // En activar la palanca, empezar la animacion y activar el objeto asociado
    public override void Activate()
    {
        palancaState = !palancaState;
        interactAnimator.SetBool("Activada", palancaState);
        base.Activate(palancaState);
    }
}
