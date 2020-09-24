
public class Conmutador : Interactuable
{
    // Elementos precacheados en Inspector
    public bool estatico = false; // Define si el conmutador marca un piso/posicion concreta o es sequencial
    public int numero = 0; // Marca el numero de posicion en caso de ser estatico

    public override void Start()
    {
        base.Start(); // Start del padre
        tipoInteractuable = Tipo.Ascensor;
    }

    // En activar la palanca, empezar la animacion y activar el objeto asociado
    public override void Activate()
    {
        if (active)
        {
            if (estatico)
            { // Si es estatico activa siempre el mismo numero
                base.Activate(numero);
            }
            else
            { // Si no activa el siguiente
                base.Activate();
            }

            //interactAnimator.SetTrigger("Activado"); // Activa el trigger del conmutador
        }
    }

}
