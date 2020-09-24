public class Palanca : Interactuable
{
    public bool palancaState = false; // El estado de activacion de la palanca

    public override void Start()
    {
        base.Start(); // Base start
        tipoInteractuable = Tipo.Palanca;
    }

    public override void Activate()
    { // En activar la palanca, empezar la animacion y activar el objeto asociado
        if (active)
        {
            palancaState = !palancaState;
            interactAnimator.SetBool("Activada", palancaState);
        }
    }

    public void ActivatePalanca()
    {
        base.Activate(palancaState);
    }

}
