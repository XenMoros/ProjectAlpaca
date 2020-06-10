using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    public Animator animator;
    public WaypointManager waipoints;

    float animationTimer;
    float animationTime;
    internal bool animacionAcabada;
    internal int animNumber;
    internal AnimationGuide[] arrayAnimaciones;
    Vector3 direction;
    float velocity;
    float finishMovement;

    RuntimeAnimatorController ac;
    internal virtual void Start()
    {
        ac = animator.runtimeAnimatorController;
        RellenarArrayAnimaciones();
        animNumber = 0;
        ReadAnimation(ref animNumber);
        animator.SetTrigger(arrayAnimaciones[animNumber].animationName);
        animationTimer = 0;
        animacionAcabada = false;
    }

    // Update is called once per frame
    internal virtual void Update()
    {
        if (arrayAnimaciones[animNumber].animationIsMoving &&
            animationTimer >= arrayAnimaciones[animNumber].animationDelayMovement &&
            animationTimer <= finishMovement)
        {
            this.transform.Translate(direction * velocity * Time.deltaTime, Space.World);
        }

        animationTimer += Time.deltaTime;

        if (animationTimer >= animationTime)
        {
            animacionAcabada = true;
        }
    }

    internal virtual void LateUpdate()
    {

        if (animacionAcabada)
        {
            animationTimer = 0;
            animNumber += 1;
            ReadAnimation(ref animNumber);
            animator.SetTrigger(arrayAnimaciones[animNumber].animationName);

            animacionAcabada = false;
        }
    }

    internal void ReadAnimation(ref int animationNumber)
    {
        if(animationNumber >= arrayAnimaciones.Length)
        {
            animationNumber = 0;
        }

        if(arrayAnimaciones[animationNumber].animationLength > 0)
        {
            animationTime = arrayAnimaciones[animationNumber].animationLength;
        }
        else
        {
            for (int i = 0; i < ac.animationClips.Length; i++)
            {
                if (ac.animationClips[i].name == arrayAnimaciones[animationNumber].animationName)
                {
                    animationTime = ac.animationClips[i].length;
                }
            }
        }

        if(arrayAnimaciones[animationNumber].animationIsMoving)
        {
            waipoints.AvanzarWaypoint();
            direction = waipoints.RetornarWaypoint().RetornarPosition() - transform.position;
            velocity = direction.magnitude / (animationTime - arrayAnimaciones[animationNumber].animationDelayMovement - arrayAnimaciones[animationNumber].animationFinishMovement);
            direction.Normalize();

            transform.forward = new Vector3(direction.x, 0, direction.z).normalized;
            finishMovement = animationTime - arrayAnimaciones[animationNumber].animationFinishMovement;
        }


    }

    internal virtual void RellenarArrayAnimaciones()
    {
        arrayAnimaciones = new AnimationGuide[1];
        arrayAnimaciones[0] = new AnimationGuide("Idle", 0, false);
    }
}

internal struct AnimationGuide
{
    public string animationName;
    public float animationLength;
    public bool animationIsMoving;
    public float animationDelayMovement;
    public float animationFinishMovement;

    public AnimationGuide(string aName,float aLength,bool aIsMoving)
    {
        animationName = aName;
        animationLength = aLength;
        animationIsMoving = aIsMoving;
        animationDelayMovement = 0;
        animationFinishMovement = 0;
    }

    public AnimationGuide(string aName, float aLength, bool aIsMoving, float aDelayMovement, float aFinishMovement)
    {
        animationName = aName;
        animationLength = aLength;
        animationIsMoving = aIsMoving;
        animationDelayMovement = aDelayMovement;
        animationFinishMovement = aFinishMovement;
    }
}