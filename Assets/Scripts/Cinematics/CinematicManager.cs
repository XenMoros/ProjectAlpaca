using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    public Animator animator; // Animator del actor a hacer comportamiento cinematico
    public WaypointManager waipoints; // Los puntos de ruta de la cinematica
    public float rotationSpeed = 360f; // La velocidad de rotacion
    public float maxAnglePerSecond = 720f; // Maximo angulo por segundo de rotacion

    float animationTimer; // Timer de tiempo en una fase cinematica concreta
    float animationTime; // Tiempo de la fase cinematica actual
    internal bool animacionAcabada; // Flag de fase cinematica acabada
    internal int animNumber; // Numero de fase cinematica reproduciendose
    internal AnimationGuide[] arrayAnimaciones; // Array con las fases de cinematica a reproducir
    Vector3 direction; // Direccion de movimiento
    float velocity; // Velocidad de movimiento
    float finishMovement; // Tienpo en el que finaliza el movimiento
    Vector3 targetDirection; // Direccion hacia donde ha de mirar el actor

    RuntimeAnimatorController ac; // Animator controler en runtime 

    internal virtual void Start()
    {
        ac = animator.runtimeAnimatorController; // Asigna el runtime animator controler actual
        RellenarArrayAnimaciones(); // Rellena la lista de fases cinematicas
        animNumber = 0; // Set la fase inicial a la 0
        ReadAnimation(ref animNumber); // Lee la primera fase
        animator.SetTrigger(arrayAnimaciones[animNumber].animationName); // Activa el trigger de animacion correspondiente
        animationTimer = 0; // Marca el inicio de la fase actual
        animacionAcabada = false; // Flag como false el final de la fase actual
    }

    // Update is called once per frame
    internal virtual void Update()
    {
        GirarPersonaje(); // Reorienta el personage

        if (arrayAnimaciones[animNumber].animationIsMoving &&
            animationTimer >= arrayAnimaciones[animNumber].animationDelayMovement &&
            animationTimer <= finishMovement)
        { // Si la fase incluye movimiento y estas dentro de los tiempos en los que se mueve
            this.transform.Translate(direction * velocity * Time.deltaTime, Space.World); // Mover el actor
        }

        animationTimer += Time.deltaTime; // Avanzar el tiempo de fase activa

        if (animationTimer >= animationTime)
        { // Si ya has acabado la fase, marcala como tal
            animacionAcabada = true;
        }
    }

    internal virtual void LateUpdate()
    {
        if (animacionAcabada)
        { // Si ha terminado la fase actual
            animationTimer = 0; // Set el inicio de la fase siguiente
            animNumber += 1; // Avanza el numero de fase
            ReadAnimation(ref animNumber); // Lee la fase siguiente
            animator.SetTrigger(arrayAnimaciones[animNumber].animationName); // Activa el trigger de la animacion correspondiente

            animacionAcabada = false; // Desmarca como que ha acabado la fase
        }
    }

    // Funcion para leer la fase de animacion que toque. El numero entra por referencia para poderlo modificar dentro del script
    internal void ReadAnimation(ref int animationNumber)
    {
        if(animationNumber >= arrayAnimaciones.Length)
        { // Si el numero sobrepassa la array de fases, vuelve al principio
            animationNumber = 0;
        }

        if(arrayAnimaciones[animationNumber].animationLength > 0)
        { // Si la fase declara una duracion, conservala como lo que va a durar la fase
            animationTime = arrayAnimaciones[animationNumber].animationLength;
        }
        else
        { // Sino, la fase durara lo que dure la animacion asociada
            for (int i = 0; i < ac.animationClips.Length; i++)
            {
                if (ac.animationClips[i].name == arrayAnimaciones[animationNumber].animationName)
                { // Busqueda de la animacion con el nombre correspondiente
                    animationTime = ac.animationClips[i].length;
                }
            }
        }

        if(arrayAnimaciones[animationNumber].animationIsMoving)
        { // Si la fase declara que hay movimiento
            waipoints.AvanzarWaypoint(); // Selecciona el waypoint siguiente
            direction = waipoints.RetornarWaypoint().RetornarPosition() - transform.position; // Marca la direccion hacia dicho waypoint
            velocity = direction.magnitude / (animationTime - arrayAnimaciones[animationNumber].animationDelayMovement - arrayAnimaciones[animationNumber].animationFinishMovement);
            // Pon la velocidad tal que tarde en hacer esa distancia el tiempo que tiene de movimniento 
            // (teniendo en cuenta los delays de inicio y fin de movimiento respecto la animacion)
            direction.Normalize(); // Normaliza la direccion

            if(direction != Vector3.zero)
            { // Si la direccion es diferente de cero, mira si la direccion de mirar ha de ser directa o al reves
              // y ademas criba la coordenada Y para mirar siempre en el plano XZ
                //if (!arrayAnimaciones[animationNumber].animationReverseMovement) targetDirection = new Vector3(direction.x, 0, direction.z).normalized;
                //else targetDirection = -new Vector3(direction.x, 0, direction.z).normalized;
            }

            finishMovement = animationTime - arrayAnimaciones[animationNumber].animationFinishMovement; // Marca el tiempo de acabar el movimiento segun entrada
        }

        if(waipoints != null)
        {
            if (!arrayAnimaciones[animationNumber].animationReverseMovement) targetDirection = waipoints.RetornarWaypoint().transform.forward;
            else targetDirection = -waipoints.RetornarWaypoint().transform.forward;
        }
        else
        {
            targetDirection = transform.forward;
        }
    }

    private void GirarPersonaje()
    {// Gira la direccion donde mira el personaje gradualmente
        // First calculate the look vector as normal

        Vector3 newForward = Vector3.Slerp(transform.forward, targetDirection, Time.deltaTime * rotationSpeed);

        // Now check if the new vector is rotating more than allowed
        float angle = Vector3.Angle(transform.forward, newForward);
        float maxAngle = maxAnglePerSecond * Time.deltaTime;
        if (angle > maxAngle)
        {
            // It's rotating too fast, clamp the vector
            newForward = Vector3.Slerp(transform.forward, newForward, maxAngle / angle);
        }    
        
        // Assign the new forward to the transform
        if (newForward != Vector3.zero)
        {
            transform.forward = newForward;
        }
    }

    internal virtual void RellenarArrayAnimaciones()
    { // Rellena el array de fases de cinematica
        arrayAnimaciones = new AnimationGuide[1];
        arrayAnimaciones[0] = new AnimationGuide("Idle");
    }
}

internal struct AnimationGuide
{ // Estructura que guarda cada una de las fases de cinematica
    public string animationName; // Nombre de la animacion
    public float animationLength; // Duracion de la fase
    public bool animationIsMoving; // Si la fase conlleva movimiento
    public float animationDelayMovement; // Delay para empezar el movimiento
    public float animationFinishMovement; // Tiempo de finalizar el movimiento antes del final
    public bool animationReverseMovement; // Flag de si has de mirar En contra del movimiento

    public AnimationGuide(string aName)
    {// Constructor SOLO con el nombre de animacion
        animationName = aName;
        animationLength = 0;
        animationIsMoving = false;
        animationDelayMovement = 0;
        animationFinishMovement = 0;
        animationReverseMovement = false;
    }

    public AnimationGuide(string aName,float aLength,bool aIsMoving)
    {// Constructor con variables de delay y reverse no informacas (a 0)
        animationName = aName;
        animationLength = aLength;
        animationIsMoving = aIsMoving;
        animationDelayMovement = 0;
        animationFinishMovement = 0;
        animationReverseMovement = false;
    }

    public AnimationGuide(string aName, float aLength, bool aIsMoving, float aDelayMovement, float aFinishMovement)
    {// Constructor con reverse no informado (a zero)
        animationName = aName;
        animationLength = aLength;
        animationIsMoving = aIsMoving;
        animationDelayMovement = aDelayMovement;
        animationFinishMovement = aFinishMovement;
        animationReverseMovement = false;
    }

    public AnimationGuide(string aName, float aLength, bool aIsMoving, float aDelayMovement, float aFinishMovement, bool aReverseMovement)
    {// Constructor con todo informado
        animationName = aName;
        animationLength = aLength;
        animationIsMoving = aIsMoving;
        animationDelayMovement = aDelayMovement;
        animationFinishMovement = aFinishMovement;
        animationReverseMovement = aReverseMovement;
    }
}