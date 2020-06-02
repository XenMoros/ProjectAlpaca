

public class Conmutador : Interactuable
{
    // Elementos precacheados en Inspector
    public bool estatico = false;
    public int numero = 0;

    public override void Start()
    {
        base.Start();
    }

    // En activar la palanca, empezar la animacion y activar el objeto asociado
    public override void Activate()
    {
        if (estatico)
        {
            base.Activate(numero);
        }
        else
        {
            base.Activate();
        }

        interactAnimator.SetTrigger("Activado");

    }

}
