using UnityEngine;
using UnityEditor;

public class MonkeyCinematics : CinematicByText
{
    public CajaScript caja;

    RaycastHit hitInfo;
    public LayerMask collideLayers;

    internal override void Start()
    {
        base.Start();
        MankeyRedefineAnimationGuide();
    }

    internal override void Update()
    {
        base.Update();
    }

    internal override void LateUpdate()
    {
        base.LateUpdate();
    }

    void MankeyRedefineAnimationGuide()
    {
        for(int i=0;i<arrayAnimaciones.Length;i++)
        {
            switch (arrayAnimaciones[i].animationName)
            {
                case "Jump":
                    arrayAnimaciones[i] = new AnimationGuide(arrayAnimaciones[i].animationName,
                                                                arrayAnimaciones[i].animationLength,
                                                                arrayAnimaciones[i].animationIsMoving,
                                                                0.5f,
                                                                0.83f);
                    break;
                case "Pull":
                    arrayAnimaciones[i] = new AnimationGuide(arrayAnimaciones[i].animationName,
                                                                arrayAnimaciones[i].animationLength,
                                                                arrayAnimaciones[i].animationIsMoving,
                                                                0.25f,
                                                                arrayAnimaciones[i].animationFinishMovement,
                                                                true);
                    break;
                default:
                break;
            }
        }
    }

    public void AcoplarCaja()
    {
        if (caja != null)
        {
            caja.SetParent(this.transform, false);
        }
    }

    public void DesacoplarCaja()
    {
        if (caja != null)
        {
            caja.SetParent();
        }
    }

    public void DarPatada()
    {
        if (Physics.BoxCast(transform.position + Vector3.up, new Vector3(1, 1, 0.1f), transform.forward, out hitInfo, transform.rotation, 1f, collideLayers, QueryTriggerInteraction.Ignore))
        {
            if (hitInfo.collider.CompareTag("Caja"))
            {
                hitInfo.collider.GetComponent<CajaScript>().PushCaja(-hitInfo.normal);
            }
        }
    }
}