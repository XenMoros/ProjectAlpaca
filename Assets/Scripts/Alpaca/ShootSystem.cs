using System.Collections.Generic;
using UnityEngine;

public class ShootSystem: MonoBehaviour
{
    // Elementos precacheados en Inspector
    public List <BulletScript> bullets; // Lista de disparos disponibles
    public BulletScript bulletPrefab; // Prefab de los disparos
    public Transform shootOrigin; // Cabeza de la alpca
    public Transform recamara; // Posicion de la Recamara

    // Variables publicas de control
    [Range(0, 50)] public float distanciaAutoapuntado = 15, amplitudAutoapuntado = 5, alturaAutoapuntado = 2;
    [Range(1,10)]public int numeroDisparos = 3; // Numero de disparos en la recamara
    public bool autoapuntado = true; // Booleano de control de autoapuntado
    public bool debug = false;

    // Variables internas 
    private int nBala = 0; // Bala siguiente a ser lanzada
    private float profundidadAutoapuntado;

    // Variables de autoapuntado
    private Collider[] colliderList; // Lista de colliders del autoapuntado
    private Vector3 direccion; // Direccion en la que disparar con autoapuntado
    private RaycastHit hitInfo;

    internal virtual void Start()
    { // Al empezar genera los escupitajos de la pool en la recamara
        bullets = new List<BulletScript>();
        profundidadAutoapuntado = distanciaAutoapuntado / 100;
        for (int i = 0; i < numeroDisparos; i++)
        {
            bullets.Add(Instantiate(bulletPrefab, recamara.position,Quaternion.identity,recamara));
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            ExtDebug.DrawBoxCastBox(shootOrigin.position, new Vector3(amplitudAutoapuntado, alturaAutoapuntado, profundidadAutoapuntado), transform.rotation, transform.forward, distanciaAutoapuntado, Color.red);
        }
    }

    public virtual void Disparar()
    { // Animation event, escupe un proyectil

        if (autoapuntado)
        { // Si el autoapuntado esta activado

            //Lanza un BoxCast, si choca con algo marca esa direccion, si no dispara hacia alante
            if(Physics.BoxCast(shootOrigin.position, new Vector3(amplitudAutoapuntado, alturaAutoapuntado, profundidadAutoapuntado), transform.forward,out hitInfo, transform.rotation, distanciaAutoapuntado, LayerMask.GetMask("CameraLens", "BotonPared", "Guardia"), QueryTriggerInteraction.Collide))
            {
                direccion = hitInfo.collider.transform.position - shootOrigin.position;
            }
            else
            {
                direccion = transform.forward;
            }

        }
        else
        { // Si no hay autoapuntado dispara de frente
            direccion = transform.forward;
        }

        // Ordena a la bala que toca que dispare en la direccion calculada
        bullets[nBala].Escupir(direccion.normalized,shootOrigin.position);
        nBala += 1; // Recarga a la siguiente bala

        // Si llegas al final de la lista de balas, vuelve a empezar
        if (nBala > (bullets.Count - 1))
        {
            nBala = 0;
        }

    }

}

public static class ExtDebug
{
    //Draws just the box at where it is currently hitting.
    public static void DrawBoxCastOnHit(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float hitInfoDistance, Color color)
    {
        origin = CastCenterOnCollision(origin, direction, hitInfoDistance);
        DrawBox(origin, halfExtents, orientation, color);
    }

    //Draws the full box from start of cast to its end distance. Can also pass in hitInfoDistance instead of full distance
    public static void DrawBoxCastBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float distance, Color color)
    {
        direction.Normalize();
        Box bottomBox = new Box(origin, halfExtents, orientation);
        Box topBox = new Box(origin + (direction * distance), halfExtents, orientation);

        Debug.DrawLine(bottomBox.backBottomLeft, topBox.backBottomLeft, color);
        Debug.DrawLine(bottomBox.backBottomRight, topBox.backBottomRight, color);
        Debug.DrawLine(bottomBox.backTopLeft, topBox.backTopLeft, color);
        Debug.DrawLine(bottomBox.backTopRight, topBox.backTopRight, color);
        Debug.DrawLine(bottomBox.frontTopLeft, topBox.frontTopLeft, color);
        Debug.DrawLine(bottomBox.frontTopRight, topBox.frontTopRight, color);
        Debug.DrawLine(bottomBox.frontBottomLeft, topBox.frontBottomLeft, color);
        Debug.DrawLine(bottomBox.frontBottomRight, topBox.frontBottomRight, color);

        DrawBox(bottomBox, color);
        DrawBox(topBox, color);
    }

    public static void DrawBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Color color)
    {
        DrawBox(new Box(origin, halfExtents, orientation), color);
    }
    public static void DrawBox(Box box, Color color)
    {
        Debug.DrawLine(box.frontTopLeft, box.frontTopRight, color);
        Debug.DrawLine(box.frontTopRight, box.frontBottomRight, color);
        Debug.DrawLine(box.frontBottomRight, box.frontBottomLeft, color);
        Debug.DrawLine(box.frontBottomLeft, box.frontTopLeft, color);

        Debug.DrawLine(box.backTopLeft, box.backTopRight, color);
        Debug.DrawLine(box.backTopRight, box.backBottomRight, color);
        Debug.DrawLine(box.backBottomRight, box.backBottomLeft, color);
        Debug.DrawLine(box.backBottomLeft, box.backTopLeft, color);

        Debug.DrawLine(box.frontTopLeft, box.backTopLeft, color);
        Debug.DrawLine(box.frontTopRight, box.backTopRight, color);
        Debug.DrawLine(box.frontBottomRight, box.backBottomRight, color);
        Debug.DrawLine(box.frontBottomLeft, box.backBottomLeft, color);
    }

    public struct Box
    {
        public Vector3 localFrontTopLeft { get; private set; }
        public Vector3 localFrontTopRight { get; private set; }
        public Vector3 localFrontBottomLeft { get; private set; }
        public Vector3 localFrontBottomRight { get; private set; }
        public Vector3 localBackTopLeft { get { return -localFrontBottomRight; } }
        public Vector3 localBackTopRight { get { return -localFrontBottomLeft; } }
        public Vector3 localBackBottomLeft { get { return -localFrontTopRight; } }
        public Vector3 localBackBottomRight { get { return -localFrontTopLeft; } }

        public Vector3 frontTopLeft { get { return localFrontTopLeft + origin; } }
        public Vector3 frontTopRight { get { return localFrontTopRight + origin; } }
        public Vector3 frontBottomLeft { get { return localFrontBottomLeft + origin; } }
        public Vector3 frontBottomRight { get { return localFrontBottomRight + origin; } }
        public Vector3 backTopLeft { get { return localBackTopLeft + origin; } }
        public Vector3 backTopRight { get { return localBackTopRight + origin; } }
        public Vector3 backBottomLeft { get { return localBackBottomLeft + origin; } }
        public Vector3 backBottomRight { get { return localBackBottomRight + origin; } }

        public Vector3 origin { get; private set; }

        public Box(Vector3 origin, Vector3 halfExtents, Quaternion orientation) : this(origin, halfExtents)
        {
            Rotate(orientation);
        }
        public Box(Vector3 origin, Vector3 halfExtents)
        {
            this.localFrontTopLeft = new Vector3(-halfExtents.x, halfExtents.y, -halfExtents.z);
            this.localFrontTopRight = new Vector3(halfExtents.x, halfExtents.y, -halfExtents.z);
            this.localFrontBottomLeft = new Vector3(-halfExtents.x, -halfExtents.y, -halfExtents.z);
            this.localFrontBottomRight = new Vector3(halfExtents.x, -halfExtents.y, -halfExtents.z);

            this.origin = origin;
        }


        public void Rotate(Quaternion orientation)
        {
            localFrontTopLeft = RotatePointAroundPivot(localFrontTopLeft, Vector3.zero, orientation);
            localFrontTopRight = RotatePointAroundPivot(localFrontTopRight, Vector3.zero, orientation);
            localFrontBottomLeft = RotatePointAroundPivot(localFrontBottomLeft, Vector3.zero, orientation);
            localFrontBottomRight = RotatePointAroundPivot(localFrontBottomRight, Vector3.zero, orientation);
        }
    }

    //This should work for all cast types
    static Vector3 CastCenterOnCollision(Vector3 origin, Vector3 direction, float hitInfoDistance)
    {
        return origin + (direction.normalized * hitInfoDistance);
    }

    static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        Vector3 direction = point - pivot;
        return pivot + rotation * direction;
    }
}

