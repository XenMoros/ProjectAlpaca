using UnityEngine;

public class MonkeyCinematics : CinematicByText
{
    /// <summary>
    /// Script FINAL para controlar los movimientos "Cinematicos" in-game de los monos, es MUY ESPECIFICO
    /// </summary>
    public CajaScript caja; // La caja que este asignada al mono (si es necesario)

    RaycastHit hitInfo; // La informacion de los hits del Raycast
    public LayerMask collideLayers; // Las Layers que han de tener en cuenta los rayos

    internal override void Start()
    {
        base.Start(); // Hacer el Start de su clase padre
        MankeyRedefineAnimationGuide(); // Redefinir parametros de las cinematicas ESPECIFICOS para los monos
    }

    internal override void Update()
    {
        base.Update(); // Hacer la Uptdate de la clase padre
    }

    internal override void LateUpdate()
    {
        base.LateUpdate(); // Hacer la LateUptdate de la classe padre
    }

    void MankeyRedefineAnimationGuide()
    { // Redefine algunos valores de las animaciones propias de los monos
        for(int i=0;i<arrayAnimaciones.Length;i++)
        { // Recorre la array con la lista de animaciones 
            switch (arrayAnimaciones[i].animationName)
            {
                case "Jump": // Si encuentra un salto, redefine los valores de delay tanto de inicio como de final
                    arrayAnimaciones[i] = new AnimationGuide(arrayAnimaciones[i].animationName,
                                                                arrayAnimaciones[i].animationLength,
                                                                arrayAnimaciones[i].animationIsMoving,
                                                                0.5f,
                                                                0.83f);
                    break;
                case "Pull": // Si encuentra un pull, redefine el delay de inicio y marca como que anda hacia atras
                    arrayAnimaciones[i] = new AnimationGuide(arrayAnimaciones[i].animationName,
                                                                arrayAnimaciones[i].animationLength,
                                                                arrayAnimaciones[i].animationIsMoving,
                                                                0.3f,
                                                                arrayAnimaciones[i].animationFinishMovement,
                                                                true);
                    break;
                default:
                break;
            }
        }
    }

    public void AcoplarCaja()
    { // Animation event, acopla la caja linkeada al mono
        if (caja != null)
        {
            caja.SetParent(this.transform, false);
        }
    }

    public void DesacoplarCaja()
    { // Animation event, desacopla la caja
        if (caja != null)
        {
            caja.SetParent();
        }
    }

    public void DarPatada()
    { // Animation event, da una patada similar a la coz de la alpaca
        if (Physics.BoxCast(transform.position + Vector3.up, new Vector3(1, 1, 0.1f), transform.forward, out hitInfo, transform.rotation, 1f, collideLayers, QueryTriggerInteraction.Ignore))
        {
            if (hitInfo.collider.CompareTag("Caja"))
            {
                hitInfo.collider.GetComponent<CajaScript>().PushCaja(-hitInfo.normal);
            }
        }
    }
}